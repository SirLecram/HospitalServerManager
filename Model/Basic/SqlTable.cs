using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalServerManager.InterfacesAndEnums;
using HospitalServerManager.Model.Controllers;

namespace HospitalServerManager.Model.Basic
{
    internal abstract class SqlTable : ISqlTableModel
    {
		// TODO: Dodac table do klas modelu
		public string PrimaryKey { get; protected set; }
        public string PrimaryKeyName { get; set; }
        protected List<string> ColumnNames { get; set; }
        protected SqlTable()
        {
            PrimaryKey = "BRAK";
            PrimaryKeyName = "BRAK";
            ColumnNames = new List<string>();
        }
        public SqlTable(string primaryKey, string primaryKeyName, List<string> columnNames)
        {
            PrimaryKey = primaryKey;
            PrimaryKeyName = primaryKeyName;
            ColumnNames = columnNames;
        }

		public List<string> GetColumnNames()
		{
			return ColumnNames;
		}
	}
}
