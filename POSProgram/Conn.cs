using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSProgram
{
    class Conn
    {
        // string connection=>ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        public string getConnection()
        {
            // return "server=192.168.99.100;user id=root; password=p123456;database=LearnDB";
            //Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DB12;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False
           // @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=pos;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
            return @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DB12;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }
    }
}
