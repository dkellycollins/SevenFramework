// ComputerAlgebra Library
//
// Copyright © Medvedev Igor, Okulovsky Yuri, Borcheninov Jaroslav, 2013
// imedvedev3@gmail.com, yuri.okulovsky@gmail.com, yariksuperman@gmail.com
//

using System;
using System.Collections.Generic;
using Seven.Mathematics.Symbolics.Resolution;
using Seven.Mathematics.Symbolics.Rules;
using Seven.Mathematics.Symbolics.Tree;

namespace Seven.Mathematics.Symbolics.RulesCollection
{
    public partial class LogicRules : SelectClauseWriter
    {
        private static readonly Random Rnd = new Random();

        public static IEnumerable<Rule> Get()
        {
            yield return Rule
                .New("&&0", StdTag.Inductive, StdTag.Logic, StdTag.SafeResection, StdTag.Simplification)
                .Select(AnyA[ChildB, ChildC])
                .Where<Logic_Operators.And, Constant<bool>, INode>(z => !z.B.Value)
                .Mod(z => z.A.Replace(z.B.Node));

            yield return Rule
                .New("||1", StdTag.Inductive, StdTag.Logic, StdTag.SafeResection, StdTag.Simplification)
                .Select(AnyA[ChildB, ChildC])
                .Where<Logic_Operators.Or, Constant<bool>, INode>(z => z.B.Value)
                .Mod(z => z.A.Replace(z.B.Node));

            yield return Rule
                .New("&&1", StdTag.Inductive, StdTag.Logic, StdTag.SafeResection, StdTag.Simplification)
                .Select(AnyA[ChildB, ChildC])
                .Where<Logic_Operators.And, Constant<bool>, INode>(z => z.B.Value)
                .Mod(z => z.A.Replace(z.C.Node));

            yield return Rule
                .New("||0", StdTag.Inductive, StdTag.Logic, StdTag.SafeResection, StdTag.Simplification)
                .Select(AnyA[ChildB, ChildC])
                .Where<Logic_Operators.Or, Constant<bool>, INode>(z => !z.B.Value)
                .Mod(z => z.A.Replace(z.C.Node));

            yield return Rule
                .New("!!", StdTag.Deductive, StdTag.Logic, StdTag.SafeResection, StdTag.Simplification)
                .Select(AnyA[B[C]])
                .Where<Logic_Operators.Not, Logic_Operators.Not, INode>()
                .Mod(z => z.A.Replace(z.C.Node));

            yield return Rule
                .New("x V x", StdTag.Inductive, StdTag.Logic, StdTag.SafeResection, StdTag.Simplification)
                .Select(A[ChildB, ChildC])
                .Where<Logic_Operators.Or, INode, INode>(z => UnificationService.IsSame(z.B, z.C, true))
                .Mod(z => z.A.Replace(z.B.Node));

            yield return Rule
                .New("!x V x", StdTag.Inductive, StdTag.Logic, StdTag.SafeResection, StdTag.Simplification)
                .Select(A[ChildB[ChildC], ChildD])
                .Where<Logic_Operators.Or, Logic_Operators.Not, INode, INode>(z => UnificationService.IsSame(z.C, z.D, true))
                .Mod(z => z.A.Replace(Constant.Bool(true)));
        }
    }
}