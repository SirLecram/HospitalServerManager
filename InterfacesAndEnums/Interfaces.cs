using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalServerManager.InterfacesAndEnums
{
    public interface ISqlTableModelable : IPrimaryKeyGetable
    {
        List<string> GetColumnNames();
    }
    public interface IPrimaryKeyGetable
    {
        string GetPrimaryKey();
        string GetPrimaryKeyName();
    }
}
