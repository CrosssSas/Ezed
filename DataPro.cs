using System;
using System.IO;
using static System.Console;
using ConsoleTables;
using System.Globalization;

namespace Ezed
{
    struct DataPro
    {
        public WorkerS[] workers; //массив временного хранения записей

        public WorkerS[] workersCopy; //Полностью идентичный массив для копирования записей

        private string path;

        public int index; //счётчик работников в базе на данный момент
        string[] titles; //массив временного хранения заголовков

        int MaxValue; //Хранит наибольший номер - ID
        bool WorkDelerFind; //Переменная для метода WorkDeler
        int index2; //Переменная для метода RangePrinter
        bool index3;

        public DataPro(string Path) //Инициализация
        {
            this.path = Path;
            this.index = 0;
            this.titles = new string[6];
            this.workers = new WorkerS[1];
            this.workersCopy = new WorkerS[1];
            this.WorkDelerFind = false;
            this.MaxValue = 0;
            this.index2 = 0;
            this.index3 = false;
        }

        private void Resize1(bool Flag)
        {
            if (Flag)
            {
                Array.Resize(ref this.workers, this.workers.Length + 1);
                Array.Resize(ref this.workersCopy, this.workersCopy.Length + 1);
            }
        }

        public void Add(WorkerS ConWor)
        {
            this.Resize1(index >= this.workers.Length);
            this.workers[index] = ConWor;
            this.index++;
        }

        public void Load() //Загружает даннные во временную память
        {
            int count = CheckFile(this.path);
            if (count > 0)
            {
                using (StreamReader sr = new StreamReader(this.path))
                {
                    titles = sr.ReadLine().Split('_'); 

                    while (!sr.EndOfStream)
                    {
                        string[] wor = sr.ReadLine().Split('_');

                        Add(new WorkerS(int.Parse(wor[0]), DateTime.ParseExact(wor[1], "M/dd/yyyy h:mm:ss tt", CultureInfo.CreateSpecificCulture("en-US")), wor[2], byte.Parse(wor[3]), byte.Parse(wor[4]), wor[5], wor[6]));
                    }
                }
            }
            else if(count == 0)
            {
            
                using (StreamWriter SW = new StreamWriter(path))
                {
                    SW.Write("ID_Дата создания_ФИО_Возраст_Рост_Дата рождения_Место рождения");
                }

                using (StreamReader sr = new StreamReader(this.path))
                {
                    titles = sr.ReadLine().Split('_');
                    var table = new ConsoleTable($"{this.titles[0]}", $"{this.titles[1]}", $"{this.titles[2]}", $"{this.titles[3]}", $"{this.titles[4]}", $"{this.titles[5]}", $"{this.titles[6]}");
                    table.Write(Format.Alternative);
                }


                WriteLine("База данных пуста");
            }
        }

        public void PrintDataToConsole() // Выводит данные на экран
        {
            var table = new ConsoleTable($"{this.titles[0]}", $"{this.titles[1]}", $"{this.titles[2]}", $"{this.titles[3]}", $"{this.titles[4]}", $"{this.titles[5]}", $"{this.titles[6]}");
            for (int i = 0; i < index; i++)
            {
                table.AddRow(this.workers[i].Print().Split('_'));
            }
            table.Write(Format.Alternative);
        }

        public int Count { get { return this.index; } }

        public void AddUserToDB(string FIO, byte Age, byte Height, string BrithData, string BirthPlace) // Добавляет пользователя в структуру
        {
            DateTime DTN = DateTime.Now;
            for (int i = 0; i < index; i++)
            {
                MaxValue = Math.Max(workers[i].ID, MaxValue);
            }
            int NumWor = MaxValue + 1;
            Add(new WorkerS(NumWor, DTN, FIO, Age, Height, BrithData, BirthPlace));
            SaveDataToDisk();
            MaxValue = 0;
        }
        
        public void SaveDataToDisk() // Сохраняет данные в файл
        {
            File.WriteAllText(path, String.Empty);
            using (StreamWriter start = new StreamWriter(path))
            {
                for (int i = 0; i < titles.Length; i++) //Запись заголовков
                {
                   start.Write(titles[i]);
                   if (i < 6)
                   {
                       start.Write("_");
                   }
                }
                start.WriteLine();   

                for (int i = 0; i < index; i++) //Добавление записей
                {
                    start.WriteLine(workers[i].Print());
                }
            }
        }

        public int CheckFile(string way) //Считает колиство строк в файле
        {
            string[] strok = File.ReadAllLines(way);
            int CheckCount = strok.Length;
            if (CheckCount == 1 && String.IsNullOrEmpty(strok[0]))
            {
                CheckCount = 0;
            }
            
            return CheckCount;
        }

