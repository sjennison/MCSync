﻿using ClussPro.Base.Data;
using ClussPro.Base.Data.Query;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ClussPro.SqlServerProvider
{
    public class CreateTableQuery : BaseTransactionalQuery, ICreateTable
    {
        public string SchemaName { get; set; }
        public string TableName { get; set; }
        public Dictionary<string, FieldSpecification> Columns { get; set; } = new Dictionary<string, FieldSpecification>();

        public void Execute(ITransaction transaction)
        {
            CheckedTransactionExecute(transaction, (localTransaction) =>
            {
                using (SqlCommand command = new SqlCommand(null, localTransaction.SQLTransaction.Connection, localTransaction.SQLTransaction))
                {
                    command.CommandText = GetSQL();
                    command.ExecuteNonQuery();
                }
            });
        }

        private string GetSQL()
        {
            StringBuilder builder = new StringBuilder("CREATE TABLE ");
            builder.Append($"[{SchemaName}].[{TableName}] (");

            bool first = true;
            foreach(KeyValuePair<string, FieldSpecification> col in Columns)
            {
                if (!first)
                {
                    builder.Append(", ");
                }
                first = false;

                builder.Append($"[{col.Key}] ");

                switch(col.Value.FieldType)
                {
                    case FieldSpecification.FieldTypes.BigInt:
                        builder.Append("BIGINT ");
                        break;
                    case FieldSpecification.FieldTypes.NVarChar:
                        builder.Append("NVARCHAR(");

                        if (col.Value.DataSize == -1)
                        {
                            builder.Append("MAX) ");
                        }
                        else
                        {
                            builder.Append($"{col.Value.DataSize}) ");
                        }
                        break;
                    case FieldSpecification.FieldTypes.Binary:
                        builder.Append("VARBINARY(");
                        if (col.Value.DataSize == -1)
                        {
                            builder.Append("MAX) ");
                        }
                        else
                        {
                            builder.Append($"{col.Value.DataSize}) ");
                        }
                        break;
                    case FieldSpecification.FieldTypes.TinyInt:
                        builder.Append("TINYINT ");
                        break;
                    case FieldSpecification.FieldTypes.DateTime2:
                        builder.Append($"DATETIME2({col.Value.DataSize}) ");
                        break;
                    case FieldSpecification.FieldTypes.UniqueIdentifier:
                        builder.Append("UNIQUEIDENTIFIER ");
                        break;
                    case FieldSpecification.FieldTypes.Bit:
                        builder.Append("BIT ");
                        break;
                    case FieldSpecification.FieldTypes.Int:
                        builder.Append("INT ");
                        break;
                    case FieldSpecification.FieldTypes.Decimal:
                        builder.Append(string.Format("DECIMAL ({0}, {1}) ", col.Value.DataSize, col.Value.DataScale));
                        break;
                    case FieldSpecification.FieldTypes.SmallInt:
                        builder.Append("SMALLINT ");
                        break;
                }

                if (col.Value.IsPrimary)
                {
                    builder.Append("PRIMARY KEY IDENTITY ");
                }

                if (col.Value.DefaultValue != null)
                {
                    builder.Append($"DEFAULT '{col.Value.DefaultValue}' NOT NULL");
                }
            }

            builder.Append(")");

            return builder.ToString();
        }
    }
}
