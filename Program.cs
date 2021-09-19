using System;
using static System.Console;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Globalization;
using static System.ConsoleColor;

namespace DateAndTimeExcercise
{
    class Program
    {


        static void Main(string[] args)
        {
            bool on = true; 

            var path = "/Users/alessandro/Workspace/ECUtbildning/C#/SSN Forms.txt";

            using (var file = new StreamReader(path))
            {
                string text = file.ReadToEnd();
                string[] lines = text.Split(Environment.NewLine);

                List<string> lineList = new List<string>();

                foreach (var item in lines)
                {
                    lineList.Add(item);
                }

                while (on)
                {
                    WriteLine("\t---- Welcome to Date Generator ----\n");
                    WriteLine("\tMake your choice by picking a number below!\n");
                    WriteLine("\t\t1. Print list.");
                    WriteLine("\t\t2. Search person in list.");
                    WriteLine("\t\t3. Try your own date.");

                    ForegroundColor = Red;
                    WriteLine("\t\t4. Exit :(");

                    var menuInput = ReadKey(true);
                    ForegroundColor = Black;

                    Clear();

                    switch (menuInput.Key)
                    {
                        case ConsoleKey.D1:
                        case ConsoleKey.NumPad1:


                            for (int i = 0; i < lineList.Count; i++)
                            {
                                try
                                {
                                    WriteLine($"\t\t{lines[i]}\n");
                                    DateStatistics(lineList[i]);
                                    WriteLine("\t\t==============================\n");

                                }
                                catch
                                {
                                    ForegroundColor = DarkRed;
                                    WriteLine("\t\tInvalid social security number!\n");
                                    ForegroundColor = Black;
                                    WriteLine("\t\t==============================\n");

                                }
                            }
                            break;

                        case ConsoleKey.D2:
                        case ConsoleKey.NumPad2:

                            Write("\tSearch by name: ");
                            var search = ReadLine();

                            Clear();

                            for (int i = 0; i < lineList.Count; i++)
                            {

                                if (lines[i].ToLower().Contains(search.ToLower()))
                                {
                                    WriteLine($"\t\t{lineList[i]}\n");
                                    DateStatistics(lineList[i]);
                                    ReadLine();

                                }
                                else if (!lines[i].ToLower().Contains(search.ToLower()))
                                {
                                    ForegroundColor = DarkRed;
                                    WriteLine("\tSorry. No person found!\n");
                                    ForegroundColor = Black;

                                    break;
                                }
                            }
                            break;

                        case ConsoleKey.D3:
                        case ConsoleKey.NumPad3:
                            Write("\tEnter your first name: ");
                            var firstName = ReadLine();

                            Write("\tEnter your last name: ");
                            var lastName = ReadLine();

                            Write("\tEnter your social security number(yyyyMMdd-xxxx): ");
                            var userSSN = ReadLine();

                            Clear();

                            try
                            {
                                WriteLine($"\t{firstName} {lastName}, {userSSN}\n");
                                DateStatistics(userSSN);
                                
                            }
                            catch
                            {
                                Clear();
                                ForegroundColor = DarkRed;
                                WriteLine("\tInvalid social security number!\n");
                                ForegroundColor = Black;

                                WriteLine("\tWould you like to try again? y/n");
                                var tryAgain = ReadKey(true);

                                if (tryAgain.Key == ConsoleKey.Y)
                                {
                                    Clear();
                                    goto case ConsoleKey.D3;
                                }
                                else
                                    break;
                            }

                            break;

                        case ConsoleKey.D4:
                        case ConsoleKey.NumPad4:

                            ForegroundColor = DarkGreen;
                            WriteLine("\tThanks for playing! :)");
                            Thread.Sleep(2000);
                            on = false;

                            return;

                        default:
                            ForegroundColor = ConsoleColor.DarkRed;
                            WriteLine("\t\t\nInvalid choice. Please try again!\n");
                            
                            
                            ForegroundColor = ConsoleColor.Black;
                            break;
                    }
                    ForegroundColor = DarkYellow;
                    WriteLine("\tPress any key to continue...");
                    ReadKey(true);
                    Clear();
                    ForegroundColor = Black;
                }
            }
        }

