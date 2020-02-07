using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSProgram.Entry
{
    class EmployeeEntity
    {
        private int id;
        private string employeeID;
        private string employeeName;

        public EmployeeEntity(int id, string employeeID, string employeeName)
        {
            this.id = id;
            this.employeeID = employeeID;
            this.employeeName = employeeName;
        }

        public int ID { get => id; set => id = value; }
        public string EmployeeID { get => employeeID; set => employeeID = value; }
        public string EmployeeName { get => employeeName; set => employeeName = value; }
    }
}
