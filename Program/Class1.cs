using System;
using System.Collections.Generic;

namespace Program
{
    public abstract class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ReportingManager { get; set; }

        public Employee(int id, string name, string reportingManager)
        {
            ID = id;
            Name = name;
            ReportingManager = reportingManager;
        }

        public abstract void DisplayDetails();
    }

    public class ContractEmployee : Employee
    {
        public DateTime ContractDate { get; set; }
        public int Duration { get; set; } 
        public double Charges { get; set; }

        public ContractEmployee(int id, string name, string reportingManager, DateTime contractDate, int duration, double charges)
            : base(id, name, reportingManager)
        {
            ContractDate = contractDate;
            Duration = duration;
            Charges = charges;
        }

        public override void DisplayDetails()
        {
            Console.WriteLine($"[Contract Employee] ID: {ID}, Name: {Name}, Manager: {ReportingManager}, Contract Date: {ContractDate.ToShortDateString()}, Duration: {Duration} months, Charges: {Charges:C}");
        }
    }

    public class PayrollEmployee : Employee
    {
        public DateTime JoiningDate { get; set; }
        public int Experience { get; set; } 
        public double BasicSalary { get; set; }
        public double DA { get; private set; }
        public double HRA { get; private set; }
        public double PF { get; private set; }
        public double NetSalary { get; private set; }

        public PayrollEmployee(int id, string name, string reportingManager, DateTime joiningDate, int experience, double basicSalary)
            : base(id, name, reportingManager)
        {
            JoiningDate = joiningDate;
            Experience = experience;
            BasicSalary = basicSalary;
        }

        public void CalculateSalary()
        {
            if (Experience > 10)
            {
                DA = 0.1 * BasicSalary;
                HRA = 0.085 * BasicSalary;
                PF = 6200;
            }
            else if (Experience > 7 && Experience <= 10)
            {
                DA = 0.07 * BasicSalary;
                HRA = 0.065 * BasicSalary;
                PF = 4100;
            }
            else if (Experience > 5 && Experience <= 7)
            {
                DA = 0.041 * BasicSalary;
                HRA = 0.038 * BasicSalary;
                PF = 1800;
            }
            else
            {
                DA = 0.019 * BasicSalary;
                HRA = 0.02 * BasicSalary;
                PF = 1200;
            }

            NetSalary = BasicSalary + DA + HRA - PF;
        }

        public override void DisplayDetails()
        {
            Console.WriteLine($"[Payroll Employee] ID: {ID}, Name: {Name}, Manager: {ReportingManager}, Joining Date: {JoiningDate.ToShortDateString()}, Experience: {Experience} years, Basic Salary: {BasicSalary:C}, DA: {DA:C}, HRA: {HRA:C}, PF: {PF:C}, Net Salary: {NetSalary:C}");
        }
    }

    public class Program
    {
        public static void Main()
        {
            
            List<Employee> employees = new List<Employee>();

           
            employees.Add(new ContractEmployee(1, "Alice", "Manager1", DateTime.Now.AddMonths(-6), 12, 50000));
            employees.Add(new PayrollEmployee(2, "Bob", "Manager2", DateTime.Now.AddYears(-8), 8, 70000));
            employees.Add(new PayrollEmployee(3, "Charlie", "Manager3", DateTime.Now.AddYears(-11), 11, 85000));

            
            foreach (var emp in employees)
            {
                if (emp is PayrollEmployee payrollEmployee)
                {
                    payrollEmployee.CalculateSalary();
                }
            }

            
            foreach (var emp in employees)
            {
                emp.DisplayDetails();
            }

           
            Console.WriteLine($"Total Number of Employees: {employees.Count}");
        }
    }
}