using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсовая
{
    class Medicine
    {
        public static Medicine AddMedicine()
        {
            Console.Write("Введите номер препарата: ");
            int number = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите номер отдела хранения: ");
            int floor = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите тип (категорию): ");
            string type = Console.ReadLine();
            Console.Write("Введите цену: ");
            int cost = Convert.ToInt32(Console.ReadLine());
            return new Medicine(number, floor, type, cost);
        }

        public void Save()
        {
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter("Препараты.txt", true))
            {
                writer.WriteLine($"{Number};{Floor};{Type};{Cost_per_day}");
            }
        }
        public int Number { get; set; }           // ID препарата
        public int Floor { get; set; }            // Участок (или склад, полка)
        public string Type { get; set; }          // Категория
        public int Cost_per_day { get; set; }     // Цена

        public Medicine(int number, int floor, string type, int cost)
        {
            Number = number;
            Floor = floor;
            Type = type;
            Cost_per_day = cost;
        }

        public static Medicine FromFile(string line)
        {
            var parts = line.Split(';');
            return new Medicine(int.Parse(parts[0]), int.Parse(parts[1]), parts[2], int.Parse(parts[3]));
        }

        public void Print()
        {
            Console.WriteLine($"Номер: {Number}, Участок: {Floor}, Тип: {Type}, Цена: {Cost_per_day} руб.");
        }

        public static void LoadMedicines(List<Medicine> medicines)
        {
            if (System.IO.File.Exists("Препараты.txt"))
            {
                foreach (var line in System.IO.File.ReadAllLines("Препараты.txt"))
                    medicines.Add(Medicine.FromFile(line));
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Препараты успешно считаны");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Файл Препараты.txt не найден");
            }
            Console.ResetColor();
        }

        public static void ShowAll(List<Medicine> medicines)
        {
            foreach (var m in medicines)
                m.Print();
            Console.Write("Информация успешно выведена. Хотите продолжить? ");
            Console.ReadLine();
        }

        public static void ShowCostByNumber(List<Medicine> medicines)
        {
            Console.Write("Введите необходимый номер: ");
            int num = Convert.ToInt32(Console.ReadLine());
            var m = medicines.FirstOrDefault(x => x.Number == num);
            if (m != null)
            {
                Console.WriteLine($"Стоимость {num} составляет {m.Cost_per_day} руб. за упаковку");
            }
            else
            {
                Console.WriteLine("Препарат с таким номером не найден");
            }
            Console.Write("Хотите продолжить? ");
            Console.ReadLine();
        }
    }
}