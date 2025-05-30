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
            bool inLoadBase = true;
            bool inFirstMenu = false;  // первое меню
            bool inMainMenu = false;  // главное меню
            bool inCustomerMenu = false;  // меню покупателей
            bool inEmployeeMenu = false;  //  меню сотрудников
            bool inMedicineMenu = false;  //  меню препаратов
            bool running = true; 

            while (running)
            {
                if (inLoadBase)
                { bool Fload = false;
                  bool Fload1 = false;
                  bool Fload2 = false;
                  bool Fload3 = false;

                    Console.Clear();
                    Console.WriteLine("Загружаем данные... ");


                    try
                    {
                        db.Customers = System.IO.File.ReadAllLines("Покупатели.txt").Select(Customer.FromFile).ToList();
                        db.Employees = System.IO.File.ReadAllLines("Сотрудники.txt").Select(Employee.FromFile).ToList();
                        Medicine.LoadMedicines(db.Medicines);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Данные загружены");
                        Fload = true;
                    }
                    catch
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Загрузка не удалась...");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        
                          var path1 = "Покупатели.txt";
                          var exist1 = File.Exists(path1);
                          if (!exist1)
                          {
                              Console.WriteLine("Файл Покупатели.txt не найден. Создать его?(Y/N)");
                              char choice1 = Console.ReadKey(true).KeyChar;
                              if (choice1 == 'Y' || choice1 == 'y')
                              {
                                  File.Create("Покупатели.txt").Close();
                                  Fload1 = true;
                              }
                          }
                          
                          var path2 = "Сотрудники.txt";
                          var exist2 = File.Exists(path2);
                          if (!exist2)
                          {
                              Console.WriteLine("Файл Сотрудники.txt не найден. Создать его?(Y/N)");
                              char choice2 = Console.ReadKey(true).KeyChar;
                              if (choice2 == 'Y' || choice2 == 'y')
                              {
                                  File.Create("Сотрудники.txt").Close();
                                  Fload2 = true;
                              }
                          }
                          var path3 = "Препараты.txt";
                          var exist3 = File.Exists(path3);
                          if (!exist3)
                          {
                              Console.WriteLine("Файл Препараты.txt не найден. Создать его?(Y/N)");
                              char choice3 = Console.ReadKey(true).KeyChar;
                              if (choice3 == 'Y' || choice3 == 'y')
                              {
                                  File.Create("Препараты.txt").Close();
                                  Fload3 = true;
                              }
                          }

                          Fload = Fload1 && Fload2 && Fload3;

                          }

                         
                        if (Fload)
                        {
                            Console.ResetColor();
                            Console.Write("Нажмите Enter для продолжения...");
                            Console.ReadLine();
                            inFirstMenu = true;
                            inLoadBase = false;
                        }
                    }
                
                // Первое меню
                if (inFirstMenu)
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

                    char choice = Console.ReadKey(true).KeyChar;
                    if (choice == '1')
                    {
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
                    }
                    else if (choice == '2')
                    {
                        var customer = Customer.AddCustomer();
                        db.Customers.Add(customer);
                        customer.Save();
                        db.Customers = System.IO.File.ReadAllLines("Покупатели.txt").Select(Customer.FromFile).ToList();
                        Console.WriteLine("Новый клиент добавлен");
                        Console.Write("Нажмите Enter для продолжения...");
                        Console.ReadLine();
                    }
                    else if (choice == '3')
                    {
                        var employee = Employee.AddEmployee();
                        db.Employees.Add(employee);
                        employee.Save();
                        Console.WriteLine("Новый служащий добавлен");
                        Console.Write("Нажмите Enter для продолжения...");
                        Console.ReadLine();
                    }
                    else if (choice == '4')
                    {
                        var med = Medicine.AddMedicine();
                        db.Medicines.Add(med);
                        med.Save();
                        db.Medicines = System.IO.File.ReadAllLines("Препараты.txt").Select(Medicine.FromFile).ToList();
                        Console.WriteLine("Препарат добавлен");
                        Console.Write("Нажмите Enter для продолжения...");
                        Console.ReadLine();
                    }
                    else if (choice == '5')
                    {
                        inFirstMenu = false;
                        inMainMenu = true;
                    }
                }

                // Главное меню
                else if (inMainMenu)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("ГЛАВНОЕ МЕНЮ");
                    Console.ResetColor();
                    Console.WriteLine("[1] Покупатели");
                    Console.WriteLine("[2] Сотрудники");
                    Console.WriteLine("[3] Препараты");
                    Console.WriteLine("[4] Общая сумма покупок клиентов");
                    Console.WriteLine("[5] Обновить данные клиента");
                    Console.WriteLine("[6] Выход из программы");
                    Console.WriteLine("[7] Вернуться в первое меню");

                    char choice = Console.ReadKey(true).KeyChar;
                    if (choice == '1')
                    {
                        inMainMenu = false;
                        inCustomerMenu = true;
                    }
                    else if (choice == '2')
                    {
                        inMainMenu = false;
                        inEmployeeMenu = true;
                    }
                    else if (choice == '3')
                    {
                        inMainMenu = false;
                        inMedicineMenu = true;
                    }
                    else if (choice == '4')
                    {
                        Console.Clear();
                        Customer.CalculateTotalCost(db.Customers, db.Medicines);
                        Console.Write("Нажмите Enter для возврата...");
                        Console.ReadLine();
                    }
                    else if (choice == '5')
                    {
                        Console.Clear();
                        Customer.UpdateCustomer(db.Customers, db.Medicines);
                        Console.Write("Нажмите Enter для возврата...");
                        Console.ReadLine();
                    }
                    else if (choice == '6')
                    {
                        running = false;
                    }
                    else if (choice == '7')
                    {
                        inMainMenu = false;
                        inFirstMenu = true;
                    }
                }

                // Меню покупателей
                else if (inCustomerMenu)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("МЕНЮ ПОКУПАТЕЛЕЙ");
                    Console.ResetColor();
                    Console.WriteLine("[1] Вывод всех покупателей");
                    Console.WriteLine("[2] Поиск покупателей по городу");
                    Console.WriteLine("[3] Вывод покупателей по категории");
                    Console.WriteLine("[4] Выход в главное меню");

                    char choice = Console.ReadKey(true).KeyChar;
                    if (choice == '1')
                    {
                        Console.Clear();
                        foreach (var c in db.Customers) c.Print();
                        Console.Write("Нажмите Enter для возврата...");
                        Console.ReadLine();
                    }
                    else if (choice == '2')
                    {
                        Console.Clear();
                        Customer.City(db.Customers);
                        Console.Write("Нажмите Enter для возврата...");
                        Console.ReadLine();
                    }
                    else if (choice == '3')
                    {
                        Console.Clear();
                        foreach (var c in db.Customers) c.Single();
                        Console.Write("Нажмите Enter для возврата...");
                        Console.ReadLine();
                    }
                    else if (choice == '4')
                    {
                        inCustomerMenu = false;
                        inMainMenu = true;
                    }
                }

                // Меню сотрудников
                else if (inEmployeeMenu)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("МЕНЮ СОТРУДНИКОВ");
                    Console.ResetColor();
                    Console.WriteLine("[1] Вывод всех сотрудников");
                    Console.WriteLine("[2] Поиск сотрудников по дню и участку");
                    Console.WriteLine("[3] Удаление сотрудника");
                    Console.WriteLine("[4] Вывод сотрудников по дню недели");
                    Console.WriteLine("[5] Выход в главное меню");

                    char choice = Console.ReadKey(true).KeyChar;
                    if (choice == '1')
                    {
                        Console.Clear();
                        foreach (var e in db.Employees) e.Print();
                        Console.Write("Нажмите Enter для возврата...");
                        Console.ReadLine();
                    }
                    else if (choice == '2')
                    {
                        Console.Clear();
                        if (db.Employees.Count > 0)
                            db.Employees[0].Cleaning();
                        else
                            Console.WriteLine("Список сотрудников пуст.");
                        Console.Write("Нажмите Enter для возврата...");
                        Console.ReadLine();
                    }
                    else if (choice == '3')
                    {
                        Console.Clear();
                        Employee.Delete(db.Employees);
                    }
                    else if (choice == '4')
                    {
                        Console.Clear();
                        Employee.ShowByDay(db.Employees);
                    }
                    else if (choice == '5')
                    {
                        inEmployeeMenu = false;
                        inMainMenu = true;
                    }
                }

                // Меню препаратов
                else if (inMedicineMenu)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("МЕНЮ ПРЕПАРАТОВ");
                    Console.ResetColor();
                    Console.WriteLine("[1] Вывод всех препаратов");
                    Console.WriteLine("[2] Поиск стоимости по номеру");
                    Console.WriteLine("[3] Фильтрация препаратов по категории");
                    Console.WriteLine("[4] Выход в главное меню");

                    char choice = Console.ReadKey(true).KeyChar;
                    if (choice == '1')
                    {
                        Console.Clear();
                        Medicine.ShowAll(db.Medicines);
                    }
                    else if (choice == '2')
                    {
                        Console.Clear();
                        Medicine.ShowCostByNumber(db.Medicines);
                    }
                    else if (choice == '3')
                    {
                        Console.Clear();
                        Medicine.FilterByType(db.Medicines);
                    }
                    else if (choice == '4')
                    {
                        inMedicineMenu = false;
                        inMainMenu = true;
                        Console.ReadLine();
                    }
                }
            }
        }
    }
}