using Core.SimpleTemp.Common.FilterExpression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Core.SimpleTemp.Common
{
    public static class ExpressionExtension
    {
        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            var binaryExpression = Expression.Or(expr1.Body, invokedExpr);
            return Expression.Lambda<Func<T, bool>>(binaryExpression, expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var invocationExpression = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            var binaryExpression = Expression.And(expr1.Body, invocationExpression);
            return Expression.Lambda<Func<T, bool>>(binaryExpression, expr1.Parameters);
        }

        public static Expression<Func<T, bool>> GetFilterExpression<T>(List<FilterModel> filterConditionList)
        {
            Expression<Func<T, bool>> condition = null;
            try
            {
                if (filterConditionList != null && filterConditionList.Count > 0)
                {
                    foreach (FilterModel filterCondition in filterConditionList)
                    {
                        if (filterCondition.Value == null)
                            continue;
                        Expression<Func<T, bool>> tempCondition = CreateLambda<T>(filterCondition);
                        if (condition == null)
                        {
                            condition = tempCondition;
                        }
                        else
                        {
                            if ("AND".Equals(filterCondition.Logic, StringComparison.CurrentCultureIgnoreCase))
                            {
                                condition = condition.And(tempCondition);
                            }
                            else
                            {
                                condition = condition.Or(tempCondition);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return condition;
        }



        public static Expression<Func<T, bool>> CreateLambda<T>(FilterModel filterCondition)
        {
            var parameter = Expression.Parameter(typeof(T), "p");//创建参数i
            var constant = Expression.Constant(filterCondition.Value);//创建常数
            //MemberExpression
            Expression member = GetParameter(parameter, filterCondition.Field);


            if ("=".Equals(filterCondition.Action))
            {
                return Expression.Lambda<Func<T, bool>>(Expression.Equal(member, constant), parameter);
            }
            else if ("!=".Equals(filterCondition.Action))
            {
                return Expression.Lambda<Func<T, bool>>(Expression.NotEqual(member, constant), parameter);
            }
            else if (">".Equals(filterCondition.Action))
            {
                return Expression.Lambda<Func<T, bool>>(Expression.GreaterThan(member, constant), parameter);
            }
            else if ("<".Equals(filterCondition.Action))
            {
                return Expression.Lambda<Func<T, bool>>(Expression.LessThan(member, constant), parameter);
            }
            else if (">=".Equals(filterCondition.Action))
            {
                return Expression.Lambda<Func<T, bool>>(Expression.GreaterThanOrEqual(member, constant), parameter);
            }
            else if ("<=".Equals(filterCondition.Action))
            {
                return Expression.Lambda<Func<T, bool>>(Expression.LessThanOrEqual(member, constant), parameter);
            }
            else if ("in".Equals(filterCondition.Action))
            {
                if (filterCondition.Value.GetType().IsGenericType)//判断是泛型类即为数组
                    //数组情况处理  类似于Sql In（）
                    return GetMethodAtrayInExpression<T>(filterCondition, parameter);
                else
                    //字符串情况处理 类似于Sql %Like%
                    return GetExpressionWithMethod<T>("Contains", filterCondition);
            }
            else if ("out".Equals(filterCondition.Action) && "1".Equals(filterCondition.DataType))
            {
                return GetExpressionWithoutMethod<T>("Contains", filterCondition);
            }
            else
            {
                return null;
            }
        }
        public static Expression<Func<T, bool>> GetExpressionWithMethod<T>(string methodName, FilterModel filterCondition)
        {
            ParameterExpression parameterExpression = Expression.Parameter(typeof(T), "p");
            MethodCallExpression methodExpression = GetMethodExpression(methodName, filterCondition.Field, filterCondition.Value, parameterExpression);
            return Expression.Lambda<Func<T, bool>>(methodExpression, parameterExpression);
        }

        public static Expression<Func<T, bool>> GetExpressionWithoutMethod<T>(string methodName, FilterModel filterCondition)
        {
            ParameterExpression parameterExpression = Expression.Parameter(typeof(T), "p");
            MethodCallExpression methodExpression = GetMethodExpression(methodName, filterCondition.Field, filterCondition.Value, parameterExpression);
            var notMethodExpression = Expression.Not(methodExpression);
            return Expression.Lambda<Func<T, bool>>(notMethodExpression, parameterExpression);
        }

        /// <summary>
        /// 生成类似于p=>p.values.Contains("xxx");的lambda表达式
        /// parameterExpression标识p，propertyName表示values，propertyValue表示"xxx",methodName表示Contains
        /// 仅处理p的属性类型为string这种情况
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        /// <param name="parameterExpression"></param>
        /// <returns></returns>
        private static MethodCallExpression GetMethodExpression(string methodName, string propertyName, object propertyValue, ParameterExpression parameterExpression)
        {
            var parameter = parameterExpression;//创建参数i
            var constant = Expression.Constant(propertyValue);//创建常数
            Expression member = GetParameter(parameterExpression, propertyName);
            //var propertyExpression = Expression.Property(parameterExpression, propertyName);
            MethodInfo method = typeof(string).GetMethod(methodName, new[] { propertyValue.GetType() });
            var someValue = Expression.Constant(propertyValue, propertyValue.GetType());
            return Expression.Call(member, method, someValue);
        }

        /// <summary>
        /// 处理FilterModel.Value为数组情况
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="ListT"></typeparam>
        /// <param name="filterCondition"></param>
        /// <param name="parameterExpression"></param>
        /// <returns></returns>
        //public static Expression<Func<T, bool>> GetMethodAtrayInExpression<T, ListT>(FilterModel filterCondition, ParameterExpression parameterExpression)
        public static Expression<Func<T, bool>> GetMethodAtrayInExpression<T>(FilterModel filterCondition, ParameterExpression parameterExpression)
        {
            var parameter = parameterExpression;//创建参数i
            var constant = Expression.Constant(filterCondition.Value);//创建常数
            Expression member = GetParameter(parameter, filterCondition.Field);

            MethodInfo method = filterCondition.Value.GetType().GetMethod("Contains");
            var someValue = Expression.Constant(filterCondition.Value, filterCondition.Value.GetType());
            var callExp = Expression.Call(someValue, method, member);
            return Expression.Lambda<Func<T, bool>>(callExp, parameter);
        }

        /// <summary>
        /// 根据字段名称已字符“.”分割规则 最深层MemberExpression
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private static Expression GetParameter(ParameterExpression parameter, string propertyName)
        {
            MemberExpression member = null;
            //分割属性
            var conlumnLevel = propertyName.Split('.');
            int i = 0;
            MemberExpression curr = null;
            foreach (var item in conlumnLevel)
            {
                if (i == 0)
                    curr = Expression.PropertyOrField(parameter, item);
                else
                    curr = Expression.PropertyOrField(curr, item);
                member = curr;
                i++;
            }
            return member;
        }

    }
}
