namespace Byzan.Biz.Abstractions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Text.Json;
    using Zy.App.Common.AppExtensions;
    using Zy.App.Common.Interfaces;
    using Zy.App.Common.Models;

    public static class QueryableExtensions
    {
        private static readonly MethodInfo? StringContainsMethodInfo;
        private static readonly MethodInfo StringEndsWithContainsMethodInfo;
        private static readonly MethodInfo StringStartsWithContainsMethodInfo;
        private static readonly MethodInfo ExpressionLambdaMethodInfo;
        private static readonly MethodInfo? QueryableWhereMethodInfo;
        private static readonly MethodInfo? MySqlDbFuncJsonContainsMethodInfo;
        private static readonly string Space = " ";

        static QueryableExtensions()
        {
            StringContainsMethodInfo = typeof(string).GetMethod(nameof(string.Contains), new[] { typeof(string), typeof(StringComparison) });

            if (StringContainsMethodInfo == null) 
            {
                throw new Exception("未从string上获取到 Contains(string par,StringComparison par)方法");
            }

            StringStartsWithContainsMethodInfo = typeof(string).GetMethods().First(e =>
            {
                if (e.Name != nameof(string.StartsWith))
                {
                    return false;
                }
                ParameterInfo[] parameters = e.GetParameters();

                if (parameters.Length != 2)
                {
                    return false;
                }
                if (parameters[0].ParameterType != typeof(string) && parameters[1].ParameterType != typeof(StringComparison))
                {
                    return false;
                }
                return true;
            });

            StringEndsWithContainsMethodInfo = typeof(string).GetMethods().First(e =>
            {
                if (e.Name != nameof(string.EndsWith))
                {
                    return false;
                }
                ParameterInfo[] parameters = e.GetParameters();

                if (parameters.Length != 2)
                {
                    return false;
                }
                if (parameters[0].ParameterType != typeof(string) && parameters[1].ParameterType != typeof(StringComparison))
                {
                    return false;
                }
                return true;
            });

            ExpressionLambdaMethodInfo = typeof(Expression).GetMethods().First(x => x.Name == "Lambda" && x.ContainsGenericParameters && x.GetParameters().Length == 2);

            QueryableWhereMethodInfo = typeof(Queryable).GetMethods()
            .FirstOrDefault((x) =>
            {
                if (x.Name != "Where")
                {
                    return false;
                }
                ParameterInfo[] parameters = x.GetParameters();
                if (parameters.Length != 2)
                {
                    return false;
                }
                ParameterInfo parameter1 = parameters[1];

                if (parameter1.ParameterType.GenericTypeArguments.Length != 1)
                {
                    return false;
                }
                if (parameter1.ParameterType.ToString() != "System.Linq.Expressions.Expression`1[System.Func`2[TSource,System.Boolean]]")
                {
                    return false;
                }
                return true;
            });

            if (QueryableWhereMethodInfo == null) 
            {
                throw new Exception("未从Queryable上获取到where方法");
            }

            MySqlDbFuncJsonContainsMethodInfo = typeof(MySqlDbFunctions).GetMethod(nameof(MySqlDbFunctions.JsonContains));

        }

        public static IQueryable<TSource> OrderByCustomer<TSource>(this IQueryable<TSource> source, IQueryCustomerSort customerOrderBy)
        {
            if (!customerOrderBy.EnableCustomerSort)
            {
                return source;
            }

            return source.OrderByCustomer(customerOrderBy.SortExpression);
        }

        public static IQueryable<T> OrderByCustomer<T>(this IQueryable<T> source, string sortExpression, bool throwException = false)
        {
            return source.OrderByCustomerInternal(sortExpression, throwException, false);
        }

        public static IQueryable<T> ThenByCustomer<T>(this IQueryable<T> source, string sortExpression, bool throwException = false)
        {
            return source.OrderByCustomerInternal(sortExpression, throwException, true);
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, IDictionary<string, OrderTypes> orderBy, bool throwException = false)
        {
            var sortExpression = orderBy == null || !orderBy.Any()
                ? string.Empty
                : string.Join(",", orderBy.Select(x => $"{x.Key} {x.Value}"));

            return source.OrderByCustomer(sortExpression, throwException);
        }

        public static IQueryable<TSource> Paging<TSource>(this IQueryable<TSource> source, IQueryPaging paging)
        {
            if (!paging.EnablePaging)
            {
                return source;
            }

            int offset = GetValidOffset(paging);
            int limit = GetValidLimit(paging);
            if (offset > 0)
            {
                source = source.Skip(offset);
            }

            source = source.Take(limit);
            return source;
        }

        public static IQueryable<TSource> Where<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, bool applyPredicate)
        {
            if (applyPredicate)
            {
                return source.Where(predicate);
            }

            return source;
        }

        public static IQueryable<TSource> WhereLike<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, string>> expression, string value, StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        {
            if (string.IsNullOrEmpty(value))
            {
                return source;
            }

            value = value.Replace("\\", "\\\\");

            MemberExpression? memberExpression = expression.Body as MemberExpression;

            Type typeSource = typeof(TSource);

            Type funcType = typeof(Func<,>).MakeGenericType(typeSource, typeof(bool));

            MethodInfo lambdaBuilder = ExpressionLambdaMethodInfo.MakeGenericMethod(funcType);

            ParameterExpression parametrExpression = Expression.Parameter(typeSource);
            PropertyInfo? propertyInfo = memberExpression?.Member as PropertyInfo;

            if (propertyInfo == null)
            {
                throw new InvalidOperationException($"为获取到{typeof(TSource).Name}propertyInfo字段属性");
            }
            MemberExpression propertyExpression = Expression.Property(parametrExpression, propertyInfo);
            Expression predicateExpression;
            var comparisonExpression = Expression.Constant(stringComparison);

            if (value.Length >= 2 && value.StartsWith("\"") && value.EndsWith("\""))
            {
                predicateExpression = Expression.Equal(propertyExpression, Expression.Constant(value.Substring(1, value.Length - 2)));
            }
            else if (value.Length >= 2 && value.StartsWith("*") && value.EndsWith("*"))
            {
                predicateExpression = Expression.Call(propertyExpression, StringContainsMethodInfo!, Expression.Constant(value.Substring(1, value.Length - 2)), comparisonExpression);
            }
            else if (value.Length > 1 && value.StartsWith("*") && !value.EndsWith("*"))
            {
                predicateExpression = Expression.Call(propertyExpression, StringEndsWithContainsMethodInfo, Expression.Constant(value.Substring(1)), comparisonExpression);
            }
            else if (value.Length > 1 && value.EndsWith("*") && !value.StartsWith("*"))
            {
                predicateExpression = Expression.Call(propertyExpression, StringStartsWithContainsMethodInfo, Expression.Constant(value.Substring(0, value.Length - 1)), comparisonExpression);
            }
            else
            {
                predicateExpression = Expression.Call(propertyExpression, StringContainsMethodInfo!, Expression.Constant(value), comparisonExpression);
            }

            object? predicateLambda = lambdaBuilder.Invoke(null, new object[] { predicateExpression, new[] { parametrExpression } });

            MethodInfo where = QueryableWhereMethodInfo!.MakeGenericMethod(typeSource);

            return (IQueryable<TSource>)(where.Invoke(null, new[] { source, predicateLambda }) ?? source);
        }

        public static IQueryable<TSource> WhereDateRange<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, DateTime>> expression, DateRange value)
        {
            if (value.From == DateTime.MinValue && (value.To == DateTime.MaxValue || value.To == DateTime.MinValue))
            {
                return source;
            }
            MemberExpression? memberExpression = expression.Body as MemberExpression;

            Type typeSource = typeof(TSource);

            Type funcType = typeof(Func<,>).MakeGenericType(typeSource, typeof(bool));

            MethodInfo lambdaBuilder = ExpressionLambdaMethodInfo.MakeGenericMethod(funcType);

            ParameterExpression parameterExpression = Expression.Parameter(typeSource);
            PropertyInfo? propertyInfo = memberExpression?.Member as PropertyInfo;

            if (propertyInfo == null)
            {
                throw new InvalidOperationException($"为获取到{typeof(TSource).Name}propertyInfo字段属性");
            }

            MemberExpression propertyExpression = Expression.Property(parameterExpression, propertyInfo);

            BinaryExpression? lessThanExpression = null;
            BinaryExpression? greaterThanExpression = null;
            if (value.From != DateTime.MinValue)
            {
                greaterThanExpression = Expression.GreaterThanOrEqual(propertyExpression, Expression.Constant(value.From));
            }
            if (value.To != DateTime.MaxValue)
            {
                lessThanExpression = Expression.LessThanOrEqual(propertyExpression, Expression.Constant(value.To));
            }
            BinaryExpression? finalExpression;
            if (lessThanExpression != null && greaterThanExpression != null)
            {
                finalExpression = Expression.AndAlso(greaterThanExpression, lessThanExpression);
            }
            else if (value.From == value.To)
            {
                finalExpression = Expression.Equal(propertyExpression, Expression.Constant(value.From));
            }
            else
            {
                finalExpression = lessThanExpression ?? greaterThanExpression;
            }

            object? predicateLambda = lambdaBuilder.Invoke(null, new object?[] { finalExpression, new[] { parameterExpression } });

            MethodInfo where = QueryableWhereMethodInfo!.MakeGenericMethod(typeSource);

            return (IQueryable<TSource>)(where.Invoke(null, new[] { source, predicateLambda }) ?? source);
        }

        public static IQueryable<TSource> WhereBetween<TSource, TColumn>(this IQueryable<TSource> source, Expression<Func<TSource, TColumn>> expression, (TColumn StartValue, TColumn EndValue) betweenValue, ByzanBetweenOptions byzanBetweenOptions = ByzanBetweenOptions.ClosedInterval)
        {
            if (betweenValue.StartValue == null && betweenValue.EndValue == null)
            {
                return source;
            }
            MemberExpression? memberExpression = expression.Body as MemberExpression;

            Type typeSource = typeof(TSource);

            Type funcType = typeof(Func<,>).MakeGenericType(typeSource, typeof(bool));

            MethodInfo lambdaBuilder = ExpressionLambdaMethodInfo.MakeGenericMethod(funcType);

            ParameterExpression parameterExpression = Expression.Parameter(typeSource);
            PropertyInfo? propertyInfo = memberExpression?.Member as PropertyInfo;
            if (propertyInfo == null)
            {
                throw new InvalidOperationException($"为获取到{typeof(TSource).Name}propertyInfo字段属性");
            }
            MemberExpression propertyExpression = Expression.Property(parameterExpression, propertyInfo);

            BinaryExpression? lessThanExpression = null;
            BinaryExpression? greaterThanExpression = null;
            if (betweenValue.StartValue != null)
            {
                greaterThanExpression = byzanBetweenOptions == ByzanBetweenOptions.ClosedInterval || byzanBetweenOptions == ByzanBetweenOptions.RightSideOpenInterval ? Expression.GreaterThanOrEqual(propertyExpression, Expression.Constant(betweenValue.StartValue)) : Expression.GreaterThan(propertyExpression, Expression.Constant(betweenValue.StartValue));
            }

            if (betweenValue.EndValue != null)
            {
                lessThanExpression = byzanBetweenOptions == ByzanBetweenOptions.ClosedInterval || byzanBetweenOptions == ByzanBetweenOptions.LeftSideOpenInterval ? Expression.LessThanOrEqual(propertyExpression, Expression.Constant(betweenValue.EndValue)) : Expression.LessThan(propertyExpression, Expression.Constant(betweenValue.EndValue));
            }

            BinaryExpression? finialExpression;
            if (lessThanExpression != null && greaterThanExpression != null)
            {
                finialExpression = Expression.AndAlso(greaterThanExpression, lessThanExpression);
            }
            else
            {
                finialExpression = lessThanExpression ?? greaterThanExpression;
            }

            object? predicateLambda = lambdaBuilder.Invoke(null, new object?[] { finialExpression, new[] { parameterExpression } });

            MethodInfo where = QueryableWhereMethodInfo!.MakeGenericMethod(typeSource);

            return (IQueryable<TSource>)(where.Invoke(null, new[] { source, predicateLambda }) ?? source);
        }

        public static IQueryable<TSource> WhereKeywords<TSource>(this IQueryable<TSource> source, string content, params Expression<Func<TSource, string?>>[] expressions)
        {
            return source.WhereKeywords(content, Space, expressions);
        }

        public static IQueryable<TSource> WhereKeywords<TSource>(this IQueryable<TSource> source, string content, string split = " ", params Expression<Func<TSource, string?>>[] expressions)
        {
            if (string.IsNullOrEmpty(content) || !expressions.Any())
            {
                return source;
            }

            content = content.Replace("\\", "\\\\");

            Type typeSource = typeof(TSource);

            Type funcType = typeof(Func<,>).MakeGenericType(typeSource, typeof(bool));

            MethodInfo lambdaBuilder = ExpressionLambdaMethodInfo.MakeGenericMethod(funcType);

            ParameterExpression parameterExpression = Expression.Parameter(typeSource);
            List<Expression> predicateExpressions = new List<Expression>();
            var values = content.Split(split, StringSplitOptions.RemoveEmptyEntries);

            foreach (var expression in expressions!)
            {
                if (expression.Body.NodeType != ExpressionType.MemberAccess)
                {
                    throw new ArgumentNullException("参数非类型成员");
                }

                MemberExpression? memberExpression = expression.Body as MemberExpression;
                PropertyInfo? propertyInfo = memberExpression?.Member as PropertyInfo;
                var propertyAttribute = propertyInfo?.GetCustomAttribute<ColumnAttribute>();
                if (propertyAttribute != null && propertyAttribute.TypeName == ColumnTypes.Json)
                {
                    throw new ArgumentNullException("暂不支持Json类型数据");
                }

                if (propertyInfo == null)
                {
                    throw new InvalidOperationException($"为获取到{typeof(TSource).Name}propertyInfo字段属性");
                }

                MemberExpression propertyExpression = Expression.Property(parameterExpression, propertyInfo);

                ConstantExpression compareConstantExpression = Expression.Constant(StringComparison.OrdinalIgnoreCase, typeof(StringComparison));
                Expression? predicateExpression = null;

                foreach (var value in values)
                {
                    MethodCallExpression containsExpression = Expression.Call(propertyExpression, StringContainsMethodInfo!, Expression.Constant(value), compareConstantExpression);
                    predicateExpression = (predicateExpression == null) ? containsExpression as Expression : Expression.And(predicateExpression, containsExpression);
                }
                predicateExpressions.Add(predicateExpression!);
            }
            Expression? resultExpression = null;
            foreach (var predicateExpression in predicateExpressions)
            {
                resultExpression = (resultExpression == null) ? predicateExpression : Expression.Or(resultExpression, predicateExpression);
            }

            object? predicateLambda = lambdaBuilder.Invoke(null, new object?[] { resultExpression, new[] { parameterExpression } });

            MethodInfo where = QueryableWhereMethodInfo!.MakeGenericMethod(typeSource);

            return (IQueryable<TSource>)(where.Invoke(null, new[] { source, predicateLambda }) ?? source);
        }

        public static IQueryable<TSource> WhereAnyIn<TSource, TItem>(this IQueryable<TSource> source, TItem[] queryItems, Expression<Func<TSource, string>> expression)
            where TItem : IConvertible
        {
            return source.WhereAnyIn(queryItems, expression, false, true);
        }

        public static IQueryable<TSource> WhereAnyIn<TSource, TItem>(this IQueryable<TSource> source, TItem[] queryItems, Expression<Func<TSource, string>> expression, bool emptyQueryShowItems)
    where TItem : IConvertible
        {
            return source.WhereAnyIn(queryItems, expression, emptyQueryShowItems, true);
        }

        public static IQueryable<TSource> WhereAnyIn<TSource, TItem>(this IQueryable<TSource> source, TItem[] queryItems, Expression<Func<TSource, string>> expression, bool emptyQueryShowItems, bool applyPredicate)
            where TItem : IConvertible
        {
            queryItems ??= new TItem[0];

            if (queryItems.Length == 0 && emptyQueryShowItems)
            {
                return source;
            }

            if (!applyPredicate)
            {
                return source;
            }

            MemberExpression? memberExpression = expression.Body as MemberExpression;

            Type typeSource = typeof(TSource);

            Type funcType = typeof(Func<,>).MakeGenericType(typeSource, typeof(bool));

            MethodInfo lambdaBuilder = ExpressionLambdaMethodInfo.MakeGenericMethod(funcType);

            ParameterExpression parameterExpression = Expression.Parameter(typeSource);
            PropertyInfo? propertyInfo = memberExpression?.Member as PropertyInfo;
            if (propertyInfo == null)
            {
                throw new InvalidOperationException($"为获取到{typeof(TSource).Name}propertyInfo字段属性");
            }
            MemberExpression propertyExpression = Expression.Property(parameterExpression, propertyInfo);

            Expression entityOrCondition = Expression.Equal(Expression.Constant(1), Expression.Constant(2));
            foreach (var item in queryItems)
            {
                var itemJson = JsonSerializer.Serialize(item);
                ConstantExpression itemValue = Expression.Constant(itemJson, typeof(string));
                Expression jsonContains = Expression.Call(null, typeof(MySqlDbFunctions).GetMethod("JsonContains")!, propertyExpression, itemValue);
                entityOrCondition = Expression.Or(entityOrCondition, Expression.Equal(jsonContains, Expression.Constant("1")));
            }

            object? predicateLambda = lambdaBuilder.Invoke(null, new object?[] { entityOrCondition, new[] { parameterExpression } });

            MethodInfo where = QueryableWhereMethodInfo!.MakeGenericMethod(typeSource);

            return (IQueryable<TSource>)(where.Invoke(null, new[] { source, predicateLambda }) ?? source);

        }

        /// <summary>
        /// 根据传入表达式与参数数据进行or组装
        /// </summary>
        /// <param name="sources">原数据</param>
        /// <param name="parParams">参数数据</param>
        /// <param name="expression">自定义匹配表达式</param>
        /// <typeparam name="TSource">entityStore</typeparam>
        /// <typeparam name="TParam">参数数组类型</typeparam>
        /// <returns>IQueryable</returns>
        public static IQueryable<TSource> Contains<TSource, TParam>(this IQueryable<TSource> sources, TParam[] parParams, Expression<Func<TSource, TParam, bool>> expression) // where TSource : EntityBase
        {
            if (!parParams.Any())
            {
                return sources;
            }

            if (expression == null)
            {
                throw new AggregateException("Contains传入的表达不能为NULL!");
            }

            var paramX = Expression.Parameter(typeof(TSource), "x");
            Expression defaultExpression = Expression.Constant(false);

            foreach (var parParam in parParams)
            {
                var treeModifier = new WhereOrExpressionVisitor<TParam>(parParam);
                Expression lambdaExpression = treeModifier.Visit(expression);

                defaultExpression = Expression.OrElse(defaultExpression, Expression.Invoke(lambdaExpression, paramX, Expression.Constant(parParam)));
            }

            var lambda = Expression.Lambda<Func<TSource, bool>>(defaultExpression, paramX);

            return sources.Where(lambda);
        }

        public static int GetValidLimit(this IQueryPaging paging)
        {
            if (!paging.Limit.HasValue || paging.Limit <= 0)
            {
                return paging.DefaultPageSize <= 0 ? 1 : paging.DefaultPageSize;
            }

            int minPageSize = Math.Max(paging.MinPageSize, 1);

            return Math.Max(minPageSize, Math.Min(paging.Limit.Value, paging.MaxPageSize));
        }

        public static int GetValidOffset(this IQueryPaging paging)
        {
            if (!paging.Offset.HasValue)
            {
                return 0;
            }

            return Math.Max(paging.Offset.Value, 0);
        }

        public static IQueryable<TSource> WhereLabels<TSource>(this IQueryable<TSource> linq, IQueryLabels query)
           where TSource : IEntityLabels
        {
            return linq.WhereLabels(query.Labels);
        }

        public static IOrderedQueryable<TSource> OrderByCaseWhen<TSource, TKey>(this IQueryable<TSource> linq, Expression<Func<TSource, TKey>> keySelector, TKey[] orderValues)
        {
            return linq.OrderBy(OrderByCaseWhenExpression(keySelector, orderValues));
        }

        public static IOrderedQueryable<TSource> ThenByCaseWhen<TSource, TKey>(this IOrderedQueryable<TSource> linq, Expression<Func<TSource, TKey>> keySelector, TKey[] orderValues)
        {
            return linq.ThenBy(OrderByCaseWhenExpression(keySelector, orderValues));
        }

        private static Expression<Func<TSource, int>> OrderByCaseWhenExpression<TSource, TKey>(Expression<Func<TSource, TKey>> keySelector, TKey[] orderValues)
        {
            int valueLength = orderValues.Length;
            Dictionary<int, TKey> orderValuesDic = new Dictionary<int, TKey>();
            for (int i = 0; i < valueLength; i++)
            {
                orderValuesDic[i] = orderValues[i];
            }
            var lambdaExpression = orderValuesDic.Aggregate(null as Expression, (condition, dicElement) =>
            {
                var equalExpression = Expression.Equal(keySelector.Body, Expression.Constant(dicElement.Value));
                Expression ifTrue = Expression.Constant(dicElement.Key, typeof(int));
                Expression ifFalse = Expression.Constant(1);
                if (condition == null)
                {
                    ifFalse = Expression.Constant(valueLength, typeof(int));
                }
                else
                {
                    ifFalse = condition;
                }
                var conditionExpression = Expression.Condition(equalExpression, ifTrue, ifFalse);
                return conditionExpression;
            });
            return Expression.Lambda<Func<TSource, int>>(lambdaExpression!, keySelector.Parameters);
        }

        private static IQueryable<T> OrderByCustomerInternal<T>(this IQueryable<T> source, string sortExpression, bool throwException, bool thenBy)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (string.IsNullOrEmpty(sortExpression))
            {
                return source;
            }

            string[] sortElements = sortExpression.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            IQueryable<T> query = source;
            for (int i = 0; i < sortElements.Length; i++)
            {
                string sortElement = sortElements[i];
                if (string.IsNullOrWhiteSpace(sortElement))
                {
                    continue;
                }

                if (thenBy)
                {
                    query = query.ThenbyExpression(sortElement, throwException) ?? source;
                }
                else
                {
                    query = query.OrderbyExpression(sortElement, throwException) ?? source;

                    // 对于多字段的排序表达式，orderby 之后再次循环时调用thenby
                    thenBy = true;
                }
            }

            return query;
        }

        public static IQueryable<TSource> WhereLabels<TSource>(this IQueryable<TSource> linq, IDictionary<string, string>? queryLabels)
      where TSource : IEntityLabels
        {
            if (queryLabels == null || queryLabels.Count == 0)
            {
                return linq;
            }

            foreach (var item in queryLabels)
            {
                if (string.IsNullOrWhiteSpace(item.Value) || item.Value == "*")
                {
                    continue;
                }
                char start = '\0';
                char end = '\0';

                if (item.Value.Length > 0)
                {
                    start = item.Value[0];
                    end = item.Value[item.Value.Length - 1];
                }

                if (start == '*' || end == '*')
                {
                    var searchValue = item.Value.Trim('*');
                    if (string.IsNullOrWhiteSpace(searchValue))
                    {
                        continue;
                    }

                    if (start == '*' && end == '*')
                    {
                        linq = linq.Where(_ => MySqlDbFunctions.JsonUnQuote(MySqlDbFunctions.JsonExtract(_.Labels ?? string.Empty, $"$.{item.Key}")).Contains(searchValue));
                    }
                    else if (start == '*')
                    {
                        linq = linq.Where(_ => MySqlDbFunctions.JsonUnQuote(MySqlDbFunctions.JsonExtract(_.Labels ?? string.Empty, $"$.{item.Key}")).EndsWith(searchValue));
                    }
                    else
                    {
                        linq = linq.Where(_ => MySqlDbFunctions.JsonUnQuote(MySqlDbFunctions.JsonExtract(_.Labels ?? string.Empty, $"$.{item.Key}")).StartsWith(searchValue));
                    }
                }
                else
                {
                    linq = linq.Where(_ => MySqlDbFunctions.JsonUnQuote(MySqlDbFunctions.JsonExtract(_.Labels ?? string.Empty, $"$.{item.Key}")) == item.Value);
                }
            }

            return linq;
        }

        public static IQueryable<T>? ThenbyExpression<T>(this IQueryable<T> source, string sortExpression, bool throwException)
        {
            return source.OrderbyExpressionInternal(sortExpression, throwException, true);
        }

        public static IQueryable<T>? OrderbyExpression<T>(this IQueryable<T> source, string sortExpression, bool throwException)
        {
            return source.OrderbyExpressionInternal(sortExpression, throwException, false);
        }

        private static IQueryable<T>? OrderbyExpressionInternal<T>(this IQueryable<T> source, string sortExpression, bool throwException, bool thenBy)
        {
            string[] parts = sortExpression.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0 || string.IsNullOrWhiteSpace(parts[0]))
            {
                return source;
            }

            bool isDescending = false;
            Type type = typeof(T);
            string propertyName = parts[0].Trim();

            if (parts.Length > 1)
            {
                isDescending = parts[1].Trim().ToLower() == "desc";
            }
            PropertyInfo? prop = null;
            string? childPropertyName = null;
            if (propertyName.Contains("."))
            {
                var posPoint = propertyName.IndexOf(".");
                if (posPoint > 0 || posPoint < propertyName.Length - 1)
                {
                    var parentPropertyName = propertyName.Substring(0, posPoint);
                    childPropertyName = propertyName.Substring(posPoint + 1);
                    prop = type.GetProperty(parentPropertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                }
            }
            else
            {
                prop = type.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            }

            if (prop == null)
            {
                if (throwException)
                {
                    throw new ArgumentException(
                        string.Format("Invalid property '{0}' on type '{1}'", propertyName, type.Name));
                }

                return source;
            }
            object? sortLambda;

            Type funcType = typeof(Func<,>).MakeGenericType(type, prop.PropertyType);

            MethodInfo lambdaBuilder = typeof(Expression).GetMethods()
                .First(x => x.Name == "Lambda" && x.ContainsGenericParameters && x.GetParameters().Length == 2)
                .MakeGenericMethod(funcType);

            ParameterExpression parameter = Expression.Parameter(type);
            MemberExpression propExpress = Expression.Property(parameter, prop);

            if (childPropertyName != null)
            {
                var info = typeof(MySqlDbFunctions).GetMethod(nameof(MySqlDbFunctions.JsonExtract), BindingFlags.Static | BindingFlags.Public);
                var callExpress = Expression.Call(info, propExpress, Expression.Constant($"$.{childPropertyName}"));
                sortLambda = lambdaBuilder.Invoke(null, new object[] { callExpress, new[] { parameter } });
            }
            else
            {
                sortLambda = lambdaBuilder.Invoke(null, new object[] { propExpress, new[] { parameter } });
            }

            var orderByMethodName = nameof(Queryable.OrderBy);

            if (thenBy)
            {
                orderByMethodName = isDescending ? nameof(Queryable.ThenByDescending) : nameof(Queryable.ThenBy);
            }
            else
            {
                orderByMethodName = isDescending ? nameof(Queryable.OrderByDescending) : nameof(Queryable.OrderBy);
            }

            MethodInfo sorter = typeof(Queryable).GetMethods()
                .FirstOrDefault(
                    x => x.Name == orderByMethodName && x.GetParameters().Length == 2)!
                .MakeGenericMethod(type, prop.PropertyType);

            return (IQueryable<T>?)sorter.Invoke(null, new[] { source, sortLambda });
        }
    }
}
