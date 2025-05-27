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
            Console.Write("Введите ключ: ");
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
            Console.WriteLine($"Ключ:{Key},ФИО:{FIO},Номер паспорта:{Number_pasport},Откуда:{Where}");
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
    }
}