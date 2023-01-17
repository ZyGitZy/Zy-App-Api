using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Zy.App.Common.AppExtensions
{
    public class WhereOrExpressionVisitor<T> : ExpressionVisitor
    {
        private T _t;

        public WhereOrExpressionVisitor(T t)
        {
            this._t = t;
        }

        public override Expression Visit(Expression node)
        {
            if (node.NodeType != ExpressionType.MemberAccess)
            {
                return base.Visit(node) ?? throw new InvalidOperationException("Contains Visit迭代返回NULL");
            }

            if (!(node is MemberExpression memberExpression))
            {
                return base.Visit(node) ?? throw new InvalidOperationException("Contains Visit迭代返回NULL");
            }

            var memberInfo = memberExpression.Member;
            var name = memberInfo.Name;
            var declaringType = memberInfo.DeclaringType;

            if (declaringType != typeof(T))
            {
                return base.Visit(node) ?? throw new InvalidOperationException("Contains Visit迭代返回NULL");
            }

            var prop = typeof(T).GetProperty(name);
            if (prop is null)
            {
                throw new AggregateException("Contains 传入的表达树节点有误，未匹配到对应的属性名称!");
            }

            var value = prop.GetValue(this._t);
            if (value is null)
            {
                throw new AggregateException("Contains 传入的表达树节点有误，未匹配到对应的属性名称!");
            }

            var type = value.GetType();

            return Expression.Constant(value, type);
        }
    }
}
