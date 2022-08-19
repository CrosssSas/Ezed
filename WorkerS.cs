using System;


namespace Ezed
{
    struct WorkerS
    {
        public int ID { get; set; } //ID

        public DateTime DTN { get; set; } //Дата создания

        public string FIO { get; set; } //ФИО

        public byte Age { get; set; } //Возраст

        public byte Height { get; set; } //Рост

        public string BirthDate { get; set; } //Дата рождения

        public string BirthPlace { get; set; } //Место рождения


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


        public string Print() //Вспомогательный метод при выводе в консоль
        {
            return $"{ID}_{DTN.ToString(System.Globalization.CultureInfo.CreateSpecificCulture("en-US"))}_{FIO}_{Age}_{Height}_{BirthDate}_{BirthPlace}";
        }
    }
}
