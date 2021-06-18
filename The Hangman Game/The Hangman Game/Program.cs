using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace The_Hangman_Game
{
   
    class Program
    {




        public static void New_Game()
        {
            while (true)
            {
                string[] draw =
                {
                    "  _________",
                    "  |   |   |",
                    "  |   O   |",
                    "  |  /|\\  |",
                    "  |   |   |",
                    "  |  / \\  |",
                    " /|\\     /|\\",
                    "/ | \\   / | \\"
                };
                string path = @"countries_and_capitals.txt";
                StreamReader sr = File.OpenText(path);
                string Cap = "";
                List<string> country = new List<string>();
                List<string> capitals = new List<string>();
                while ((Cap = sr.ReadLine()) != null)
                {
                    country.Add(Cap.Substring(0, (Cap.IndexOf("|"))).Trim());
                    capitals.Add(Cap.Substring((Cap.LastIndexOf("|")) + 2, Cap.Length - (Cap.IndexOf("|")) - 2).Trim());
                }
                Random random = new Random();
                string randomNumber = capitals[random.Next(capitals.Count - 1)];
                string randomCountry = country[capitals.IndexOf(randomNumber)];

                int wordLen = randomNumber.Length; 
                char[] userWord = new char[wordLen]; 
                for (int i = 0; i < wordLen; i++) 
                {
                    userWord[i] = '-';
                }
                int errCount = draw.Length; 
                int err = 0;
                
                char[] errChars = new char[errCount]; 
                bool game = true; 
                for (int i = 0; i < errCount; i++) 
                {
                    Console.WriteLine(draw[i]);
                }
                
                while (game) 
                {
                    for (int i = 0; i < err; i++) 
                    {
                        Console.WriteLine(draw[i]);
                    }
                    Console.WriteLine();
                    Console.WriteLine(userWord); 
                    Console.WriteLine();
                    Console.Write("Wrong letters: ");
                    Console.Write(errChars); 
                    Console.WriteLine("Trials remain: {0}", errCount - err); 
                    Console.WriteLine("\nEnter a letter: ");
                    string s = Console.ReadLine(); 
                    char c;
                    if (s.Length > 0) 
                        c = s.ElementAt(0); 
                    else 
                        continue;
                    bool charFound = false; 
                    bool win = false; 
                    for (int i = 0; i < wordLen; i++) 
                    {
                        if (c == randomNumber.ElementAt(i)) 
                        {
                            userWord[i] = c; 
                            charFound = true; 
                           
                        }
                    }
                    bool errCharFound = false; 
                    if (!charFound) 
                    {
                        err += 1; 
                        for (int i = 0; i < errChars.Length; i++) 
                        {
                            if (errChars[i] == c)
                            {
                                errCharFound = true; 
                                break;
                            }
                        }
                        if (!errCharFound) 
                        {
                            for (int i = 0; i < errChars.Length; i++)
                            {
                                if (errChars[i] == 0) 
                                {
                                    errChars[i] = c; 
                                    break;
                                }

                            }
                        }
                    }
                    for (int i = 0; i < wordLen; i++)
                    {
                        if (userWord[i] == '-')
                        {
                            win = false;
                            break;
                        }
                        win = true;
                    }
                    if (err == errCount-1)
                    {
                        Console.WriteLine("The capital of : {0}\n", randomCountry);
                    }
                    if (err == errCount) 
                    {
                        Console.WriteLine("You died. The ward was: {0}!\n", randomNumber);
                        Console.ReadLine();
                        game = false;
                        



                        Console.WriteLine("If you want to restart type 1, If not type  2");
                        var choice = int.Parse(Console.ReadLine());
                        if(choice==1)
                        {
                            New_Game();
                        }
                        else
                        {
                            Menu();
                        }

                    }
                    if (win) 
                    {
                        Console.WriteLine("\nYou won. The ward was: {0}!", randomNumber);
                        Console.ReadLine();
                        game = false;
                        DateTime thisDay = DateTime.Now;
                        Console.WriteLine(thisDay.ToString());
                        Console.WriteLine();

                        string path1 = @"leaderboard.txt";
                        StreamWriter sw;

                        if (!File.Exists(path1))
                        {
                            sw = File.CreateText(path1);


                        }
                        else
                        {
                            sw = new StreamWriter(path1, true);

                        }
                        Console.WriteLine(" Type your name :");
                        string tekst = Console.ReadLine();
                        sw.WriteLine($"Player: {tekst}  Mistakes made: {err} Date : {thisDay.ToString("g")}");
                       
                        sw.Close();

                        Console.WriteLine("If you want to restart type 1, If not type  2");
                        var choice = int.Parse(Console.ReadLine());
                        if (choice == 1)
                        {
                            New_Game();
                        }
                        else
                        {
                            Menu();
                        }
                    }

                }

            }


        }       

            public static void Leaderboard()
            {
            string path1 = @"leaderboard.txt";
            StreamReader sr = File.OpenText(path1);
            string s = "";
            int i = 1;
            Console.WriteLine(" ");
            while ((s = sr.ReadLine()) != null)
            {
                Console.WriteLine(i++ + ". " + s);
            }
            sr.Close();
            Console.WriteLine();
            Menu();
        }
            public static void Exit()
            {
            Environment.Exit(1);
            }

            public static void Menu()
            {

                Console.WriteLine(" * ----------- * --- *  ");
                Console.WriteLine(" |   New Game  |  1  |  ");
                Console.WriteLine(" | Leaderboard |  2  |  ");
                Console.WriteLine(" |     Exit    |  3  |  ");
                Console.WriteLine(" * ----------- * --- *  ");
                Console.WriteLine(" ");
                Console.WriteLine("  Select 1,2,3 : ");
                

                while (true)
                {
                    int switchCase = int.Parse(Console.ReadLine());
                    switch (switchCase)
                    {
                        case 1:
                            New_Game();
                            break;
                        case 2:
                            Leaderboard();
                            break;
                        case 3:
                            Exit();
                            break;
                        default:
                            Console.WriteLine("Select correct number");
                            break;
                    }
                }
            }
            public static void Main()
            {

                Menu();
            }
        }
    }

