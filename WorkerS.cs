using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;


namespace Ezed
{
    struct WorkerS
    {
        public int ID { get; set; }

        public DateTime DTN { get; set; }

        public string FIO { get; set; }

        public byte Age { get; set; }

        public byte Height { get; set; }

        public string BirthDate { get; set; }

        public string BirthPlace { get; set; }


        public WorkerS(int ID, DateTime DTN, string FIO, byte Age, byte Height, string BirthDate, string BirthPlace)
        {
            this.ID = ID;
            this.DTN = DTN;
            this.FIO = FIO;
            this.Age = Age;
            this.Height = Height;
            this.BirthDate = BirthDate;
            this.BirthPlace = BirthPlace;
        }


        public string Print()
        {
            return $"{ID}_{DTN}_{FIO}_{Age}_{Height}_{BirthDate}_{BirthPlace}";
        }
    }
}
