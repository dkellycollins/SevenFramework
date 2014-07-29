// ComputerAlgebra Library
//
// Copyright © Medvedev Igor, Okulovsky Yuri, Borcheninov Jaroslav, 2013
// imedvedev3@gmail.com, yuri.okulovsky@gmail.com, yariksuperman@gmail.com
//

using System;
using System.Linq.Expressions;
using Seven.Mathematics.Symbolics.Tree;

namespace Seven.Mathematics.Symbolics.Tools
{
    class Tree2Expression
    {
        public static Expression Parse(INode node)
        {
            try
            {
                return GetTree(node);
            } catch(ParseException e)
            {
                throw new ParseException(string.Format("Failed to parse tree {0} into a expression", node), e);
            }
        }

        private static Expression GetTree(INode node)
        {
            if (node is BinaryOp)
            {
                var operand = (BinaryOp)node;
                if(operand is Arithmetic_Operators.Plus)
                    return Expression.Add(GetTree(operand.Children[0]), GetTree(operand.Children[1]));
                if(operand is Arithmetic_Operators.Minus)
                    return Expression.Subtract(GetTree(operand.Children[0]), GetTree(operand.Children[1]));
                if(operand is Arithmetic_Operators.Product)
                    return Expression.Multiply(GetTree(operand.Children[0]), GetTree(operand.Children[1]));
                if(operand is Arithmetic_Operators.Divide)
                    return Expression.Divide(GetTree(operand.Children[0]), GetTree(operand.Children[1]));
                if(operand is Arithmetic_Operators.Pow)
                    return Expression.Power(GetTree(operand.Children[0]), GetTree(operand.Children[1]));                
            }
            if (node is UnaryOp)
            {
                var operand = (UnaryOp)node;
                if (operand is Arithmetic_Operators.Negate)
                    return Expression.Negate(GetTree(operand.Children[0]));
                if (operand is Arithmetic_Operators.Sin)
                    return Expression.Call(typeof(Math).GetMethod("Sin"), GetTree(operand.Children[0]));
                if (operand is Arithmetic_Operators.Cos)
                    return Expression.Call(typeof(Math).GetMethod("Cos"), GetTree(operand.Children[0]));
                if (operand is Arithmetic_Operators.Tan)
                    return Expression.Call(typeof(Math).GetMethod("Tan"), GetTree(operand.Children[0]));                
                if (operand is Arithmetic_Operators.Ln)
                    return Expression.Call(typeof(Math).GetMethod("Log", new Type[] { typeof(double) }), GetTree(operand.Children[0])); 
            }
            if (node is VariableNode)
            {
                var var = (VariableNode)node;
                return Expression.Parameter(var.Type, var.ToString());
            }
            if (node is Constant)
            {
                var _const = (Constant)node;
                return Expression.Constant(Double.Parse(_const.ToString()), _const.Type);
            }
            throw new ParseException(string.Format("Unexpected argument '{0}'", node));
        }
    }
}
