using System;

namespace Assignement3.OOP
{
    class Program
    {
        static void Main(string[] args)
        {
            var contractualEmployee1 = new Contractual()
            {
                FirstName = "Michael",
                LastName = "Sarmiento",
                AnnualSalary = 800000M
            };

            var contractualPayoutType = PayoutType.Hour;
            Console.WriteLine($"{nameof(Contractual)} - {contractualEmployee1.FirstName} {contractualEmployee1.LastName} Salary per {contractualPayoutType} {contractualEmployee1.GetSalary(contractualPayoutType).ToString("0.00")}");

            var contractualEmployee2 = new Contractual()
            {
                FirstName = "John",
                LastName = "Puruntong",
                AnnualSalary = 1200000M
            };

            var contractual2PayoutType = PayoutType.Day;
            Console.WriteLine($"{nameof(Contractual)} - {contractualEmployee2.FirstName} {contractualEmployee2.LastName} Salary per {contractual2PayoutType} {contractualEmployee2.GetSalary(contractual2PayoutType).ToString("0.00")}");


            var dev1 = new Developer()
            {
                FirstName = "Edward",
                LastName = "Lu",
                AnnualSalary = 1800000M,
            };

            var developerPayoutType = PayoutType.Month;
            Console.WriteLine($"{nameof(Developer)} - {dev1.FirstName} {dev1.LastName} Salary per {developerPayoutType} {dev1.GetSalary(developerPayoutType).ToString("0.00")}");

            Console.ReadLine();
        }

        public abstract class Employee
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string MiddleName { get; set; }
            public DateTime BirthDate { get; set; }
            internal decimal AnnualSalary { get; set; }
            public Employee() { }
            ~Employee() { }
            public void Work() { }

            public abstract decimal GetSalary(PayoutType payoutType);
        }



        public class Consultant : Employee
        {
            public string ProjectName { get; set; }

            public override decimal GetSalary(PayoutType payoutType)
            {
                return GetSalarayByPayoutType(payoutType, AnnualSalary);
            }
        }
        public class QualityEngineer : Employee
        {
            public string TestingTool { get; set; }

            public override decimal GetSalary(PayoutType payoutType)
            {
                return GetSalarayByPayoutType(payoutType, AnnualSalary);
            }
        }
        public class Developer : Employee
        {
            public string ProgrammingLanguage { get; set; }

            public override decimal GetSalary(PayoutType payoutType)
            {
                return GetSalarayByPayoutType(payoutType, AnnualSalary);
            }
        }
        public class Contractual : Employee
        {
            public override decimal GetSalary(PayoutType payoutType)
            {
                return GetSalarayByPayoutType(payoutType, AnnualSalary);
            }
        }

        public enum PayoutType
        {
            Hour,
            Day,
            Month,
            Commission
        }

        public static decimal GetSalarayByPayoutType(PayoutType payoutType, decimal annualSalary)
        {
            decimal salary = 0;

            switch (payoutType)
            {
                case PayoutType.Hour:
                    salary = annualSalary / 1920;
                    break;
                case PayoutType.Day:
                    salary = annualSalary / 240;
                    break;
                case PayoutType.Month:
                    salary = annualSalary / 12;
                    break;
                case PayoutType.Commission:
                    salary = 0.1M * annualSalary;
                    break;
            }

            return salary;
        }
    }
}
