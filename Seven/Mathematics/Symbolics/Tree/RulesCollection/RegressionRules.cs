// ComputerAlgebra Library
//
// Copyright © Medvedev Igor, Okulovsky Yuri, Borcheninov Jaroslav, 2013
// imedvedev3@gmail.com, yuri.okulovsky@gmail.com, yariksuperman@gmail.com
//

using System.Collections.Generic;
using Seven.Mathematics.Symbolics.Regression;
using Seven.Mathematics.Symbolics.Rules;
using Seven.Mathematics.Symbolics.Tree;

namespace Seven.Mathematics.Symbolics.RulesCollection
{
    public class RegressionRules : SelectClauseWriter
    {
        public static int IterationCount;

        public static IEnumerable<Rule> Get(List<double[]> inSample, List<double> exactResult)
        {
            yield return Rule
                .New("Regression", StdTag.Regression, StdTag.Algebraic)
                .Select(AnyA)
                .Where<INode<double>>()
                .Mod(z => Modificator(z, inSample, exactResult, IterationCount));
        }

        private static void Modificator(TypizedDecorArray<INode<double>> z, List<double[]> inSample, List<double> exactResult, int iterationCount)
        {
            var alg = new RegressionAlgorithm(z.A.Node, inSample, exactResult);
            alg.Run(iterationCount); 
            z.A.Replace(alg.GetResult());
        }
    }
}