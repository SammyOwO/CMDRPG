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
    public class Village
    {
        public static void Square()
        {
            MenuID = 2;
            options =
                [
                "Forge",
                "Workbench",
                "Armour Merchant",
                "Weaponsmith",
                "Tavern",
                "Woods"
                ];
            while (true)
            {
                Console.WriteLine("You arrive in the town square.");
                Console.WriteLine("Where would you like to go? \n \nPlaces: \n");
                MenuList.Options(options);
                var place = Console.ReadKey(true);
                var option = Data.MenuCheck(place.Key);
                switch (option)
                {
                    case 0:
                        Console.Clear();
                        Menu.Places(); break;
                    case 1:
                        Console.Clear();
                        Forge(); break;
                    case 2:
                        Console.Clear();
                        Workbench(); break;
                    case 3:
                        Console.Clear();
                        Armour(); break;
                    case 4:
                        Console.Clear();
                        Weapons(); break;
                    case 5:
                        Console.Clear();
                        Tavern(); break;
                    case 6:
                        Console.Clear();
                        Woods(); break;
                    case 88:
                        Console.Clear();
                        Inv.Inventory(); break;
                    case 99:
                        Exit(); break;
                    default:
                        Menu.Invalid(place); continue;
                }
                break;
            }
        }
        public static void Forge()
        {
            MenuID = 3;
            options =
                [

                ];
        }
        public static void Workbench()
        {
            MenuID = 4;
            options =
                [

                ];
        }
        public static void Armour()
        {
            MenuID = 5;
            options =
                [

                ];
        }
        public static void Weapons()
        {
            MenuID = 6;
            options =
                [

                ];
        }
        public static void Tavern()
        {
            MenuID = 7;
            options =
                [

                ];
        }
        public static void Woods()
        {
            MenuID = 8;
            options =
                [
                "Chop an Oak Tree",
                "Chop a Birch Tree",
                "Gather Sticks",
                "Pick up Stones",
                "Travel Deeper"
                ];
            while (true)
            {
                Console.WriteLine("Wandering in the woods you think of what to do: \n");
                MenuList.Options(options);
                var action = Console.ReadKey(true);
                var option = Data.MenuCheck(action.Key);
                switch (option)
                {
                    case 0:
                        Square(); break;
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        Console.Clear();
                        Console.WriteLine("fuck You");
                        continue;
                    case 88:
                        Inv.Inventory(); break;
                    case 99:
                        Exit(); break;
                    default:
                        Menu.Invalid(action); continue;
                }
                break;
            }
        }
    }
    public class Caves
    {
        public static void Commune()
        {
            Console.Clear();
        }
    }
    public class Mines
    {
        public static void Town()
        {
            Console.Clear();
        }
    }
    public class Mountains
    {
        public static void Base()
        {
            Console.Clear();
        }
    }
}