        static void DateStatistics(string userDate)
        {
            

            var userDateInt = userDate.Substring(userDate.Length-13, 8);
            //var str = int.Parse(userDate.Substring(11,23));
            var date = DateTime.ParseExact(userDateInt, "yyyyMMdd", CultureInfo.InvariantCulture);


            // Was born on a[day]. (t.ex. "Saturday")
            
            if (date < DateTime.Now)
            {
                WriteLine($"\t\tWas born on a {date.DayOfWeek}.");
                
            }
            else
                WriteLine($"\t\tWill be born on a {date.DayOfWeek}");;

                // Was born during a[isLeapYear] leap year. (eller "regular year")
                var isLeapYear = DateTime.IsLeapYear(date.Year);

            if (isLeapYear && date < DateTime.Now)
                WriteLine("\t\tWas born on a leap year.");
            else if (!isLeapYear && date < DateTime.Now)
                WriteLine("\t\tWas not born on a leap year.");
            else if (isLeapYear & date > DateTime.Now)
                WriteLine("\t\tWill be born on a leap year.");
            else if (!isLeapYear & date > DateTime.Now)
                WriteLine("\t\tWill not be born on a leap year.");


            // Has lived for [days] days.
            var daysLived = DateTime.Now - date;

            if(date < DateTime.Now)
            WriteLine($"\t\tHas lived {daysLived.Days} days.");
            else
                WriteLine("\t\tHas lived 0 days");

            // Is a[starsign]. (t.ex. "Capricorn")
            switch (date.Month)
            {
                case 1:

                    if (date.Day <= 19)
                        WriteLine("\t\tIs a Capricorn"); 
                    else
                        WriteLine("\t\tIs a Aquarius");
                    break;

                case 2:

                    if (date.Day <= 18)
                        WriteLine("\t\tIs a Aquarius");
                    else
                        WriteLine("\t\tIs a Pisces");
                    break;

                case 3:

                    if (date.Day <= 20)
                        WriteLine("\t\tIs a Pisces");
                    else
                        WriteLine("\t\tIs a Aries");
                    break;

                case 4:

                    if (date.Day <= 19)
                        WriteLine("\t\tIs a Aries");
                    else
                        WriteLine("\t\tIs a Taurus");
                    break;

                case 5:

                    if (date.Day <= 20)
                        WriteLine("\t\tIs a Taurus");
                    else
                        WriteLine("\t\tIs a Gemini");
                    break;

                case 6:
                    if (date.Day <= 20)
                        WriteLine("\t\tIs a Gemini");
                    else
                        WriteLine("\t\tIs a Cancer");
                    break;

                case 7:
                    if (date.Day <= 22)
                        WriteLine("\t\tIs a Cancer");
                    else
                        WriteLine("\t\tIs a Leo");
                    break;

                case 8:
                    if (date.Day <= 22)
                        WriteLine("\t\tIs a Leo");
                    else
                        WriteLine("\t\tIs a Virgo");
                    break;

                case 9:
                    if (date.Day <= 22)
                        WriteLine("\t\tIs a Virgo");
                    else
                        WriteLine("\t\tIs a Libra");
                    break;

                case 10:
                    if (date.Day <= 22)
                        WriteLine("\t\tIs a Libra");
                    else
                        WriteLine("\t\tIs a Scorpio");
                    break;

                case 11:
                    if (date.Day <= 21)
                        WriteLine("\t\tIs a Scorpio");
                    else
                        WriteLine("\t\tIs a Sagittarius");
                    break;

                case 12:
                    if (date.Day <= 21)
                        WriteLine("\t\tIs a Sagittarius");
                    else
                        WriteLine("\t\tIs a Capricorn");
                    break;
            }

            // Being a[gender], has[years] years left to live. (gender: woman / man)
            var userGenderDate = int.Parse(userDate.Substring(userDate.Length - 2, 1));

            var gender = userGenderDate % 2 == 0 ? "female" : "male";

            if (gender == "female" && date < DateTime.Now)
            {
                var yearsToLiveFemale = date.Year + 81;
                var yearsLeftFemale = yearsToLiveFemale - DateTime.Now.Year;

                WriteLine($"\t\tBeing a {gender}, she has {yearsLeftFemale} years to live.\n");
            }
            else if (gender == "male" && date < DateTime.Now)
            {

                var yearsToLiveMale = date.Year + 76;
                var yearsLeftMale = yearsToLiveMale - DateTime.Now.Year;

                WriteLine($"\t\tBeing a {gender}, he has {yearsLeftMale} years to live.\n");
            }
            else if (gender == "female" && date > DateTime.Now)
            {
                WriteLine($"\t\tBeing a {gender}, her life expectency is 81 years.\n");

            }
            else if (gender == "male" && date > DateTime.Now)
            {
                WriteLine($"\t\tBeing a {gender}, his life expectency is 76 years.\n");

            }





        }
    }
}
