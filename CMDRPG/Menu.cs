using static Game;

namespace CMDRPG
{
    internal class Menu
    {
        public static void MainM()
        {
            MenuID = 0;
            while (true)
            {
                Console.WriteLine("Main Menu: \n");
                Console.WriteLine("P: Places \nI: Inventory \nQ: Quests \nEnd: Exit");
                var next = Console.ReadKey(true);
                switch (next.Key)
                {
                    case ConsoleKey.P:
                        Console.Clear();
                        Places(); break;
                    case ConsoleKey.I:
                        Console.Clear();
                        Inv.Inventory(); break;
                    case ConsoleKey.Q:
                        Console.Clear();
                        Quests(); break;
                    case ConsoleKey.End:
                        Exit(); break;
                    case ConsoleKey.Delete:
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine($"{next.Key} is an invalid input, try again. \n"); continue;
                }
                break;
            }
        }
        public static void Places()
        {
            MenuID = 1;
            while (true)
            {
                options =
                    [
                    "Village (Lvl 0-5)",
                    "Caves (Lvl 5-15)",
                    "Mines (Lvl 15-25)",
                    "Mountains (Lvl 25-40)"
                    ];
                Console.WriteLine("Where would you like to go? \n \nPlaces: \n");
                MenuList.Options(options);
                var place = Console.ReadKey(true);
                var option = Data.MenuCheck(place.Key);
                switch (option)
                {
                    case 0:
                        Console.Clear();
                        MainM(); break;
                    case 1:
                        Console.Clear();
                        Village.Square(); break;
                    case 2:
                        Console.Clear();
                        Caves.Commune(); break;
                    case 3:
                        Console.Clear();
                        Mines.Town(); break;
                    case 4:
                        Console.Clear();
                        Mountains.Base(); break;
                    case 11:
                        Console.Clear();
                        Inv.Inventory(); break;
                }
                break;
            }
        }
        public static void Quests()
        {
            Console.WriteLine("This doesn't exist, probably never will. \n");
            Console.ReadKey(true);
            MainM();
        }
        public static void Invalid(ConsoleKeyInfo option)
        {
            Console.Clear();
            Console.WriteLine($"{option.Key} is not a valid key, try again. \n");
        }
        public static void Canceled()
        {
            Console.Clear(); 
            Console.WriteLine("Action Canceled. \nPress any key to continue...");
            Console.ReadKey(true);
        }
    }
}