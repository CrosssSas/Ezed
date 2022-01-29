using System;
using static System.Console;
using System.IO;

namespace Ezed
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"db.txt";

            DataPro data = new DataPro(path);
            data.Load();
            data.PrintDataToConsole();

            WriteLine("Какое действие нужно выполнить?");
            WriteLine("(Добавить запись - 1, ...)");
            sbyte chose = sbyte.Parse(ReadLine());

            if (chose == 1)
            {
                WriteLine("Введите ФИО:");
                string FIO = ReadLine();
                WriteLine("Введите Возраст:");
                byte Age = byte.Parse(ReadLine());
                WriteLine("Введите Рост:");
                byte Height = byte.Parse(ReadLine());
                WriteLine("Введите Дату рождения:");
                string BirthDate = ReadLine();
                WriteLine("Введите Место рождения:");
                string BirthPlace = ReadLine();

                data.AddUserToDB(FIO, Age, Height, BirthDate, BirthPlace);
            }





            ReadKey();
        }
    }
}
