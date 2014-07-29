// ComputerAlgebra Library
//
// Copyright © Medvedev Igor, Okulovsky Yuri, Borcheninov Jaroslav, 2013
// imedvedev3@gmail.com, yuri.okulovsky@gmail.com, yariksuperman@gmail.com
//

using System;
using System.Collections.Generic;
using Seven.Mathematics.Symbolics.Rules;
using Seven.Mathematics.Symbolics.Tree;

namespace Seven.Mathematics.Symbolics.RulesCollection
{
  public class AlgebraicRules : SelectClauseWriter
  {
    //public static Rule[] _simplifications = new Rule[]
    //{
    //  Rule
    //    .New("*0", StdTags.SafeResection, StdTags.Algebraic, StdTags.Simplification)
    //    .Select(AnyA[ChildB, ChildC])
    //    .Where<Arithmetic_Operators.Product<double>, Constant<double>, INode>(z => z.B.Value == 0)
    //    .Mod(z => z.A.Replace(z.B.Node)),
    //};

    public static IEnumerable<Rule> Get()
    {
      yield return Rule
          .New("*0", StdTag.SafeResection, StdTag.Algebraic, StdTag.Simplification)
          .Select(AnyA[ChildB, ChildC])
          .Where<Arithmetic_Operators.Product<double>, Constant<double>, INode>(z => z.B.Value == 0)
          .Mod(z => z.A.Replace(z.B.Node));

      yield return Rule
          .New("*1", StdTag.SafeResection, StdTag.Algebraic, StdTag.Simplification)
          .Select(AnyA[ChildB, ChildC])
          .Where<Arithmetic_Operators.Product<double>, Constant<double>, INode>(z => z.B.Value == 1)
          .Mod(z => z.A.Replace(z.C.Node));

      yield return Rule
          .New("+0", StdTag.SafeResection, StdTag.Algebraic, StdTag.Simplification)
          .Select(AnyA[ChildB, ChildC])
          .Where<Arithmetic_Operators.Plus<double>, Constant<double>, INode>(z => z.B.Value == 0)
          .Mod(z => z.A.Replace(z.C.Node));

      yield return Rule
          .New("-0", StdTag.SafeResection, StdTag.Algebraic, StdTag.Simplification)
          .Select(AnyA[B, C])
          .Where<Arithmetic_Operators.Minus<double>, INode, Constant<double>>(z => z.C.Value == 0)
          .Mod(z => z.A.Replace(z.B.Node));

      yield return Rule
          .New("0-", StdTag.SafeResection, StdTag.Algebraic, StdTag.Simplification)
          .Select(AnyA[B, C])
          .Where<Arithmetic_Operators.Minus<double>, Constant<double>, INode>(z => z.B.Value == 0)
          .Mod(z => z.A.Replace(new Arithmetic_Operators.Negate<double>(z.C.Node)));

      yield return Rule
          .New("/1", StdTag.SafeResection, StdTag.Algebraic, StdTag.Simplification)
          .Select(AnyA[B, C])
          .Where<Arithmetic_Operators.Divide<double>, INode, Constant<double>>(z => z.C.Value == 1)
          .Mod(z => z.A.Replace(z.B.Node));

      yield return Rule
          .New("0/", StdTag.SafeResection, StdTag.Algebraic, StdTag.Simplification)
          .Select(AnyA[B, C])
          .Where<Arithmetic_Operators.Divide<double>, Constant<double>, INode>(z => z.B.Value == 0)
          .Mod(z => z.A.Replace(z.B.Node));

      yield return Rule
          .New("0^", StdTag.SafeResection, StdTag.Algebraic, StdTag.Simplification)
          .Select(AnyA[B, C])
          .Where<Arithmetic_Operators.Pow<double>, Constant<double>, INode>(z => z.B.Value == 0)
          .Mod(z => z.A.Replace(z.B.Node));

      yield return Rule
          .New("^1", StdTag.SafeResection, StdTag.Algebraic, StdTag.Simplification)
          .Select(AnyA[B, C])
          .Where<Arithmetic_Operators.Pow<double>, INode, Constant<double>>(z => z.C.Value == 1)
          .Mod(z => z.A.Replace(z.B.Node));

      yield return Rule
          .New("^0", StdTag.SafeResection, StdTag.Algebraic, StdTag.Simplification)
          .Select(AnyA[B, C])
          .Where<Arithmetic_Operators.Pow<double>, INode, Constant<double>>(z => z.C.Value == 0)
          .Mod(z => z.A.Replace(Constant.Double(1)));

      yield return Rule
          .New("(-0)", StdTag.SafeResection, StdTag.Algebraic, StdTag.Simplification)
          .Select(AnyA[B])
          .Where<Arithmetic_Operators.Negate<double>, Constant<double>>(z => z.B.Value == 0)
          .Mod(z => z.A.Replace(z.B.Node));

      yield return Rule
          .New("C+C", StdTag.SafeResection, StdTag.Algebraic, StdTag.Simplification)
          .Select(AnyA[B, C])
          .Where<Arithmetic_Operators.Plus<double>, Constant<double>, Constant<double>>()
          .Mod(z => z.A.Replace(Constant.Double(z.B.Node.Value + z.C.Node.Value)));

      yield return Rule
          .New("C*C", StdTag.SafeResection, StdTag.Algebraic, StdTag.Simplification)
          .Select(AnyA[B, C])
          .Where<Arithmetic_Operators.Product<double>, Constant<double>, Constant<double>>()
          .Mod(z => z.A.Replace(Constant.Double(z.B.Node.Value * z.C.Node.Value)));

      yield return Rule
          .New("C-C", StdTag.SafeResection, StdTag.Algebraic, StdTag.Simplification)
          .Select(AnyA[B, C])
          .Where<Arithmetic_Operators.Minus<double>, Constant<double>, Constant<double>>()
          .Mod(z => z.A.Replace(Constant.Double(z.B.Node.Value - z.C.Node.Value)));

      yield return Rule
          .New("C^C", StdTag.SafeResection, StdTag.Algebraic, StdTag.Simplification)
          .Select(AnyA[B, C])
          .Where<Arithmetic_Operators.Pow<double>, Constant<double>, Constant<double>>()
          .Mod(z => z.A.Replace(Constant.Double(Math.Pow(z.B.Node.Value, z.C.Node.Value))));

      yield return Rule
          .New("C/C", StdTag.SafeResection, StdTag.Algebraic, StdTag.Simplification)
          .Select(AnyA[B, C])
          .Where<Arithmetic_Operators.Divide<double>, Constant<double>, Constant<double>>()
          .Mod(z => z.A.Replace(Constant.Double(z.B.Node.Value / z.C.Node.Value)));

      yield return Rule
          .New("(x+C)+C", StdTag.Algebraic, StdTag.Simplification)
          .Select(AnyA[ChildB[ChildC, ChildD], ChildE])
          .Where<Arithmetic_Operators.Plus, Arithmetic_Operators.Plus, INode, Constant<double>, Constant<double>>()
          .Mod(z => z.A.Replace(new Arithmetic_Operators.Plus<double>(z.C.Node, Constant.Double(z.D.Node.Value + z.E.Node.Value))));

      yield return Rule
          .New("(x-C)+C", StdTag.Algebraic, StdTag.Simplification)
          .Select(AnyA[ChildB[C, D], ChildE])
          .Where<Arithmetic_Operators.Plus, Arithmetic_Operators.Minus, INode, Constant<double>, Constant<double>>()
          .Mod(z => z.A.Replace(new Arithmetic_Operators.Plus<double>(z.C.Node, Constant.Double(z.E.Node.Value - z.D.Node.Value))));
    }
  }
}
