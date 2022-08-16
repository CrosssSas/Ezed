using static System.Console;

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
            WriteLine("(Добавить запись - 1, Удалить запись - 2)");
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
                WriteLine("Введите номер работника, которого хотите удалить из базы:");
                int DelNum = int.Parse(ReadLine());

                data.WorkDeler(DelNum);
                data.SaveDataToDisk();
                WriteLine("Запись успешно удалена!");
                data.index = 0;
                data.Load();

            }
            else if(chose == 3)
            {

            }

            ReadKey();

            
        }
    }
}
