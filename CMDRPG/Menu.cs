using static Game;

namespace CMDRPG
{
    internal class Menu
    {
        public static void MainM()
        {
            MenuID = 0;
            Console.Clear();
            while (true)
            {
                Console.WriteLine("Main Menu: \n");
                Console.WriteLine("P: Places \nI: Inventory \nQ: Quests \nX: Exit \n");
                var next = Console.ReadKey(true);
                switch (next.Key)
                {
                    case ConsoleKey.P:
                        Places(); break;
                    case ConsoleKey.I:
                        Inventory(); break;
                    case ConsoleKey.Q:
                        Quests(); break;
                    case ConsoleKey.X:
                        Exit(); break;
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
            Console.Clear();
            while (true)
            {
                Console.WriteLine("Where would you like to go? \n \nPlaces: \n");
                Console.WriteLine("1. Village (Lvl 0-5) \n2. Caves (Lvl 5-15) \n3. Mines (Lvl 15-25) \n4. Mountains (Lvl 25-40) \n \n0. Go back | I: Inventory \n");
                var place = Console.ReadKey(true);
                if (!menuOption.TryGetValue(place.Key, out var option))
                {
                    Console.Clear();
                    Console.WriteLine($"{place.Key} is not a valid key, try again.");
                    continue;
                }
                switch (option)
                {
                    case 0:
                        MainM(); break;
                    case 1:
                        Village.Square(); break;
                    case 2:
                        Caves.Commune(); break;
                    case 3:
                        Mines.Town(); break;
                    case 4:
                        Mountains.Base(); break;
                    case 11:
                        Inventory(); break;
                }
                break;
            }
        }
        public static void Inventory()
        {
            Console.Clear();
            Console.WriteLine("Inventory \n");
            while (true)
            {
                Console.WriteLine("1. Stuff | 0. Go Back | X: Exit \n");
                for (int i = 0; i < Data.saveData.Inventory.Length; i++)
                {
                    if (Data.saveData.Inventory[i] > 0)
                    {
                        if (Items.TryGetValue(i, out var item))
                        {
                            Console.WriteLine("{0}, {1}, {2}, {3}, {4}: {5}", item.Id, item.Name, item.Description, item.Level, item.Equipable, Data.saveData.Inventory[i]);
                        }
                    }
                }
                var key = Console.ReadKey(true);
                if (!menuOption.TryGetValue(key.Key, out var option))
                {
                    Console.Clear();
                    Console.WriteLine($"{key.Key} is not a valid key, try again. \n");
                    continue;
                }
                switch (option)
                {
                    case 0:
                        Data.Back(); break;
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Stuff \n"); continue;
                    case 99:
                        Exit(); break;
                    default:
                        Console.Clear();
                        Console.WriteLine($"{key.Key} is not a valid key, try again. \n"); continue;
                }
                break;
            }
        }
        public static void Quests()
        {
            Console.Clear();
            Console.WriteLine("This doesn't exist, probably never will. \n");
            Console.ReadKey(true);
            MainM();
        }
    }
    public class Village
    {
        public static void Square()
        {
            MenuID = 2;
            Console.Clear();
            while (true)
            {
                Console.WriteLine("You arrive in the town square.");
                Console.WriteLine("Where would you like to go? \n \nPlaces: \n");
                Console.WriteLine("1. Forge \n2. Workbench \n3. Armourer \n4. Weaponsmith \n5. Tavern \n6. Woods \n \n0. Go back | I: Inventory \n");
                var place = Console.ReadKey();
                if (!menuOption.TryGetValue(place.Key, out var option))
                {
                    Console.Clear();
                    Console.WriteLine($"{place.Key} is not a valid key, try again.");
                    continue;
                }
                switch (option)
                {
                    case 0:
                        Menu.Places(); break;
                    case 1:
                        Forge(); break;
                    case 2:
                        Workbench(); break;
                    case 3:
                        Armour(); break;
                    case 4:
                        Weapons(); break;
                    case 5:
                        Tavern(); break;
                    case 6:
                        Woods(); break;
                    case 11:
                        Menu.Inventory(); break;
                }
                break;
            }
        }
        public static void Forge()
        {
            MenuID = 3;
            Console.Clear();
        }
        public static void Workbench()
        {
            MenuID = 4;
            Console.Clear();
        }
        public static void Armour()
        {
            MenuID = 5;
            Console.Clear();
        }
        public static void Weapons()
        {
            MenuID = 6;
            Console.Clear();
        }
        public static void Tavern()
        {
            MenuID = 7;
            Console.Clear();
        }
        public static void Woods()
        {
            MenuID = 8;
            Console.Clear();
            while (true)
            {
                Console.WriteLine("Wandering in the woods you think of what to do: \n");
                Console.WriteLine("1. Chop an Oak Tree \n2. Chop a Birch Tree \n3. Gather Sticks \n4. Pick up Stones \n5. Travel Deeper \n \n0. Go Back | I: Inventory \n");
                var act = Console.ReadKey(true);
                if (!menuOption.TryGetValue(act.Key, out var option))
                {
                    Console.Clear();
                    Console.WriteLine($"{act.Key} is not a valid key, try again.");
                    continue;
                }
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
                        break;
                    case 11:
                        Menu.Inventory(); break;
                    case 99:
                        Exit(); break;
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
