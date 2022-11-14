internal class Program
{
    private static void Main(string[] args)
    {
        Console.Clear();
        List<string> TodoList = new List<string>();
        List<string> CompletedList = new List<string>();
        List<string> StartupSayingsList = new List<string>();
        List<string> StatsList = new List<string>();
        DateTime now;
        Random startupSelector = new Random();
        int startTD = 0;
        int startCOMP = 0;
        //Read the Data Stored in the flies
        string[] TodoListArray = File.ReadAllLines("To-do");
        string[] CompletedListArray = File.ReadAllLines("Complete");
        string[] StartupSayingsArray = File.ReadAllLines("StartupSayings");
        string[] StatsArray = File.ReadAllLines("Stats.csv");
        //convert the file to-do to a list in the program
        foreach(string item in TodoListArray){
            TodoList.Add(item);
            startTD++;
        }
        //convert the file complete to a list in the program
        foreach(string item in CompletedListArray){
            CompletedList.Add(item);
            startCOMP++;
        }
        foreach(string item in StartupSayingsArray){
            StartupSayingsList.Add(item);
        }
        
        foreach(string item in StatsArray){
            StatsList.Add(item);
        }
        
        
        while (true){
            int StartupText = startupSelector.Next(10);
            if (StartupText != 4 && StartupText != 5){
                Console.WriteLine(StartupSayingsList.ElementAt(StartupText));
            }else if (StartupText == 4){
                Console.WriteLine($"You still have {startTD} things left to do.");
            }else if (StartupText == 5){
                Console.WriteLine($"Wow you have {startCOMP} things completed.");
            }else{
                Console.WriteLine("Opps a correct phrase didn't load");
            }
            
            
            Console.Write("What would you like to do? \n 1:See To-do List \n 2:See Completed list \n 3:Stats \n 0:Exit \n");
            ConsoleKey keypress = Console.ReadKey(true).Key;
            if(keypress == ConsoleKey.D1){
                Console.ForegroundColor = ConsoleColor.White;
                while (true){
                    Console.ForegroundColor = ConsoleColor.Blue;
                        int count = 1;
                        foreach(string thing in TodoList){
                            Console.WriteLine($"{count}: {thing}");
                            count++;
                        }
                        Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Would you like to?\n 1:Check Something Off \n 2:Add An Item \n 3:Go Back");
                    keypress = Console.ReadKey(true).Key;
                    if (keypress == ConsoleKey.D3){
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    }else if(keypress == ConsoleKey.D2){
                        
                        //The user wants to add something new to the program
                        Console.WriteLine("What do you want to add to the list? (type it in and press enter to confirm)");
                        string entry = Console.ReadLine();
                        TodoList.Add($"{entry}, Started at; {Convert.ToString(now = DateTime.Now)}");
                        File.WriteAllLines("To-Do", TodoList);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"{entry}, was added to the todo list at {Convert.ToString(now = DateTime.Now)}");
                        Console.ForegroundColor = ConsoleColor.White;
                    } else if(keypress == ConsoleKey.D1){
                        Console.WriteLine("Which Item by ID do you want to Check off? (press ENTER to confirm)");
                        try{
                            //Check Something off and remove it from the list
                            int edit = Convert.ToInt32(Console.ReadLine());
                            edit--;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"The Entry \"{TodoList.ElementAt(edit)}\" has been checked off");
                            string checkedOff = TodoList.ElementAt(edit);
                            TodoList.RemoveAt(edit);
                            File.WriteAllLines("To-Do", TodoList);
                            CompletedList.Add($"{checkedOff}; Finished at; {Convert.ToString(now = DateTime.Now)}");
                            File.WriteAllLines("Complete", CompletedList);
                            
                        }catch (FormatException){
                            //User enters in a value that is invalid
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("You where supposed to enter a number, and now you have caused a problem");
                            Console.ForegroundColor = ConsoleColor.White;
                        }catch (ArgumentOutOfRangeException){
                            //Uset enters a value that is too big
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Im Sorry that number is not on the list, an caused an error");
                        }
                    }
                }


            }else if (keypress == ConsoleKey.D2){
                //Read the Data Stored in the Completed List
                while (true){
                    Console.ForegroundColor=ConsoleColor.White;
                    Console.WriteLine("Would you like to?\n 1:Remove Something\n 2:Go Back");
                        keypress = Console.ReadKey(true).Key;
                    if (keypress == ConsoleKey.D2){
                        break;
                    }else if(keypress == ConsoleKey.D1){
                        Console.WriteLine("Which this would you like to remove? (by Numeric ID)");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        int count = 1;
                        foreach(string thing in CompletedList){
                            Console.WriteLine($"{count}: {thing}");
                            count++;
                        }
                        Console.ForegroundColor = ConsoleColor.White;
                        int edit = Convert.ToInt32(Console.ReadLine());
                        edit--;
                        CompletedList.RemoveAt(edit);
                        File.WriteAllLines("Complete", CompletedList);

                    }
                }

            }else if (keypress == ConsoleKey.D0){
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Good Bye!");
                Console.ForegroundColor = ConsoleColor.White;
                break;
            }else if(keypress == ConsoleKey.D3){
                int statsTotal = 0;
                int statsTD = 0;
                int statsCOMP = 0;
                StatsList.Clear();
                //determins the total number of items as well at done and completed things
                foreach (var bruh in TodoList)
                {
                    statsTotal++;
                    statsTD++;
                }
                foreach (var bruh in CompletedList){
                    statsTotal++;
                    startCOMP++;
                }
                //this is all the calulations for well stuff
                {
                List<DateOnly> todoStartDate = new List<DateOnly>();
                foreach (string bruh in TodoList)
                {
                    string[] sigh = bruh.Split(";");
                    string[] leSigh = sigh[1].Split(" ");
                    todoStartDate.Add(DateOnly.FromDateTime(Convert.ToDateTime(sigh[1])));
                }

                foreach(DateOnly bruh in todoStartDate){
                    Console.WriteLine(bruh);
                    
                }
                /*
                foreach (DateOnly bruh in todoStartDate){
                    int lebruh = 0;
                    DateOnly.Compare(todoStartDate.ElementAt(lebruh), todoStartDate.ElementAt(lebruh-1));
                }
                */
                
                }





                
                StatsList.Add(Convert.ToString($"Total number of items: {statsTotal}"));
                StatsList.Add(Convert.ToString($"Number of things left To-Do: {statsTD}"));
                StatsList.Add(Convert.ToString($"Number of completed objects: {statsCOMP}"));
                foreach(string entries in StatsList){
                    Console.WriteLine(entries);
                }
                File.WriteAllLines("Stats.csv", StatsList);

                break;
            }else{
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("This is not the right key");
                Console.ForegroundColor = ConsoleColor.White;
            }



            
        }
    }



}
