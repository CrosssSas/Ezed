using static System.Console;
using System.Linq;
using System;
using System.Globalization;
using System.IO;
using System.Threading;

namespace Ezed
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"db.txt"; //Название файла хранения

            if (File.Exists(path) == false) //Создание файла и зполнение заголовков в случае отсутствия
            {
                var myFile = File.Create(path); //Создаю файл
                myFile.Close();

                using (StreamWriter SW = new StreamWriter(path)) //Запись заголовков
                {
                    SW.WriteLine("ID_Дата создания_ФИО_Возраст_Рост_Дата рождения_Место рождения");
                }
            }


            string addCount = "y"; //Переменная управления функционала

            string addCountMain = "y"; //Основная переменная управления

            DataPro data = new DataPro(path); 
            data.Load();
            data.PrintDataToConsole();

            while (addCountMain == "y" || addCountMain == "Y")
            {
                WriteLine();
                WriteLine("Какое действие нужно выполнить?");
                WriteLine("Добавить запись - 1\nУдалить запись - 2\nРедактировать запись - 3\nСортировать записи - 4\nНайти запись по ID - 5\nВыход - 0");
                sbyte chose = sbyte.Parse(ReadLine());

                if (chose == 1)
                {
                    while (addCount == "y" || addCount == "Y")
                    {
                        WriteLine("(Для отмены действия введите 0)\nВведите ФИО:");
                        string FIO = ReadLine();
                        if (FIO == "0")
                            break;
                        WriteLine("(Для отмены действия введите 0)\nВведите Возраст:");
                        byte Age = byte.Parse(ReadLine());
                        if (Age == 0)
                            break;
                        WriteLine("(Для отмены действия введите 0)\nВведите Рост:");
                        byte Height = byte.Parse(ReadLine());
                        if (Height == 0)
                            break;
                        WriteLine("(Для отмены действия введите 0)\nВведите Дату рождения:");
                        string BirthDate = ReadLine();
                        if (BirthDate == "0")
                            break;
                        WriteLine("(Для отмены действия введите 0)\nВведите Место рождения:");
                        string BirthPlace = ReadLine();
                        if (BirthPlace == "0")
                            break;

                        data.AddUserToDB(FIO, Age, Height, BirthDate, BirthPlace);
                        data.PrintDataToConsole();
                        WriteLine("Хотите добавить ещё запись? y/n");
                        addCount = ReadLine();
                    }
                    if (addCount != "y" || addCount != "Y")
                        addCount = "y";
                    continue;
                }
                else if (chose == 2)
                {
                    while (addCount == "y" || addCount == "Y")
                    {
                        WriteLine("(Для отмены действия введите 0)\nВведите ID работника, которого хотите удалить из базы:");
                        int DelNum = int.Parse(ReadLine());
                        if (DelNum == 0)
                            break;
                        data.WorkDeler(DelNum);
                        data.SaveDataToDisk();
                        WriteLine("Запись успешно удалена!");
                        data.index = 0;
                        data.Load();
                        data.PrintDataToConsole();
                        WriteLine("Хотите удалить ещё запись? y/n");
                        addCount = ReadLine();
                    }
                    if (addCount != "y" || addCount != "Y")
                        addCount = "y";
                    continue;
                }
                else if (chose == 3)
                {
                    while (addCount == "y" || addCount == "Y")
                    {
                        WriteLine("(Для отмены действия введите 0)\nВведите ID работника, которого хотите редактировать:");
                        int EditNum = int.Parse(ReadLine());
                        if (EditNum == 0)
                            break;

                        WriteLine("(Для отмены действия введите 0)\nВведите номер поля, которое нужно изменить:");
                        WriteLine("ФИО - 1\nВозраст - 2\nРост - 3\nДата рождения - 4\nМесто рождения - 5");
                        int EditField = int.Parse(ReadLine());
                        if (EditField == 0)
                            break;

                        data.Edit(EditNum, EditField);
                        data.SaveDataToDisk();
                        data.PrintDataToConsole();

                        WriteLine("Хотите редактировать ещё запись? y/n");
                        addCount = ReadLine();
                    }
                    if (addCount != "y" || addCount != "Y")
                        addCount = "y";
                    continue;
                }
                else if (chose == 4)
                {
                    while (addCount == "y" || addCount == "Y")
                    {
                        WriteLine("(Для отмены действия введите 0)\nСортировать по выбраному полю - 1\nВывести диапазон по датам создания - 2:");
                        sbyte chose2 = sbyte.Parse(ReadLine());
                        if (chose2 == 0)
                            break;
                        if (chose2 == 1)
                        {
                            WriteLine("Введите номер поля, по которому нужно остортировать:");
                            WriteLine("ID - 1\nДата создания - 2\nФИО - 3\nВозраст - 4\nРост - 5\nДата рождения - 6\nМесто рождения - 7");
                            sbyte chose3 = sbyte.Parse(ReadLine());
                            if (chose3 == 0)
                                break;

                            WriteLine();
                            WriteLine("Выберите порядок сортировки:");
                            WriteLine("Возрастание - 1\nУбывание - 2");
                            sbyte chose4 = sbyte.Parse(ReadLine());
                            if (chose4 == 0)
                                break;

                            if (chose4 == 1)
                            {
                                if (chose3 == 1)
                                {
                                    data.workers = data.workers.OrderBy(w => w.ID).ToArray();
                                }
                                else if (chose3 == 2)
                                {
                                    data.workers = data.workers.OrderBy(w => w.DTN).ToArray();
                                }
                                else if (chose3 == 3)
                                {
                                    data.workers = data.workers.OrderBy(w => w.FIO).ToArray();
                                }
                                else if (chose3 == 4)
                                {
                                    data.workers = data.workers.OrderBy(w => w.Age).ToArray();
                                }
                                else if (chose3 == 5)
                                {
                                    data.workers = data.workers.OrderBy(w => w.Height).ToArray();
                                }
                                else if (chose3 == 6)
                                {
                                    data.workers = data.workers.OrderBy(w => w.BirthDate).ToArray();
                                }
                                else if (chose3 == 7)
                                {
                                    data.workers = data.workers.OrderBy(w => w.BirthPlace).ToArray();
                                }
                            }
                            else if (chose4 == 2)
                            {
                                if (chose3 == 1)
                                {
                                    data.workers = data.workers.OrderByDescending(w => w.ID).ToArray();
                                }
                                else if (chose3 == 2)
                                {
                                    data.workers = data.workers.OrderByDescending(w => w.DTN).ToArray();
                                }
                                else if (chose3 == 3)
                                {
                                    data.workers = data.workers.OrderByDescending(w => w.FIO).ToArray();
                                }
                                else if (chose3 == 4)
                                {
                                    data.workers = data.workers.OrderByDescending(w => w.Age).ToArray();
                                }
                                else if (chose3 == 5)
                                {
                                    data.workers = data.workers.OrderByDescending(w => w.Height).ToArray();
                                }
                                else if (chose3 == 6)
                                {
                                    data.workers = data.workers.OrderByDescending(w => w.BirthDate).ToArray();
                                }
                                else if (chose3 == 7)
                                {
                                    data.workers = data.workers.OrderByDescending(w => w.BirthPlace).ToArray();
                                }
                            }

                            WriteLine("Успешно отсортировано:");
                            data.PrintDataToConsole();
                            WriteLine("Хотите отсортировать иначе? y/n");
                            string dec = ReadLine();
                            if (dec == "y" || dec == "Y")
                                continue;
                            WriteLine("Сохранить данные в таком виде? y/n");
                            string decision = ReadLine();

                            if (decision == "y" | decision == "Y")
                            {
                                data.SaveDataToDisk();
                                WriteLine("Сохранено успешно!");
                            }

                            break;
                        }
                        else if (chose2 == 2)
                        {
                            WriteLine("(Для отмены действия введите 0)\nВведите номер поля, по которому нужно вывести диапазон:");
                            WriteLine("ID - 1\nДата создания - 2\nВозраст - 3\nРост - 4");
                            sbyte chose5 = sbyte.Parse(ReadLine());
                            if (chose5 == 0)
                                break;

                            if (chose5 == 1)
                            {
                                WriteLine("(Для отмены действия введите 0)\nВведите нижнюю границу диапазона");
                                int firstBorder = int.Parse(ReadLine());
                                if (firstBorder == 0)
                                    break;
                                WriteLine("(Для отмены действия введите 0)\nВведите верхнюю границу диапазона");
                                int lastBorder = int.Parse(ReadLine());
                                if (lastBorder == 0)
                                    break;

                                data.workers = data.workers.OrderBy(w => w.ID).ToArray();
                                data.RangePrinter(firstBorder, lastBorder);
                                data.PrintDataToConsoleRange();
                                WriteLine("Хотите выбрать другие параметры? y/n");
                                string dec = ReadLine();
                                if (dec == "y" || dec == "Y")
                                    continue;

                                break;
                            }
                            else if (chose5 == 2)
                            {
                                WriteLine("(Для отмены действия введите 0)\nВведите нижнюю границу диапазона");
                                WriteLine("Введите число");
                                sbyte dAtA = sbyte.Parse(ReadLine());
                                if (dAtA == 0)
                                    break;
                                WriteLine("Введите месяц");
                                sbyte MoUnTh = sbyte.Parse(ReadLine());
                                if (MoUnTh == 0)
                                    break;
                                WriteLine("Введите год");
                                short YeAr = short.Parse(ReadLine());
                                if (YeAr == 0)
                                    break;

                                WriteLine("(Для отмены действия введите 0)\nВведите верхнюю границу диапазона");
                                WriteLine("Введите число");
                                sbyte dAtA1 = sbyte.Parse(ReadLine());
                                if (dAtA1 == 0)
                                    break;
                                WriteLine("Введите месяц");
                                sbyte MoUnTh1 = sbyte.Parse(ReadLine());
                                if (MoUnTh1 == 0)
                                    break;
                                WriteLine("Введите год");
                                short YeAr1 = short.Parse(ReadLine());
                                if (YeAr1 == 0)
                                    break;

                                data.workers = data.workers.OrderBy(w => w.DTN).ToArray();
                                data.RangePrinter(dAtA, MoUnTh, YeAr, dAtA1, MoUnTh1, YeAr1);
                                data.PrintDataToConsoleRange();

                                WriteLine("Хотите выбрать другие параметры? y/n");
                                string dec = ReadLine();
                                if (dec == "y" || dec == "Y")
                                    continue;

                                break;
                            }
                            else if (chose5 == 3)
                            {
                                WriteLine("Введите нижнюю границу диапазона");
                                byte firstBorder3 = byte.Parse(ReadLine());
                                if (firstBorder3 == 0)
                                    break;
                                WriteLine("Введите верхнюю границу диапазона");
                                byte lastBorder3 = byte.Parse(ReadLine());
                                if (lastBorder3 == 0)
                                    break;

                                data.workers = data.workers.OrderBy(w => w.Age).ToArray();
                                data.RangePrinter(firstBorder3, lastBorder3);
                                data.PrintDataToConsoleRange();

                                WriteLine("Хотите выбрать другие параметры? y/n");
                                string dec = ReadLine();
                                if (dec == "y" || dec == "Y")
                                    continue;

                                break;
                            }
                            else if (chose5 == 4)
                            {
                                WriteLine("Введите нижнюю границу диапазона");
                                byte firstBorder4 = byte.Parse(ReadLine());
                                if (firstBorder4 == 0)
                                    break;
                                WriteLine("Введите верхнюю границу диапазона");
                                byte lastBorder4 = byte.Parse(ReadLine());
                                if (lastBorder4 == 0)
                                    break;

                                data.workers = data.workers.OrderBy(w => w.Height).ToArray();
                                data.RangePrinter2(firstBorder4, lastBorder4);
                                data.PrintDataToConsoleRange();

                                WriteLine("Хотите выбрать другие параметры? y/n");
                                string dec = ReadLine();
                                if (dec == "y" || dec == "Y")
                                    continue;

                                break;
                            }
                        }
                    }
                    if (addCount != "y" || addCount != "Y")
                        addCount = "y";
                    continue;
                }
                else if (chose == 5)
                {
                    WriteLine("(Для отмены действия введите 0)\nВведите ID:");
                    int iD = int.Parse(ReadLine());
                    if (iD == 0)
                        break;

                    data.IdChecker(iD);
                }
                else if (chose == 0)
                {
                    WriteLine();
                    WriteLine("До скорого ;)");
                    break;
                }
                
            }

            
        }
    }
}
