using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApp
{
    public class Employee
    {
        public int EmployeeID { get; set; } // Уникальный идентификатор сотрудника
        public string Name { get; set; } // Имя сотрудника
        public string Position { get; set; } // Должность
        public string Department { get; set; } // Отдел
        public DateTime HireDate { get; set; } // Дата приема на работу
        public decimal Salary { get; set; } // Зарплата
        public string Email { get; set; } // Email
    }
}
