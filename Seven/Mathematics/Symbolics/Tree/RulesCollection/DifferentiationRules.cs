// ComputerAlgebra Library
//
// Copyright © Medvedev Igor, Okulovsky Yuri, Borcheninov Jaroslav, 2013
// imedvedev3@gmail.com, yuri.okulovsky@gmail.com, yariksuperman@gmail.com
//

using System.Collections.Generic;
using Seven.Mathematics.Symbolics.Rules;
using Seven.Mathematics.Symbolics.Tree;

namespace Seven.Mathematics.Symbolics.RulesCollection
{
    public partial class DifferentiationRules : SelectClauseWriter
    {
        public static IEnumerable<Rule> Get()
        {
            yield return Rule
               .New("d(-U)/dx", StdTag.Differentiation, StdTag.Algebraic)
               .Select(AnyA[ChildB[ChildC], ChildD])
               .Where<Differentiation.Dif<double>, Arithmetic_Operators.Negate<double>, INode, VariableNode>()
               .Mod(z => z.A.Replace(new Arithmetic_Operators.Negate<double>(new Differentiation.Dif<double>(z.C.Node, z.D.Node))));

            yield return Rule
                .New("d(U+V)/dx", StdTag.Differentiation, StdTag.Algebraic)
                .Select(AnyA[ChildB[ChildC, ChildD], ChildE])
                .Where<Differentiation.Dif<double>, Arithmetic_Operators.Plus<double>, INode, INode, VariableNode>()
                .Mod(z => z.A.Replace(new Arithmetic_Operators.Plus<double>(new Differentiation.Dif<double>(z.C.Node, z.E.Node), new Differentiation.Dif<double>(z.D.Node, (VariableNode)z.E.Node.Clone()))));

            yield return Rule
                .New("d(U-V)/dx", StdTag.Differentiation, StdTag.Algebraic)
                .Select(AnyA[ChildB[ChildC, ChildD], ChildE])
                .Where<Differentiation.Dif<double>, Arithmetic_Operators.Minus<double>, INode, INode, VariableNode>()
                .Mod(z => z.A.Replace(new Arithmetic_Operators.Minus<double>(new Differentiation.Dif<double>(z.C.Node, z.E.Node), new Differentiation.Dif<double>(z.D.Node, (VariableNode)z.E.Node.Clone()))));

            yield return Rule
                .New("d(U*V)/dx", StdTag.Differentiation, StdTag.Algebraic)
                .Select(AnyA[ChildB[ChildC, ChildD], ChildE])
                .Where<Differentiation.Dif<double>, Arithmetic_Operators.Product<double>, INode, INode, VariableNode>()
                .Mod(z => z.A.Replace(new Arithmetic_Operators.Plus<double>(
                    new Arithmetic_Operators.Product<double>(new Differentiation.Dif<double>(z.C.Node, z.E.Node), z.D.Node),
                    new Arithmetic_Operators.Product<double>(new Differentiation.Dif<double>((INode)z.D.Node.Clone(), (VariableNode)z.E.Node.Clone()), (INode)z.C.Node.Clone()))));

            yield return Rule
                .New("d(U/V)/dx", StdTag.Differentiation, StdTag.Algebraic)
                .Select(AnyA[ChildB[ChildC, ChildD], ChildE])
                .Where<Differentiation.Dif<double>, Arithmetic_Operators.Divide<double>, INode, INode, VariableNode>()
                .Mod(z => z.A.Replace(new Arithmetic_Operators.Divide<double>(
                        new Arithmetic_Operators.Minus<double>(
                            new Arithmetic_Operators.Product<double>(
                                new Differentiation.Dif<double>(z.C.Node, z.E.Node), z.D.Node),
                            new Arithmetic_Operators.Product<double>(
                                new Differentiation.Dif<double>((INode)z.D.Node.Clone(), (VariableNode)z.E.Node.Clone()), (INode)z.C.Node.Clone())),
                        new Arithmetic_Operators.Pow<double>((INode)z.D.Node.Clone(), Constant.Double(2.0)))));

            yield return Rule
                .New("d(U^c)/dx", StdTag.Differentiation, StdTag.Algebraic)
                .Select(AnyA[ChildB[C, D], ChildE])
                .Where<Differentiation.Dif<double>, Arithmetic_Operators.Pow<double>, INode, Constant<double>, VariableNode>()
                .Mod(z => z.A.Replace(new Arithmetic_Operators.Product<double>(new Arithmetic_Operators.Product<double>(
                    Constant.Double(z.D.Node.Value),
                    new Arithmetic_Operators.Pow<double>(z.C.Node, Constant.Double(z.D.Node.Value - 1))), new Differentiation.Dif<double>((INode)z.C.Node.Clone(), (VariableNode)z.E.Node.Clone()))));

            yield return Rule
               .New("d(U^V)/dx", StdTag.Differentiation, StdTag.Algebraic)
               .Select(AnyA[ChildB[ChildC, ChildD], ChildE])
               .Where<Differentiation.Dif<double>, Arithmetic_Operators.Pow<double>, INode, INode, VariableNode>()
               .Mod(z => z.A.Replace(new Arithmetic_Operators.Product<double>(
                   new Arithmetic_Operators.Pow<double>(z.C.Node, z.D.Node),
                   new Arithmetic_Operators.Plus<double>(
                       new Arithmetic_Operators.Product<double>(new Differentiation.Dif<double>((INode)z.D.Node.Clone(), z.E.Node), new Arithmetic_Operators.Ln((INode)z.C.Node.Clone())),
                       new Arithmetic_Operators.Divide<double>(
                           new Arithmetic_Operators.Product<double>((INode)z.D.Node.Clone(), new Differentiation.Dif<double>((INode)z.C.Node.Clone(), (VariableNode)z.E.Node.Clone())),
                           (INode)z.C.Node.Clone())))));
            
            yield return Rule
                .New("d(lnU)/dx", StdTag.Differentiation, StdTag.Algebraic)
                .Select(AnyA[ChildB[ChildC], ChildD])
                .Where<Differentiation.Dif<double>, Arithmetic_Operators.Ln, INode, VariableNode>()
                .Mod(z => z.A.Replace(new Arithmetic_Operators.Divide<double>(new Differentiation.Dif<double>(z.C.Node, z.D.Node), (INode)z.C.Node.Clone())));
             
            yield return Rule
                .New("dx/dx", StdTag.Differentiation, StdTag.Deductive, StdTag.Algebraic)
                .Select(AnyA[ChildB, ChildC])
                .Where<Differentiation.Dif<double>, VariableNode, VariableNode>(z => z.B.Index == z.C.Index)
                .Mod(z => z.A.Replace(Constant.Double(1.0)));

            yield return Rule
                .New("dy/dx", StdTag.Differentiation, StdTag.Deductive, StdTag.Algebraic)
                .Select(AnyA[ChildB, ChildC])
                .Where<Differentiation.Dif<double>, VariableNode, VariableNode>(z => z.B.Index != z.C.Index)
                .Mod(z => z.A.Replace(Constant.Double(0.0)));

            yield return Rule
                .New("dc/dx", StdTag.Differentiation, StdTag.Deductive, StdTag.Algebraic)
                .Select(AnyA[ChildB, ChildC])
                .Where<Differentiation.Dif<double>, Constant<double>, VariableNode>()
                .Mod(z => z.A.Replace(Constant.Double(0.0)));
        }
    }
}
