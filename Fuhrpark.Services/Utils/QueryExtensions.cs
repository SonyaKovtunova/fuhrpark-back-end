using Fuhrpark.Enums;
using Fuhrpark.Models;
using Fuhrpark.Services.Contracts.Dtos;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Fuhrpark.Services.Utils
{
    public static class QueryExtensions
    {
        public static IQueryable<T> Filter<T>(this IQueryable<T> query, IEnumerable<SearchAttributeDto> searchAttributes)
        {
            var predicate = PredicateBuilder.New<T>(true);

            var properties = typeof(T).GetProperties();

            foreach (var searchAttribute in searchAttributes)
            {
                var property = properties.FirstOrDefault(x => x.Name.Equals(searchAttribute.CarField.ToString()));

                switch (searchAttribute.ComparingType)
                {
                    case ComparingType.Max:
                        predicate = predicate.And(LessThanOrEqual<T>(property, searchAttribute.Data));
                        break;
                    case ComparingType.Min:
                        predicate = predicate.And(GreaterThanOrEqual<T>(property, searchAttribute.Data));
                        break;
                    case ComparingType.Contains:
                        predicate = predicate.And(Contains<T>(property, searchAttribute.Data));
                        break;
                    case ComparingType.Equal:
                        predicate = predicate.And(Equal<T>(property, searchAttribute.Data));
                        break;
                    default:
                        break;
                }
            }

            return query.Where(predicate);
        }

        private static Expression<Func<T, bool>> Equal<T>(PropertyInfo property, object data)
        {
            var parameterExpression = Expression.Parameter(typeof(T), property.Name);
            var propertyExpression = Expression.Property(parameterExpression, property.Name);

            ConstantExpression searchDataExpression;
            BinaryExpression equalExpression;

            if (IsNullableType(propertyExpression.Type))
            {
                var withValueExpression = Expression.NotEqual(propertyExpression, Expression.Constant(null, propertyExpression.Type));
                var valueExpression = Expression.Property(propertyExpression, "Value");

                searchDataExpression = Expression.Constant(Convert.ChangeType(data, valueExpression.Type));
                equalExpression = Expression.Equal(valueExpression, searchDataExpression);

                return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(withValueExpression, equalExpression), parameterExpression);
            }

            searchDataExpression = Expression.Constant(Convert.ChangeType(data, propertyExpression.Type));
            equalExpression = Expression.Equal(propertyExpression, searchDataExpression);
            return Expression.Lambda<Func<T, bool>>(equalExpression, parameterExpression);
        }

        private static Expression<Func<T, bool>> Contains<T>(PropertyInfo property, object data)
        {
            var parameterExpression = Expression.Parameter(typeof(T), property.Name);
            var propertyExpression = Expression.Property(parameterExpression, property.Name);
            var propertyAccess = Expression.MakeMemberAccess(parameterExpression, property);

            ConstantExpression searchDataExpression;
            Expression containsExpression;

            if (IsNullableType(propertyExpression.Type))
            {
                var withValueExpression = Expression.NotEqual(propertyExpression, Expression.Constant(null, propertyExpression.Type));
                var valueExpression = Expression.Property(propertyExpression, "Value");
                
                searchDataExpression = Expression.Constant(Convert.ChangeType(data, valueExpression.Type));
                containsExpression = Expression.Call(propertyAccess, "Contains", null, searchDataExpression);

                return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(withValueExpression, containsExpression), parameterExpression);
            }

            searchDataExpression = Expression.Constant(Convert.ChangeType(data, propertyExpression.Type));
            containsExpression = Expression.Call(propertyAccess, "Contains", null, searchDataExpression);
            return Expression.Lambda<Func<T, bool>>(containsExpression, parameterExpression);
        }

        private static Expression<Func<T, bool>> LessThanOrEqual<T>(PropertyInfo property, object data)
        {
            var parameterExpression = Expression.Parameter(typeof(T), property.Name);
            var propertyExpression = Expression.Property(parameterExpression, property.Name);

            ConstantExpression searchDataExpression;
            BinaryExpression lessExpression;

            if (IsNullableType(propertyExpression.Type))
            {
                var withValueExpression = Expression.NotEqual(propertyExpression, Expression.Constant(null, propertyExpression.Type));
                var valueExpression = Expression.Property(propertyExpression, "Value");

                searchDataExpression = Expression.Constant(Convert.ChangeType(data, valueExpression.Type));
                lessExpression = Expression.LessThanOrEqual(valueExpression, searchDataExpression);

                return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(withValueExpression, lessExpression), parameterExpression);
            }

            searchDataExpression = Expression.Constant(Convert.ChangeType(data, propertyExpression.Type));
            lessExpression = Expression.LessThanOrEqual(propertyExpression, searchDataExpression);
            return Expression.Lambda<Func<T, bool>>(lessExpression, parameterExpression);
        }

        private static Expression<Func<T, bool>> GreaterThanOrEqual<T>(PropertyInfo property, object data)
        {
            var parameterExpression = Expression.Parameter(typeof(T), property.Name);
            var propertyExpression = Expression.Property(parameterExpression, property.Name);

            ConstantExpression searchDataExpression;
            BinaryExpression greaterExpression;

            if (IsNullableType(propertyExpression.Type))
            {
                var withValueExpression = Expression.NotEqual(propertyExpression, Expression.Constant(null, propertyExpression.Type));
                var valueExpression = Expression.Property(propertyExpression, "Value");

                searchDataExpression = Expression.Constant(Convert.ChangeType(data, valueExpression.Type));
                greaterExpression = Expression.GreaterThanOrEqual(valueExpression, searchDataExpression);

                return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(withValueExpression, greaterExpression), parameterExpression);
            }

            searchDataExpression = Expression.Constant(Convert.ChangeType(data, propertyExpression.Type));
            greaterExpression = Expression.GreaterThanOrEqual(propertyExpression, searchDataExpression);
            return Expression.Lambda<Func<T, bool>>(greaterExpression, parameterExpression);
        }

        private static bool IsNullableType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
    }
}
