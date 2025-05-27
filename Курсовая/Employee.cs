using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсовая
{
    class Employee
    {
        public int Key { get; set; }
        public string FIO { get; set; }
        public List<int> Sections { get; set; } = new List<int>();
        public List<string> WorkDays { get; set; } = new List<string>();

        public Employee(int key, string fio, List<int> sections, List<string> days)
        {
            Key = key;
            FIO = fio;
            Sections = sections;
            WorkDays = days;
        }

        public static Employee AddEmployee()
        {
            Console.Write("Введите ключ: ");
            int key = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите ФИО: ");
            string fio = Console.ReadLine();

            List<int> sections = new List<int>();
            for (int i = 0; i < 7; i++)
            {
                Console.Write("Введите участок: ");
                sections.Add(Convert.ToInt32(Console.ReadLine()));
            }

            List<string> days = new List<string>();
            for (int i = 0; i < 7; i++)
            {
                Console.Write("Введите день недели: ");
                days.Add(Console.ReadLine());
            }

            return new Employee(key, fio, sections, days);
        }

        public static Employee FromFile(string line)
        {
            var parts = line.Split(';');
            var sections = parts.Skip(2).Take(7).Select(int.Parse).ToList();
            var days = parts.Skip(9).Take(7).ToList();
            return new Employee(int.Parse(parts[0]), parts[1], sections, days);
        }

        public void Print()
        {
            Console.WriteLine($"Ключ:{Key},ФИО:{FIO}");
            foreach (var elem in Sections)
                Console.WriteLine($"Участок:{elem};");
            foreach (var elem in WorkDays)
                Console.WriteLine($"День недели:{elem}");
        }

        public void Cleaning()
        {
            Console.Write("Введите день недели: ");
            string day = Console.ReadLine();
            Console.Write("Введите участок: ");
            int section = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < WorkDays.Count; i++)
            {
                if (WorkDays[i] == day && Sections[i] == section)
                    Print();
            }
        }

        public static void Delete(List<Employee> employees)
        {
            Console.Write("Введите номер сотрудника, которого необходимо удалить: ");
            int delete = Convert.ToInt32(Console.ReadLine());
            employees.RemoveAll(x => x.Key == delete);

            string[] lines = System.IO.File.ReadAllLines("Сотрудники.txt");
            lines = lines.Where((line, index) => !line.StartsWith(delete.ToString() + ";")).ToArray();
            System.IO.File.WriteAllLines("Сотрудники.txt", lines);
            Console.WriteLine("Сотрудник удалён. Хотите продолжить?");
            Console.ReadLine();
        }

        public void Save()
        {
            using (StreamWriter writer = new StreamWriter("Сотрудники.txt", true))
            {
                writer.WriteLine($"{Key};{FIO};{string.Join(";", Sections)};{string.Join(";", WorkDays)};");
            }
        }
    }
}