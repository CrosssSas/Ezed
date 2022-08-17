﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static System.Console;
using ConsoleTables;
using System.Globalization;

namespace Ezed
{
    struct DataPro
    {
        public WorkerS[] workers;

        public WorkerS[] workersCopy;

        private string path;

        public int index;
        string[] titles; //промежуточный массив временного хранения данных

        bool dbEmpty;

        int MaxValue;

        bool WorkDelerFind;

        public DataPro(string Path)
        {
            this.path = Path;
            this.index = 0;
            this.titles = new string[6];
            this.workers = new WorkerS[1];
            this.workersCopy = new WorkerS[1];
            this.dbEmpty = false;
            this.WorkDelerFind = false;
            this.MaxValue = 0;
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
                PrintDataToConsole();
                dbEmpty = false;
            }
            else
            {
                WriteLine("База данных пуста");
                dbEmpty = true;
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
                if (dbEmpty == false)
                {
                    for (int i = 0; i < titles.Length; i++)
                    {
                        start.Write(titles[i]);
                        start.Write("_");
                    }
                    start.WriteLine();
                }

                for (int i = 0; i < index; i++)
                {
                    start.WriteLine(workers[i].Print());
                }
            }
        }

        public int CheckFile(string way)
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
			
    }
}