        public void WorkDeler(int deleteNumber)  //Удаляет выбраную запись
        {
            index = index - 1;
            
            for (int i = 0; i < index; i++)
            {
                if(WorkDelerFind == false)
                {
                    if (workers[i].ID == deleteNumber)
                    {
                        i--;
                        WorkDelerFind = true;
                        continue;
                    }
                }
                
                if(WorkDelerFind)
                {
                    workersCopy[i] = workers[i+1];
                }
                else
                {
                    workersCopy[i] = workers[i];
                }
            }

            WorkDelerFind = false;

            Array.Clear(workers, 0, workers.Length);

            for (int i = 0; i < index; i++)
            {
                workers[i] = workersCopy[i];
            }

            Array.Resize(ref this.workers, this.workers.Length - 1);
            Array.Resize(ref this.workersCopy, this.workersCopy.Length - 1);
        }       
			
        public void Edit(int ID, int Field)   // Изменяет выбраное поле в записи
        {
            for (int i = 0; i < index; i++)
            {
                if (workers[i].ID == ID)
                {
                    if(Field == 1)
                    {
                        WriteLine("Введите ФИО:");
                        workers[i].FIO = ReadLine();
                    }
                    else if(Field == 2)
                    {
                        WriteLine("Введите Возраст:");
                        workers[i].Age = byte.Parse(ReadLine());
                    }
                    else if(Field == 3)
                    {
                        WriteLine("Введите Рост:");
                        workers[i].Height = byte.Parse(ReadLine());
                    }
                    else if(Field == 4)
                    {
                        WriteLine("Введите Дату рождения:");
                        workers[i].BirthDate = ReadLine();
                    }
                    else if(Field == 5)
                    {
                        WriteLine("Введите Место рождения:");
                        workers[i].BirthPlace = ReadLine();
                    }
                }
            }
        }

        public void RangePrinter(int FirstB, int LastB)  //Сохраняет данные в выбранном диапазоне в выбраную память для ID
        {
            for (int i = 0; i < index; i++)
            {
                if ((workers[i].ID >= FirstB) && (workers[i].ID <= LastB))
                {
                    workersCopy[index2] = workers[i];
                    index2++;
                }
            }
            
        }

        public void PrintDataToConsoleRange() //Выводит данные в диапазоне на экран
        {
            var table = new ConsoleTable($"{this.titles[0]}", $"{this.titles[1]}", $"{this.titles[2]}", $"{this.titles[3]}", $"{this.titles[4]}", $"{this.titles[5]}", $"{this.titles[6]}");
            for (int i = 0; i < index2; i++)
            {
                table.AddRow(this.workersCopy[i].Print().Split('_'));
            }
            table.Write(Format.Alternative);

            index2 = 0;
        }

        public void RangePrinter(byte FirstB, byte LastB) //Сохраняет данные в выбранном диапазоне в выбраную память для возраста
        {
            for (int i = 0; i < index; i++)
            {
                if ((workers[i].Age >= FirstB) && (workers[i].Age <= LastB))
                {
                    workersCopy[index2] = workers[i];
                    index2++;
                }
            }
        }

        public void RangePrinter2(byte FirstB, byte LastB) //Сохраняет данные в выбранном диапазоне в выбраную память для роста
        {
            for (int i = 0; i < index; i++)
            {
                if ((workers[i].Height >= FirstB) && (workers[i].Height <= LastB))
                {
                    workersCopy[index2] = workers[i];
                    index2++;
                }
            }
        }

        public void RangePrinter(sbyte Dat, sbyte Mount, short Year, sbyte Dat1, sbyte Mount1, short Year1) //Сохраняет данные в выбранном диапазоне в выбраную память для даты создания
        {
            string dataFstr = $"{Mount}/{Dat}/{Year} 12:00:00 AM";

            string dataLstr = $"{Mount1}/{Dat1}/{Year1} 11:59:59 PM";

            DateTime date1 = DateTime.Parse(dataFstr, CultureInfo.CreateSpecificCulture("en-US"));

            DateTime date2 = DateTime.Parse(dataLstr, CultureInfo.CreateSpecificCulture("en-US"));

            for (int i = 0; i < index; i++)
            {
                if ((workers[i].DTN >= date1) && (workers[i].DTN <= date2))
                {
                    workersCopy[index2] = workers[i];
                    index2++;
                }
            }
        }
        
        public void IdChecker(int id)  //Находит и выводит запись по ID
        {
            for (int i = 0; i < index; i++)
            {
                if (workers[i].ID == id)
                {
                    workersCopy[0] = workers[i];
                    
                    var table = new ConsoleTable($"{this.titles[0]}", $"{this.titles[1]}", $"{this.titles[2]}", $"{this.titles[3]}", $"{this.titles[4]}", $"{this.titles[5]}", $"{this.titles[6]}");
                    table.AddRow(this.workersCopy[0].Print().Split('_'));
                    table.Write(Format.Alternative);
                    index3 = true;
                    break;

                }
            }

            if (index3 == false)
            {
                WriteLine($"ID номер {id} не существует");
            }
            else
            {
                index3 = false;
            }
        }
    }   
}
