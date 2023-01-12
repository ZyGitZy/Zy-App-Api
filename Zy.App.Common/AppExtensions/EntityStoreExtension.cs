using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Interfaces;

namespace Zy.App.Common.AppExtensions
{
    public static class EntityStoreExtension
    {
        private static readonly MethodInfo? StringContainsMethodInfo;

        private static readonly MethodInfo StringEndsWithContainsMethodInfo;

        private static readonly MethodInfo StringStartsWithContainsMethodInfo;

        private static readonly MethodInfo ExpressionLambdaMethodInfo;

        private static readonly MethodInfo? QueryableWhereMethodInfo;

        static EntityStoreExtension()
        {
            StringContainsMethodInfo = typeof(string).GetMethod("Contains", new Type[1] { typeof(string) });
            StringStartsWithContainsMethodInfo = typeof(string).GetMethods().First(delegate (MethodInfo e)
            {
                if (e.Name != "StartsWith")
                {
                    return false;
                }

                ParameterInfo[] parameters3 = e.GetParameters();
                if (parameters3.Length != 1)
                {
                    return false;
                }

                return (!(parameters3[0].ParameterType != typeof(string))) ? true : false;
            });
            StringEndsWithContainsMethodInfo = typeof(string).GetMethods().First(delegate (MethodInfo e)
            {
                if (e.Name != "EndsWith")
                {
                    return false;
                }

                ParameterInfo[] parameters2 = e.GetParameters();
                if (parameters2.Length != 1)
                {
                    return false;
                }

                return (!(parameters2[0].ParameterType != typeof(string))) ? true : false;
            });
            ExpressionLambdaMethodInfo = typeof(Expression).GetMethods().First((MethodInfo x) => x.Name == "Lambda" && x.ContainsGenericParameters && x.GetParameters().Length == 2);
            QueryableWhereMethodInfo = typeof(Queryable).GetMethods().FirstOrDefault(delegate (MethodInfo x)
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

                ParameterInfo parameterInfo = parameters[1];
                if (parameterInfo.ParameterType.GenericTypeArguments.Length != 1)
                {
                    return false;
                }

                return (!(parameterInfo.ParameterType.ToString() != "System.Linq.Expressions.Expression`1[System.Func`2[TSource,System.Boolean]]")) ? true : false;
            });
        }

        public static IQueryable<TSource> WhereLike<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, string>> expression, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return source;
            }

            value = value.Replace("\\", "\\\\");

            MemberExpression? memberExpression = expression.Body as MemberExpression;

            Type typeFromHandle = typeof(TSource);

            Type type = typeof(Func<,>).MakeGenericType(typeFromHandle, typeof(bool));

            MethodInfo methodInfo = ExpressionLambdaMethodInfo.MakeGenericMethod(type);

            ParameterExpression parameterExpression = Expression.Parameter(typeFromHandle);

            PropertyInfo? property = memberExpression?.Member as PropertyInfo;

            MemberExpression memberExpression2 = Expression.Property(parameterExpression, property!);

            if (StringContainsMethodInfo == null)
            {
                throw new Exception("StringContainsMethodInfo初始化失败");
            }

            Expression expression2 = ((value.Length >= 2 &&
                ((value.StartsWith("\"") && value.EndsWith("\"")) || (value.StartsWith("^") && value.EndsWith("$"))))
                ? Expression.Equal(memberExpression2, Expression.Constant(value.Substring(1, value.Length - 2)))
                : (value.Length > 1 && value.StartsWith("^")) ?
                Expression.Call(memberExpression2, StringStartsWithContainsMethodInfo,
                Expression.Constant(value.Substring(1))) : ((value.Length <= 1 || !value.EndsWith("$")) ?
                Expression.Call(memberExpression2, StringContainsMethodInfo, Expression.Constant(value))
                : Expression.Call(memberExpression2, StringEndsWithContainsMethodInfo, Expression.Constant(value.Substring(0, value.Length - 1)))));

            object? obj = methodInfo.Invoke(null, new object[2]
            {
                expression2,
                new ParameterExpression[1] { parameterExpression }
            });

            if (obj == null || QueryableWhereMethodInfo == null)
            {
                throw new Exception("表达式解析失败");
            }

            var exp = QueryableWhereMethodInfo.MakeGenericMethod(typeFromHandle).Invoke(null, new object[2] { source, obj });

            if (exp == null)
            {
                return new List<TSource>().AsQueryable();
            }

            return (IQueryable<TSource>)exp;
        }

        public static IQueryable<TSource> Paging<TSource>(this IQueryable<TSource> source, IQueryPaging queryPaging)
        {
            if (!queryPaging.EnablePaging)
            {
                return source;
            }

            int offset = queryPaging.GetValidOffset();
            int limt = queryPaging.GetValidLimit();

            if (offset > 0)
            {
                source = source.Skip(offset);
            }

            source = source.Take(limt);

            return source;
        }

        public static IQueryable<T> OrderByCustomer<T>(this IQueryable<T> source, string sortExpression, bool throwException = false)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (string.IsNullOrEmpty(sortExpression))
            {
                return source;
            }

            string[] array = sortExpression.Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            IQueryable<T> queryable = source;
            for (int num = array.Length - 1; num >= 0; num--)
            {
                string text = array[num];
                if (!string.IsNullOrWhiteSpace(text))
                {
                    queryable = queryable.OrderByCustomerInternal(text, throwException);
                }
            }

            return queryable;
        }

        private static IQueryable<T> OrderByCustomerInternal<T>(this IQueryable<T> source, string sortExpression, bool throwException)
        {
            string[] array = sortExpression.Split(new char[1] { ' ' });
            if (array.Length == 0 || string.IsNullOrWhiteSpace(array[0]))
            {
                return source;
            }

            bool isDescending = false;
            Type typeFromHandle = typeof(T);
            string text = array[0];
            if (array.Length > 1)
            {
                isDescending = array[1].Trim().ToLower() == "desc";
            }

            PropertyInfo property = typeFromHandle.GetProperty(text);
            if (property == null)
            {
                if (throwException)
                {
                    throw new ArgumentException($"No property '{text}' on type '{typeFromHandle.Name}'");
                }

                return source;
            }

            Type type = typeof(Func<,>).MakeGenericType(typeFromHandle, property.PropertyType);
            MethodInfo methodInfo = typeof(Expression).GetMethods().First((MethodInfo x) => x.Name == "Lambda" && x.ContainsGenericParameters && x.GetParameters().Length == 2).MakeGenericMethod(type);
            ParameterExpression parameterExpression = Expression.Parameter(typeFromHandle);
            MemberExpression memberExpression = Expression.Property(parameterExpression, property);
            object obj = methodInfo.Invoke(null, new object[2]
            {
                memberExpression,
                new ParameterExpression[1] { parameterExpression }
            });
            return (IQueryable<T>)typeof(Queryable).GetMethods().FirstOrDefault((MethodInfo x) => x.Name == (isDescending ? "OrderByDescending" : "OrderBy") && x.GetParameters().Length == 2).MakeGenericMethod(typeFromHandle, property.PropertyType)
                .Invoke(null, new object[2] { source, obj });
        }
    }
}
