using static Game;

namespace CMDRPG
{
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
}