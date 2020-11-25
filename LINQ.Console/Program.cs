
using LINQ.ClassLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace LINQ.ConsoleApp
{
    class Program
    {
        #region === Delegates ===

        public delegate int Sum(int val1, int val2);

        public static int PrimaSomma(int valore1, int valore2)
        {
            return valore1 + valore2;
        }

        public static int SecondaSomma(int valore1, double valore2)
        {
            return valore1 + (int)valore2;
        }

        public static void CallMe(Sum functionToInvoke)
        {
            functionToInvoke(1,2);
        }

        public static int AnotherFunc() { return 0; }

        #endregion

        static void Main(string[] args)
        {
            #region === Step 1 ===

            Console.WriteLine("=== LINQ ===");

            string firstName = "Roberto";

            var lastName = 0.4;

            //lastName = "Ajolfi";   // ERRORE!

            //using var file = new StreamWriter(null);

            //var data = new List<int>{ 1, 2, 3, 4 };

            List<Employee<int>> data = new List<Employee<int>>
            {
                new Employee<int>()
            };

            Employee<int> firstEmployee = new Employee<int>();
            Employee<string> secondEmployee;

            // Extension Methods
            string example = "230";
            example.ToUpper();
            Console.WriteLine(example.ToDouble());
            var prefix = example.WithPrefix("[TST]");
            Console.WriteLine(prefix);

            MyString example2 = new MyString();
            example2.Value = "Example";
            example2.Value.ToUpper();
            // OPPURE
            example2.ToUpper();


            foreach (var value in data)
                Console.WriteLine("#" + value.Name);

            Class1 class1 = new Class1();

            //Class2 class2 = new ClassLib.Class2();

            var person = new { firstName = "Roberto", lastName = "Ajolfi", eta = 12 };

            var person2 = new { nome = "Alice", cognome = "Colella" };

            var person3 = person2;

            firstEmployee.ID = 9;

            #endregion

            #region === Step 2 ===

            // EVENTS
            var process = new BusinessProcess();
            process.Started += Process_Started;
            process.Started += Process_Started1;
            process.Completed += CompletedProcess;

            process.StartedCore += Process_StartedCore;
            //process.ProcessData();

            // DELEGATES
            Sum lamiaSomma = new Sum(PrimaSomma);
            //// OPPURE
            Sum lamiaSomma2 = PrimaSomma;

            // Sum == Func<int, int, int>

            // Func and Action
            Func<int, double, int> primaFunc = SecondaSomma;
            Func<int, int, int> secondaFunc = PrimaSomma;

            Action<int> primaAction;

            //// ERRORE!!! Wrong Signature
            ////lamiaSomma = SecondaSomma;

            //Chiamami(lamiaSomma);
            //Chiamami(PrimaSomma);
            //// ERRORE
            //Chiamami(SecondaSomma);

            #endregion

            #region === Step 3 ===

            Func<int, int> lamdbaZero = x => 2 * x;

            Func<int, int> lamdbaZeroZero = x => {
                var result = 2 * x;
                return result; 
            };

            Func<int, int> lamdbaZeroZeroZero = Multiply;

            lamdbaZeroZero(45);

            var list = new List<int> { 1, 2, 3, 4, 5, 6 };
            //var results = Where(dataInt, x => x > 2);
            
            var results = list.Where(x => x > 2);

            Func<int, double, bool> lambdaOne = (x, y) => x > (int)y;

            #endregion

            #region === Step 4 ===

            List<EmployeeInt> employees = new List<EmployeeInt>
            {
                new EmployeeInt { ID = 1, Name ="Roberto"},
                new EmployeeInt { ID = 2, Name ="Alice"},
                new EmployeeInt { ID = 3, Name ="Mauro"},
                new EmployeeInt { ID = 4, Name ="Roberto"},
            };

            var result = employees.Where("ID", "1");
            var result2 = employees.Where("Name", "Roberto");           

            // value => value * value

            ParameterExpression y = Expression.Parameter(typeof(int), "value");
            var basettoni = new ParameterExpression[] { 
                y 
            };

            Expression<Func<int, int>> squareExpression =
               Expression.Lambda<Func<int, int>>(
                Expression.Multiply(y, y),
                basettoni
               );

            Expression<Func<int, int>> squareExpression2 = value => value * value;

            Func<int, int> funzione = squareExpression.Compile();
            Console.WriteLine(funzione(3));

            var emp = EmployeeInt.Empty;

            #endregion

            #region === Step 5 ===

            var products = new List<Product>
            {
                new Product { ID = 1, ProductCode ="PC001" },
                new Product { ID = 2, ProductCode ="PC001" },
                new Product { ID = 1, ProductCode ="PC001" }
            };

            int resultCount1 = products.Select(s => s).Distinct().Count();
            int resultCount1a = products.Select(s => s).Distinct(new ProductComparer()).Count();
            int resultCount2 = products.Select(s => new { s.ID, s.ProductCode }).Distinct().Count();

            Console.WriteLine($"{resultCount1} - {resultCount1a} - {resultCount2}");

            #endregion

            #region === Step 6 ===

            List<Employee> objEmployee = new List<Employee>()
            {
                new Employee(){ Name="Ashish Sharma", Department="Marketing", Country="India"},
                new Employee(){ Name="John Smith", Department="IT", Country="USA"},
                new Employee(){ Name="Kim Jong", Department="Sales", Country="China"},
                new Employee(){ Name="Marcia Adams", Department="HR", Country="USA"},
                new Employee(){ Name="John Doe", Department="Operations", Country="Canada"}
            };

            var emp1 = objEmployee.ToLookup(x => x.Country);

            Console.WriteLine("Grouping Employees by Country");
            Console.WriteLine("---------------------------------");

            foreach (var grouping in emp1)
            {
                Console.WriteLine(grouping.Key);

                // Lookup employees by Country
                foreach (var item in emp1[grouping.Key])
                {
                    Console.WriteLine("\t" + item.Name + "\t" + item.Department);
                }
            }

            #endregion


        }

        #region === Misc Methods ===

        private static List<int> Where(List<int> data, Func<int, bool> condizione)
        {
            var results = new List<int>();

            foreach (int value in data)
                if (condizione(value))
                    results.Add(value);

            return results;
        }

        private static int SampleForLambda(int val1, int val2)
        {
            return val1 * val2;
        }

        private static int Multiply(int x)
        {
            return 2 * x;
        }

        #endregion

        #region === Event Handlers ===

        private static void Process_StartedCore(object sender, EventArgs e)
        {
            Console.WriteLine("Ricevuto StartedCore");
        }

        private static void CompletedProcess(int duration)
        {
            Console.WriteLine($"Process Completed (duration: {duration})");
        }

        private static void Process_Started1()
        {
            Console.WriteLine("Altro Handler");
        }

        private static void Process_Started()
        {
            Console.WriteLine("Ricevuto - Processo avviato!");
        }

        #endregion
    }

    #region === OTHER CLASSES ( DON'T DO IT IN PRODUCTION :D ) ===

    internal class MyString
    {
        public string Value { get; set; }

        public string ToUpper()
        {
            return Value.ToUpper();
        }
    }

    #region Generics

    internal class Employee<T>
    {
        public T ID { get; set; }

        public string Name { get; set; }
    }

    internal class Employee {
        public Employee() { }
        //public Employee(string name, int age) { }
        public Employee(string name) { Name = name; }

        public string Name { get; set; }

        public string Department { get; set; }
        public string Country { get; set; }
    }
    internal class EmployeeInt : Employee
    {
        public EmployeeInt(): base("") { }
        public EmployeeInt(string name): base(name) { }

        public static EmployeeInt Empty = new EmployeeInt();

        private int _id;
        public int ID
        {
            get
            {
                return _id;
            }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("ID must be greater than zero.");
                _id = value;
            }
        }
    }

    internal class EmployeeString
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }

    #endregion

    #endregion
}
