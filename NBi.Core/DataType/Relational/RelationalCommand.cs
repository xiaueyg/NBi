﻿using Microsoft.AnalysisServices.AdomdClient;
using NBi.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBi.Core.DataType.Relational
{

    class RelationalCommand : DataTypeDiscoveryCommand
    {
        public RelationalCommand(IDbCommand command, CommandDescription description)
            : base(command, description)
        {
        }

        public override DataTypeInfo Execute()
        {
            RelationalRow row = null;

            command.Connection.Open();
            var rdr = ExecuteReader(command);
            if (rdr.Read())
            {
                row = new RelationalRow();
                row.IsNullable = rdr.GetString(0);
                row.DataType = rdr.GetString(1);
                row.CharacterMaximumLength = rdr.IsDBNull(2) ? 0 : rdr.GetInt32(2);
                row.NumericPrecision = rdr.IsDBNull(3) ? 0 : rdr.GetByte(3);
                row.NumericScale = rdr.IsDBNull(4) ? 0 : rdr.GetInt32(4);
                row.DateTimePrecision = rdr.IsDBNull(5) ? 0 : rdr.GetInt16(5);
                row.CharacterSetName = rdr.IsDBNull(6) ? string.Empty : rdr.GetString(6);
                row.CollationName = rdr.IsDBNull(7) ? string.Empty : rdr.GetString(7);
                row.DomainName = rdr.IsDBNull(8) ? string.Empty : rdr.GetString(8);
                
            }
            command.Connection.Close();

            if (row != null)
            {
                var factory = new DataTypeInfoFactory();
                var dataTypeInfo = factory.Instantiate(row);
                return dataTypeInfo;
            }
            else
                return null;
        }

        protected IDataReader ExecuteReader(IDbCommand cmd)
        {
            Trace.WriteLineIf(NBiTraceSwitch.TraceInfo, cmd.CommandText);

            IDataReader rdr = null;
            try
            {
                rdr = cmd.ExecuteReader();
                return rdr;
            }
            catch (Exception ex)
            { throw new ConnectionException(ex, cmd.Connection.ConnectionString); }
        }
    }
}
