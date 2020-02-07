using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace POSProgram.Entry
{
    class LoginEnty
    {
        private string Id;
        private string employeeId;
        private string userName;
        private string password;
        private string birthDate;
        private string photo;
        private string note;

        public LoginEnty(string id, string employeeId, string userName, string password, string birthDate, string photo, string note)
        {
            this.Id = id;
            this.employeeId = employeeId;
            this.userName = userName;
            this.password = password;
            this.birthDate = birthDate;
            this.photo = photo;
            this.note = note;
        }

        public string ID { get => Id; set => Id = value; }
        public string EmployeeId { get => employeeId; set => employeeId = value; }
        public string UserName { get => userName; set => userName = value; }
        public string Password { get => password; set => password = value; }
        public string BirthDate { get => birthDate; set => birthDate = value; }
        public string Photo { get => photo; set => photo = value; }
        public string Note { get => note; set => note = value; }
    }
}
