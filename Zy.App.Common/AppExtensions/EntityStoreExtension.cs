using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Zy.App.Common.AppExtensions
{
    public static class EntityStoreExtension
    {
        public static IQueryable<TSource> WhereLike<TSource>(this IQueryable<TSource> entities, Expression<Func<TSource, bool>> predicate, bool isSearce)
        {
            if (isSearce)
            {
                return entities.Where(predicate);
            }
            return entities;
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
