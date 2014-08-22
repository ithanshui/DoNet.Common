using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.CodeDom.Compiler;

namespace DoNet.Common.LinqExpression
{
    /// <summary>
    /// 表达式辅助类
    /// </summary>
    public class Helper
    {
        /// <summary>
        /// 转换表达式为sql条件
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="fun">表达式</param>
        /// <returns>查询条件</returns>
        public static string DserExpressionToWhere<T>(Expression<Func<T, bool>> fun)
        {
            var be = (BinaryExpression)fun.Body;
            var where = BinarExpressionProvider(be.Left, be.Right, be.NodeType);
            return where;
        }

        // 条件表达式解析。
        public static string BinarExpressionProvider(Expression left, Expression right, ExpressionType type)
        {
            string sb = "(";
            //先处理左边
            string tmpStr = ExpressionRouter(left);
            if (tmpStr == "null")
            {
                Expression temp = left;
                left = right;
                right = temp;
            }
            sb += ExpressionRouter(left);
            sb += ExpressionTypeCast(type);
            //再处理右边
            tmpStr = ExpressionRouter(right);
            if (tmpStr == "null")
            {
                if (sb.EndsWith(" ="))
                    sb = sb.Substring(0, sb.Length - 2) + " is null";
                else if (sb.EndsWith("<>"))
                    sb = sb.Substring(0, sb.Length - 2) + " is not null";
            }
            else
                sb += tmpStr;
            return sb += ")";
        }
        // 表达式路由。
        static string ExpressionRouter(Expression exp)
        {
            string sb = string.Empty;
            if (exp is BinaryExpression)
            {
                BinaryExpression be = ((BinaryExpression)exp);
                return BinarExpressionProvider(be.Left, be.Right, be.NodeType);
            }
            else if (exp is MemberExpression)
            {
                if (!exp.ToString().StartsWith("value("))
                {
                    MemberExpression me = ((MemberExpression)exp);
                    return me.Member.Name;
                }
                else
                {
                    var result = Expression.Lambda(exp).Compile().DynamicInvoke();
                    if (result == null)
                        return "null";
                    if (result is ValueType)
                        return result.ToString();
                    else if (result is string || result is DateTime || result is char)
                        return string.Format("'{0}'", (result as string).Replace("'", "''"));
                }
            }
            else if (exp is NewArrayExpression)
            {
                NewArrayExpression ae = ((NewArrayExpression)exp);
                StringBuilder tmpstr = new StringBuilder();
                foreach (Expression ex in ae.Expressions)
                {
                    tmpstr.Append(ExpressionRouter(ex));
                    tmpstr.Append(",");
                }
                return tmpstr.ToString(0, tmpstr.Length - 1);
            }
            else if (exp is MethodCallExpression)
            {
                MethodCallExpression mce = (MethodCallExpression)exp;
                if (mce.Method.Name == "Like")
                    return string.Format("({0} like {1})", ExpressionRouter(mce.Arguments[0]), ExpressionRouter(mce.Arguments[1]));
                else if (mce.Method.Name == "NotLike")
                    return string.Format("({0} Not like {1})", ExpressionRouter(mce.Arguments[0]), ExpressionRouter(mce.Arguments[1]));
                else if (mce.Method.Name == "In")
                    return string.Format("{0} In ({1})", ExpressionRouter(mce.Arguments[0]), ExpressionRouter(mce.Arguments[1]));
                else if (mce.Method.Name == "NotIn")
                    return string.Format("{0} Not In ({1})", ExpressionRouter(mce.Arguments[0]), ExpressionRouter(mce.Arguments[1]));
            }
            else if (exp is ConstantExpression)
            {
                ConstantExpression ce = ((ConstantExpression)exp);
                if (ce.Value == null)
                    return "null";
                else if (ce.Value is ValueType)
                    return ce.Value.ToString();
                else if (ce.Value is string || ce.Value is DateTime || ce.Value is char)
                    return string.Format("'{0}'", (ce.Value as string).Replace("'", "''"));
            }
            else if (exp is UnaryExpression)
            {
                UnaryExpression ue = ((UnaryExpression)exp);
                return ExpressionRouter(ue.Operand);
            }
            return null;
        }
        // 表达式类型转换。
        static string ExpressionTypeCast(ExpressionType type)
        {
            switch (type)
            {
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                    return " AND ";
                case ExpressionType.Equal:
                    return " =";
                case ExpressionType.GreaterThan:
                    return " >";
                case ExpressionType.GreaterThanOrEqual:
                    return ">=";
                case ExpressionType.LessThan:
                    return "<";
                case ExpressionType.LessThanOrEqual:
                    return "<=";
                case ExpressionType.NotEqual:
                    return "<>";
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    return " Or ";
                case ExpressionType.Add:
                case ExpressionType.AddChecked:
                    return "+";
                case ExpressionType.Subtract:
                case ExpressionType.SubtractChecked:
                    return "-";
                case ExpressionType.Divide:
                    return "/";
                case ExpressionType.Multiply:
                case ExpressionType.MultiplyChecked:
                    return "*";
                default:
                    return null;
            }

        }
    }
}
