using static System.Console;
using System.Linq;

namespace Ezed
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"db.txt";

            string addCount = "y";

            DataPro data = new DataPro(path);
            data.Load();

            WriteLine();
            WriteLine("Какое действие нужно выполнить?");
            WriteLine("(Добавить запись - 1, Удалить запись - 2, Редактировать запись - 3, Сортировать записи - 4)");
            sbyte chose = sbyte.Parse(ReadLine());

            if (chose == 1)
            {
                while (addCount == "y")
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
                    WriteLine("Хотите добавить ещё запись? y/n");
                    addCount = ReadLine();
                }
            }
            else if(chose == 2)
            {
                WriteLine("Введите ID работника, которого хотите удалить из базы:");
                int DelNum = int.Parse(ReadLine());

                data.WorkDeler(DelNum);
                data.SaveDataToDisk();
                WriteLine("Запись успешно удалена!");
                data.index = 0;
                data.Load();

            }
            else if(chose == 3)
            {
                WriteLine("Введите ID работника, которого хотите редактировать:");
                int EditNum = int.Parse(ReadLine());

                WriteLine("Введите номер поля, которое нужно изменить:");
                WriteLine("1-FIO/2-Age/3-Height/4-BirthDate/5-BirthPlace");
                int EditField = int.Parse(ReadLine());

                data.Edit(EditNum, EditField);
                data.SaveDataToDisk();

            }
            else if(chose == 4)
            {
                WriteLine("Сортировать по выбраному полю - 1, Вывести диапазон по датам создания - 2:");
                sbyte chose2 = sbyte.Parse(ReadLine());
                if (chose2 == 1)
                {
                    WriteLine("Введите номер поля, по которому нужно остортировать:");
                    WriteLine("1-ID/2-Дата создания/3-FIO/4-Age/5-Height/6-BirthDate/7-BirthPlace");
                    sbyte chose3 = sbyte.Parse(ReadLine());

                    WriteLine();
                    WriteLine("Выберите порядок сортировки:");
                    WriteLine("Возрастание - 1, Убывание - 2");
                    sbyte chose4 = sbyte.Parse(ReadLine());

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
                    WriteLine("Сохранить данные в таком виде? y/n");
                    string decision = ReadLine();
                    
                    if (decision == "y" | decision == "Y")
                    {
                        data.SaveDataToDisk();
                    }
                    else if (decision == "n" | decision == "N")
                    {
                       
                    }
                }
                else if(chose2 == 2)
                {

                }

            }

            ReadKey();

            
        }
    }
}
