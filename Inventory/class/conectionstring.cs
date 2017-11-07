using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class connectionstring
{
   public static string GetConnString(string _confor)
    {
        string functionReturnValue = null;

        functionReturnValue = "Data Source=DELVKUMAR\\INGEN;Initial Catalog=inventory;Integrated Security=True";
        return functionReturnValue;
    }
}
