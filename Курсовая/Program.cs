using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсовая
{
    class Program
    {
        static void Main(string[] args)
        {
            General db = new General();
            bool firstMenu = true;

            while (firstMenu)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("ПЕРВОЕ МЕНЮ");
                Console.ResetColor();
                Console.WriteLine("[1] Считывание данных");
                Console.WriteLine("[2] Добавление клиента");
                Console.WriteLine("[3] Добавление служащего");
                Console.WriteLine("[4] Добавление препарата");
                Console.WriteLine("[5] Перейти в главное меню");

                switch (Console.ReadKey(true).KeyChar)
                {
                    case '1':
                        try
                        {
                            db.Customers = System.IO.File.ReadAllLines("Покупатели.txt").Select(Customer.FromFile).ToList();
                            db.Employees = System.IO.File.ReadAllLines("Сотрудники.txt").Select(Employee.FromFile).ToList();
                            Medicine.LoadMedicines(db.Medicines);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Считывание удалось");
                        }
                        catch
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Считывание не удалось");
                        }
                        Console.ResetColor();
                        Console.Write("Нажмите Enter для продолжения...");
                        Console.ReadLine();
                        break;
                    case '2':
                        var customer = Customer.AddCustomer();
                        db.Customers.Add(customer);
                        customer.Save();
                        db.Customers = System.IO.File.ReadAllLines("Покупатели.txt").Select(Customer.FromFile).ToList();
                        Console.WriteLine("Новый клиент успешно добавлен");
                        Console.Write("Нажмите Enter для продолжения...");
                        Console.ReadLine();
                        break;
                    case '3':
                        var employee = Employee.AddEmployee();
                        db.Employees.Add(employee);
                        employee.Save();
                        Console.WriteLine("Новый служащий успешно добавлен");
                        Console.Write("Нажмите Enter для продолжения...");
                        Console.ReadLine();
                        break;
                    case '4':
                        var med = Medicine.AddMedicine();
                        db.Medicines.Add(med);
                        med.Save();
                        db.Medicines = System.IO.File.ReadAllLines("Препараты.txt").Select(Medicine.FromFile).ToList();
                        Console.WriteLine("Препарат успешно добавлен");
                        Console.Write("Нажмите Enter для продолжения...");
                        Console.ReadLine();
                        break;
                    case '5':
                        firstMenu = false;
                        break;
                }
            }

            bool mainMenu = true;
            while (mainMenu)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("ГЛАВНОЕ МЕНЮ");
                Console.ResetColor();
                Console.WriteLine("[1] Покупатели");
                Console.WriteLine("[2] Сотрудники");
                Console.WriteLine("[3] Препараты");
                Console.WriteLine("[4] Выход из программы");

                switch (Console.ReadKey(true).KeyChar)
                {
                    case '1':
                        Console.Clear();
                        foreach (var c in db.Customers) c.Print();
                        Console.Write("Нажмите Enter для возврата...");
                        Console.ReadLine();
                        break;
                    case '2':
                        Console.Clear();
                        foreach (var e in db.Employees) e.Print();
                        Console.Write("Нажмите Enter для возврата...");
                        Console.ReadLine();
                        break;
                    case '3':
                        Console.Clear();
                        Medicine.ShowAll(db.Medicines);
                        break;
                    case '4':
                        mainMenu = false;
                        break;
                }
                Console.ReadKey();
            }
        }
    }
}