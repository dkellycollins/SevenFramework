﻿// ComputerAlgebra Library
//
// Copyright © Medvedev Igor, Okulovsky Yuri, Borcheninov Jaroslav, 2013
// imedvedev3@gmail.com, yuri.okulovsky@gmail.com, yariksuperman@gmail.com
//

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Seven.Mathematics.Symbolics.Tree;

namespace Seven.Mathematics.Symbolics.Rules
{

    public partial class SelectWhereRule
    {
        public SelectRule SelectRule;
        public SelectWhereRule(SelectRule selectRule)
        {
            this.SelectRule = selectRule;
        }
    }


    public partial class SelectRule
    {
        public ComplexSelector Selector;
        public NewRule NewRule;
        public SelectRule(NewRule newRule, ComplexSelector selector)
        {
            this.Selector = selector;
            NewRule = newRule;
        }
    }

    public class NewRule
    {
        public string Name;
        public Seven.Mathematics.Symbolics.RulesCollection.StdTag[] Tags;
        public NewRule(string name, Seven.Mathematics.Symbolics.RulesCollection.StdTag[] Tags)
        {
            this.Name = name;
            this.Tags = Tags;
        }

        public SelectRule Select(params SelectClauseNode[] clauses)
        {
            return new SelectRule(this,new ComplexSelector(clauses));
        }
    }

    public partial class Rule
    {
        readonly ComplexSelector _selector;
        readonly Func<SelectOutput, WhereOutput> _where;
        readonly Action<ModInput> _apply;

        public string Name { get; private set; }
        public ReadOnlyCollection<Seven.Mathematics.Symbolics.RulesCollection.StdTag> Tags { get; private set; }

        public Rule(string name, Seven.Mathematics.Symbolics.RulesCollection.StdTag[] tags, ComplexSelector selector, Func<SelectOutput, WhereOutput> where, Action<ModInput> apply)
        {
            this._selector=selector;
            this._where=where;
            this._apply=apply;
            this.Name = name;
            this.Tags = new ReadOnlyCollection<Seven.Mathematics.Symbolics.RulesCollection.StdTag>(tags);
        }

        public IEnumerable<SelectOutput> Select(params INode[] roots)
        {
            return _selector.Select(roots);
        }

        public IEnumerable<WhereOutput> SelectWhere(params INode[] roots)
        {
            return _selector.Select(roots)
                           .Select(e => _where(e))
                           .Where(res => res != null);
        }

        public INode[] Apply(WhereOutput instance)
        {
            var safe = instance.MakeSafe();
            _apply(safe);
            return safe.Roots.Any(e => !e.TestRoot()) 
                ? null 
                : safe.Roots;
        }

        public static NewRule New(string name, params Seven.Mathematics.Symbolics.RulesCollection.StdTag[] tags)
        {
            return new NewRule(name,tags);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
