using System.Globalization;
using CodingTimeLib.interfaces;
using CodingTimeLib.model;

namespace Cli.Ui;

static public class Ui
{
    private static IDbActions _db;
    private static DateTime _startTemp;
    private static DateTime _endTemp;


    public static void Menu(IDbActions db)
    {
        _db = db;

        while (true)
        {
            Console.WriteLine("--- Menu ---");
            Console.WriteLine("0 - exit");
            Console.WriteLine("1 - view all sessions");
            Console.WriteLine("2 - add new session");
            Console.WriteLine("3 - delete session");
            Console.WriteLine("4 - update session");

            var input = GetInput();
            switch (input)
            {
                case 0: return;
                case 1:
                    GetAllSessions();
                    break;
                case 2:
                    CreateNewSession();
                    break;
                case 3:
                    DeleteSession();
                    break;
                case 4:
                    UpdateSession();
                    break;

                default:
                    Console.WriteLine("I will not be able to recover from it");
                    break;
            }
        }

    }

    private static void UpdateSession()
    {
        Console.Clear();
        GetAllSessions();
        Console.WriteLine("Please enter id of session you wanna update: ");
        int id;
        CodingSession session;
        while (true)
        {
            try
            {
                id = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                Console.WriteLine("Enter start time...");
                string startTime = AskForTime("start");

                Console.Clear();
                Console.WriteLine("Enter end time...");
                string endTime = AskForTime("end");

                session = new CodingSession(startTime, endTime);
                break;
            }
            catch (System.Exception)
            {
                Console.WriteLine("It must be a number");
            }
        }
        _db.UpdateSession(id, session);
    }

    private static void DeleteSession()
    {
        Console.Clear();
        GetAllSessions();
        Console.WriteLine("Please enter id of session you wanna delete: ");
        int id;
        while (true)
        {
            try
            {
                id = Convert.ToInt32(Console.ReadLine());

                break;
            }
            catch (System.Exception)
            {
                Console.WriteLine("It must be a number");
            }
        }
        _db.DeleteSession(id);
    }

    private static string AskForTime(string whatTime)
    {
        DateTime dateVal = new DateTime();
        bool flag = true;
        while (flag)
        {
            try
            {
                Console.WriteLine("Please enter time in format yyyy/mm/dd hh:mm");

                var time = Console.ReadLine();
                CultureInfo cult = new CultureInfo("ru-RU");
                DateTime.TryParse(time, cult, DateTimeStyles.None, out dateVal);

                if (dateVal.Year != 2022)
                {
                    Console.WriteLine($"Looks like your year is incorrect plz try again ({dateVal.ToString()}) (year must be == 2022)");
                    continue;
                }

                switch (whatTime)
                {
                    case "start":
                        _startTemp = dateVal;
                        break;
                    case "end":
                        int compared = DateTime.Compare(_startTemp, dateVal);
                        if (compared >= 0)
                        {
                            Console.Clear();
                            Console.WriteLine($"End time must be latter than start time {_startTemp.ToString()} / mm.dd.yyyy 12h");
                            continue;
                        }
                        _endTemp = dateVal;
                        break;
                }
                flag = false;
                continue;
            }
            catch (System.Exception)
            {
                Console.WriteLine("Wrong format");
            }
        }

        return dateVal.ToString();
    }

    private static void CreateNewSession()
    {
        Console.Clear();
        Console.WriteLine("Enter start time...");
        string startTime = AskForTime("start");

        Console.Clear();
        Console.WriteLine("Enter end time...");
        string endTime = AskForTime("end");

        CodingSession coding = new CodingSession(startTime, endTime);
        _db.AddSession(coding);
    }

    private static int GetInput()
    {
        bool flag = true;
        int output = 666;
        while (flag)
        {
            try
            {
                output = Convert.ToInt32(Console.ReadLine());
                if (output <= 4 && output >= 0)
                {
                    flag = false;
                    continue;
                }
                Console.Clear();
                Console.WriteLine("Number must be in range [0..4]");
            }
            catch (System.Exception)
            {
                Console.WriteLine("Enter NUMBER [0..4]");
            }

        }
        return output;

    }

    private static void GetAllSessions()
    {
        Console.Clear();
        Console.WriteLine("--- Sessions ---");
        var list = _db.GetAllSessions();
        list.ForEach(ele => Console.WriteLine($"ID: {ele.Id}\nStart time: {ele.StartTime}\nEnd time: {ele.EndTime}\nHours: {ele.Duration}(hours)\n---"));
    }
}
