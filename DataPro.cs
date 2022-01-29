using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static System.Console;
using ConsoleTables;

namespace Ezed
{
    struct DataPro
    {
        private WorkerS[] workers;

        private string path;

        int index;
        string[] titles;


        public DataPro(string Path)
        {
            this.path = Path;
            this.index = 0;
            this.titles = new string[6];
            this.workers = new WorkerS[2];
        }

        private void Resize1(bool Flag)
        {
            if (Flag)
            {
                Array.Resize(ref this.workers, this.workers.Length * 2);
            }
        }

        public void Add(WorkerS ConWor)
        {
            this.Resize1(index >= this.workers.Length);
            this.workers[index] = ConWor;
            this.index++;
        }
        
        public void Load()
        {
            using (StreamReader sr = new StreamReader(this.path))
            {
                titles = sr.ReadLine().Split('_');

                while (!sr.EndOfStream)
                {
                    string[] wor = sr.ReadLine().Split('_');

                    Add(new WorkerS(int.Parse(wor[0]), DateTime.ParseExact(wor[1], "dd.MM.yyyy HH:mm:ss", null), wor[2], byte.Parse(wor[3]), byte.Parse(wor[4]), wor[5], wor[6]));
                }
            }
        }

        public void PrintDataToConsole()
        {
            var table = new ConsoleTable($"{this.titles[0]}", $"{this.titles[1]}", $"{this.titles[2]}", $"{this.titles[3]}", $"{this.titles[4]}", $"{this.titles[5]}", $"{this.titles[6]}");
            for (int i = 0; i < index; i++)
            {
                table.AddRow(this.workers[i].Print().Split('_'));
                
            }
            table.Write(Format.Alternative);
        }

        public int Count { get { return this.index; } }

        public void AddUserToDB(string FIO, byte Age, byte Height, string BrithData, string BirthPlace)
        {
            DateTime DTN = DateTime.Now;
            int NumWor = index;
            Add(new WorkerS(NumWor, DTN, FIO, Age, Height, BrithData, BirthPlace));
            SaveDataToDisk();
        }
        
        public void SaveDataToDisk()
        {
            File.WriteAllText(path, String.Empty);
            using (StreamWriter start = new StreamWriter(path))
            {
                for (int i = 0; i < titles.Length; i++)
                {
                    start.Write(titles[i]);
                    Write("_");
                }
                foreach (WorkerS wor in workers)
                {
                    start.WriteLine(wor.Print());
                }
            }
        }
    }
}
