using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсовая
{
    class Customer
    {
        public int Key { get; set; }
        public string FIO { get; set; }
        public int Number_pasport { get; set; }
        public string Where { get; set; }
        public int Number { get; set; }
        public string Place { get; set; }
        public int Kol_vo_days { get; set; }
        public DateTime Date { get; set; }

        public Customer(int key, string fio, int passport, string where, int number, string place, int kolvo, DateTime date)
        {
            Key = key;
            FIO = fio;
            Number_pasport = passport;
            Where = where;
            Number = number;
            Place = place;
            Kol_vo_days = kolvo;
            Date = date;
        }

        public static Customer AddCustomer()
        {
            Console.Write("Введите ID: ");
            int key = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите ФИО: ");
            string fio = Console.ReadLine();
            Console.Write("Введите номер паспорта: ");
            int passport = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите город: ");
            string city = Console.ReadLine();
            Console.Write("Введите номер препарата: ");
            int number = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите категорию: ");
            string place = Console.ReadLine();
            Console.Write("Введите кол-во упаковок: ");
            int kolvo = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите дату покупки (yyyy-MM-dd): ");
            DateTime date = DateTime.Parse(Console.ReadLine());

            return new Customer(key, fio, passport, city, number, place, kolvo, date);
        }

        public static Customer FromFile(string line)
        {
            var parts = line.Split(';');
            return new Customer(int.Parse(parts[0]), parts[1], int.Parse(parts[2]), parts[3], int.Parse(parts[4]), parts[5], int.Parse(parts[6]), DateTime.Parse(parts[7]));
        }

        public void Print()
        {
            Console.WriteLine($"ID:{Key},ФИО:{FIO},Номер паспорта:{Number_pasport},Откуда:{Where}");
            Console.WriteLine($"Номер:{Number},Место:{Place},Кол-во упаковок:{Kol_vo_days},Дата покупки:{Date:yyyy-MM-dd}");
        }

        public void Save()
        {
            using (StreamWriter writer = new StreamWriter("Покупатели.txt", true))
            {
                writer.WriteLine($"{Key};{FIO};{Number_pasport};{Where};{Number};{Place};{Kol_vo_days};{Date:yyyy-MM-dd}");
            }
        }

        public static void City(List<Customer> customers)
        {
            Console.Write("Введите название города: ");
            string cities = Console.ReadLine();
            bool found = false;
            foreach (var elem in customers)
            {
                if (elem.Where == cities)
                {
                    elem.Print();
                    found = true;
                }
            }
            if (!found)
                Console.WriteLine("Нет клиентов из указанного города.");
        }

        public void Single()
        {
            if (Place == "Одноместный")
                Print();
        }

        public static void CalculateTotalCost(List<Customer> customers, List<Medicine> medicines)
        {
            int totalCost = 0;
            foreach (var customer in customers)
            {
                var medicine = medicines.FirstOrDefault(m => m.Number == customer.Number);
                if (medicine != null)
                {
                    totalCost += medicine.Cost_per_day * customer.Kol_vo_days;
                }
            }
            Console.WriteLine($"Общая сумма покупок всех клиентов: {totalCost} руб.");
        }

        public static void UpdateCustomer(List<Customer> customers, List<Medicine> medicines)
        {
            Console.Write("Введите ID клиента для обновления: ");
            int key = Convert.ToInt32(Console.ReadLine());
            var customer = customers.FirstOrDefault(c => c.Key == key);
            if (customer == null)
            {
                Console.WriteLine("Клиент с таким ID не найден.");
                return;
            }

            Console.Write("Введите новое ФИО (оставьте пустым, чтобы не менять): ");
            string newFio = Console.ReadLine();
            if (!string.IsNullOrEmpty(newFio))
                customer.FIO = newFio;

            Console.Write("Введите новый город (оставьте пустым, чтобы не менять): ");
            string newCity = Console.ReadLine();
            if (!string.IsNullOrEmpty(newCity))
                customer.Where = newCity;

            Console.Write("Введите новый номер препарата (оставьте пустым, чтобы не менять): ");
            string newNumber = Console.ReadLine();
            if (!string.IsNullOrEmpty(newNumber))
                customer.Number = int.Parse(newNumber);

            Console.Write("Введите новое количество упаковок (оставьте пустым, чтобы не менять): ");
            string newKolvo = Console.ReadLine();
            if (!string.IsNullOrEmpty(newKolvo))
                customer.Kol_vo_days = int.Parse(newKolvo);

            File.WriteAllLines("Покупатели.txt", customers.Select(c => $"{c.Key};{c.FIO};{c.Number_pasport};{c.Where};{c.Number};{c.Place};{c.Kol_vo_days};{c.Date:yyyy-MM-dd}"));
            Console.WriteLine("Данные клиента обновлены.");
        }
    }
}