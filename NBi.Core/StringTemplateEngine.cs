﻿using System;
using System.Collections.Generic;
using System.Linq;
using Antlr4.StringTemplate;
using NBi.Core.Query;

namespace NBi.Core
{
    public class StringTemplateEngine
    {
        public string Template { get; private set; }
        public IEnumerable<IQueryTemplateVariable> Variables { get; private set; }

        public StringTemplateEngine(string template, IEnumerable<IQueryTemplateVariable> variables)
        {
            Template = template;
            Variables = variables;
        }

        public string Build()
        {
            var template = new Template(Template, '$', '$');

            foreach (IQueryTemplateVariable variable in Variables)
                template.Add(variable.Name, variable.Value);

            var str = template.Render();

            return str;
        }
    }
}
