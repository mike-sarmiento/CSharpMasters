using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Assignment4.Generics
{
    class Program
    {
        static void Main(string[] args)
        {

            var employees = new EmployeeCollection<Employee>()
            {
                new RegularEmployee { Name = "Mike Sarmiento", MonthlySalary=50000.00M},
                new RegularEmployee { Name = "John Puruntong", MonthlySalary=120000.00M},
                new RegularEmployee { Name = "Ed Caluag", MonthlySalary=130000.00M},
                new RegularEmployee { Name = "Robert Ulep", MonthlySalary=140000.00M},
                new ContractualEmployee { Name = "Dax Celis", DailyRate=5000.00M, DaysWorked = 20},
                new ContractualEmployee { Name = "Roland Lee", DailyRate=6000.00M, DaysWorked = 18},
                new ContractualEmployee { Name = "Drew Tansinsin", DailyRate=1000.00M, DaysWorked = 15},
                new CommisionBasedEmployee { Name = "Marc Villaflor", Sales=1000000, CommisionRate = 0.05M},
                new CommisionBasedEmployee { Name = "Kevin Zara", Sales=1100000, CommisionRate = 0.06M},
                new CommisionBasedEmployee { Name = "CL Flores", Sales=1200000, CommisionRate = 0.07M},
            };

            employees.ToList().Sort(new GenericComparer<Employee>("Compensation", GenericComparer<Employee>.SortOrder.Ascending));

            foreach (var item in employees)
            {
                Console.WriteLine($"{item.Name}\t - {item.Compensation}");
            }

            Console.ReadLine();
        }

        public abstract class Employee
        {
            public string Name { get; set; }

            public decimal Compensation { get => ComputeSalary(); }

            public abstract decimal ComputeSalary();
        }

        public class RegularEmployee : Employee
        {
            public decimal MonthlySalary { get; set; }
            public override decimal ComputeSalary()
            {
                return MonthlySalary;
            }
        }

        public class ContractualEmployee : Employee
        {
            public decimal DailyRate { get; set; }

            public int DaysWorked { get; set; }
            public override decimal ComputeSalary()
            {
                return DaysWorked * DailyRate;
            }
        }

        public class CommisionBasedEmployee : Employee
        {
            public decimal CommisionRate { get; set; }

            public decimal Sales { get; set; }
            public override decimal ComputeSalary()
            {
                return Sales * CommisionRate;
            }
        }

        public class EmployeeCollection<T> : ICollection<T> where T : Employee
        {
            public EmployeeCollection()
            {
                List = new List<T>();
            }

            protected IList List { get; }

            public T this[int index] => (T)List[index];

            public int Count => this.List.Count;

            public bool IsReadOnly => throw new NotImplementedException();

            public void Add(T item)
            {
                if (!string.IsNullOrEmpty(item.Name))
                {
                    List.Add(item);
                }
            }

            public void Clear()
            {
                List.Clear();
            }

            public bool Contains(T item)
            {
                return List.Contains(item);
            }

            public void CopyTo(T[] array, int arrayIndex)
            {
                throw new NotImplementedException();
            }

            public IEnumerator<T> GetEnumerator()
            {
                return new EmployeesEnumerator<T>(this);
            }

            public bool Remove(T item)
            {
                if (item != null && Contains(item))
                {
                    this.List.Remove(item);
                    return true;
                }

                return false;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return new EmployeesEnumerator<T>(this);
            }

            public List<T> ToList()
            {
                return (List<T>)List;
            }
        }

        public class EmployeesEnumerator<T> : IEnumerator<T> where T : Employee
        {
            private readonly EmployeeCollection<T> employeeCollection;
            public int Counter = -1;

            public EmployeesEnumerator(EmployeeCollection<T> collection)
            {
                employeeCollection = collection;
            }

            public T Current => employeeCollection[Counter];

            object IEnumerator.Current => employeeCollection[Counter];

            public void Dispose()
            {
                Counter = -1;
            }

            public bool MoveNext()
            {
                ++Counter;
                if (employeeCollection.Count > Counter)
                {
                    return true;
                }

                return false;
            }

            public void Reset()
            {
                Counter = -1;
            }
        }

        public class GenericComparer<T> : IComparer<T> where T : Employee
        {
            public enum SortOrder { Ascending, Descending };

            private string sortColumn;

            private SortOrder sortingOrder;

            public GenericComparer(string sortColumn, SortOrder sortingOrder)
            {
                this.sortColumn = sortColumn;

                this.sortingOrder = sortingOrder;
            }

            public string SortColumn
            {
                get { return sortColumn; }
            }

            public SortOrder SortingOrder
            {
                get { return sortingOrder; }
            }

            public int Compare(T x, T y)
            {
                PropertyInfo propertyInfo = typeof(T).GetProperty(sortColumn);

                IComparable obj1 = (IComparable)propertyInfo.GetValue(x, null);

                IComparable obj2 = (IComparable)propertyInfo.GetValue(y, null);

                if (sortingOrder == SortOrder.Ascending)
                {
                    return (obj1.CompareTo(obj2));
                }
                else
                {
                    return (obj2.CompareTo(obj1));
                }
            }
        }
    }
}
