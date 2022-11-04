internal class Program
{
    private static void Main(string[] args)
    {
        List<string> TodoList = new List<string>();
        List<string> CompletedList = new List<string>();
        DateTime now;
        while (true){
            Console.WriteLine($"Welcome to your to-do list app.");
            Console.Write("What would you like to do? \n 1:See To-do List \n 2:See Completed list \n 0:Exit \n");
            ConsoleKey keypress = Console.ReadKey(true).Key;



            //Read the Data Stored in the flies
                string[] TodoListArray = File.ReadAllLines("To-do");
                string[] CompletedListArray = File.ReadAllLines("Complete");
                Console.ForegroundColor = ConsoleColor.Blue;
                //convert the file to-do to a list in the program
                foreach(string item in TodoListArray){
                    TodoList.Add(item);
                }
                //convert the file complete to a list in the program
                foreach(string item in CompletedListArray){
                    CompletedList.Add(item);
                }


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
                        TodoList.Add($"{entry}, Started at {Convert.ToString(now = DateTime.Now)}");
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
                            CompletedList.Add($"{checkedOff}, Finished at {Convert.ToString(now = DateTime.Now)}");
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
                //This is just to allow the program to loop without showing anything
            }else{
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("This is not the right key");
                Console.ForegroundColor = ConsoleColor.White;
            }



            
        }
    }



}
