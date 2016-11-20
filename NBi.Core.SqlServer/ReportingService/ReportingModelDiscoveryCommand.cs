﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NBi.Core.Model;
using NBi.Core.Report;

namespace NBi.Core.SqlServer.ReportingService
{
    public abstract class ReportingModelDiscoveryCommand : IModelDiscoveryCommand
    {
        protected readonly IDbCommand command;
        protected readonly IEnumerable<IPostCommandFilter> postFilters;
        protected readonly ICommandDescription description;

        protected internal ReportingModelDiscoveryCommand(IDbCommand command, IEnumerable<IPostCommandFilter> postFilters, CommandDescription description)
        {
            this.command = command;
            this.postFilters = postFilters;
            this.description = description;
        }

        public virtual ICommandDescription Description
        {
            get { return description; }
        }

        public abstract IEnumerable<string> Execute();

    }
}