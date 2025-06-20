﻿using static Game;

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
                        Inv.Inventory(); break;
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
                var option = Data.MenuCheck(place.Key);
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
                        Inv.Inventory(); break;
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
    public class Battle
    {
        public static void Fight(int Id, int Level, int[] Equip)
        {
            for (int i = 0; i < Equip.Length; i++)
            {
                Items.TryGetValue(i, out var item);
            }
            Enemies.TryGetValue(Id, out var enemy);
            // var HP = enemy.Stats[0]
            // var Str = enemy.Stats[1]
            // var Def = enemy.Stats[2]
            // var Mana = enemy.Stats[3]
            // var CC = enemy.Stats[4]
            // var CD = enemy.Stats[5]
            // var Regen = enemy.Stats[6]
        }
        public static void Check(string Name, int Level, int HP, int Str, int Def)
        {
            Console.WriteLine("Enemy Name: {0}, Level: {1}, HP: {2}, Strength: {3}, Defense: {4}", Name, Level, HP, Str, Def);
        }
    }
    public class Inv
    {
        public static void Inventory()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Inventory \n");
                Console.WriteLine("1. Select Item \n2. Show Equipped Items \n0. Go Back | X: Exit \n");
                Data.InvList();
                var key = Console.ReadKey(true);
                var option = Data.MenuCheck(key.Key);
                switch (option)
                {
                    case 0:
                        Data.Back(); break;
                    case 1:
                        Console.Clear();
                        SelectItems(); continue;
                    case 2:
                        EqippedItems(); break;
                    case 99:
                        Exit(); break;
                    default:
                        Console.Clear();
                        Console.WriteLine($"{key.Key} is not a valid key, try again. \n"); continue;
                }
                break;
            }
        }
        public static void SelectItems()
        {
            Console.WriteLine("Select which item?: \n");
            Data.InvList();
            while (true)
            {

                string select = Console.ReadLine();
                var item = InventorySearch(select);
                if (item != null)
                {
                    Selected(item); break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("{0} is not a valid option, try again. \n", select, item.Name); continue;
                }
            }
        }
        public static ItemData InventorySearch(string name)
        {
            int Id;
            if (!int.TryParse(name, out Id))
            {
                Id = -1;
            }
            for (int i = 0; i < Data.saveData.Inventory.Length; i++)
            {
                if (Data.saveData.Inventory[i] > 0)
                {
                    if (Items.TryGetValue(i, out var item))
                    {
                        if (name.ToLower() == item.Name.ToLower() || Id == item.Id)
                        {
                            return item;
                        }
                    }
                }
            }
            return null;
        }
        public static void Selected(ItemData item)
        {
            Console.Clear();
            if (item.ArmourType > 0)
            {
                while (true)
                {
                    Console.WriteLine("What would you like to do with {0}? \n", item.Name);
                    Console.WriteLine("1. Inspect \n2. Delete \n3. Equip \n \n0. Cancel \n");
                    var action = Console.ReadKey(true);
                    var option = Data.MenuCheck(action.Key);
                    switch (option)
                    {
                        case 0:
                            Console.Clear();
                            break;
                        case 1:
                            Inspect(item); continue;
                        case 2:
                            Delete(item); break;
                        case 3:
                            Data.Equip(item.Id, item.ArmourType); break;
                        default:
                            Console.Clear();
                            Console.WriteLine($"{action.Key} is not a valid key, try again. \n");
                            continue;
                    }
                    break;
                }
            }
            else
            {
                while (true)
                {
                    Console.WriteLine("What would you like to do with {0}? \n", item.Name);
                    Console.WriteLine("1. Inspect \n2. Delete \n \n0. Cancel \n");
                    var action = Console.ReadKey(true);
                    var option = Data.MenuCheck(action.Key);
                    switch (option)
                    {
                        case 0:
                            Console.Clear();
                            break;
                        case 1:
                            Inspect(item); continue;
                        case 2:
                            Delete(item); break;
                        default:
                            Console.Clear();
                            Console.WriteLine($"{action.Key} is not a valid key, try again. \n");
                            continue;
                    }
                    break;
                }
            }
        }
        public static void Inspect(ItemData item)
        {
            Console.Clear();
            Console.WriteLine("Level: {0}", item.Level);
            Console.WriteLine("Name: {0}", item.Name);
            Console.WriteLine("Description: {0}", item.Description);
            Console.WriteLine();
        }
        public static void Delete(ItemData item)
        {
            Console.Clear();
            Console.WriteLine("Delete how many?: \n");
            string delamounts = Console.ReadLine();
            int.TryParse(delamounts, out int delamount);
            if (Data.saveData.Inventory[item.Id] - delamount <= 0)
            {
                Data.saveData.Inventory[item.Id] = 0;
                Console.Clear();
                Console.WriteLine("You deleted all of your {0}! \n", item.Name);
            }
            else
            {
                Data.saveData.Inventory[item.Id] -= delamount;
                Console.Clear();
                Console.WriteLine("You deleted {0} of your {1}! \n", delamount, item.Name);
            }
        }
        public static void EqippedItems()
        {
            Console.Clear();
            Console.WriteLine("1. Select Slot \n \n0. Go Back \n");
            Console.WriteLine("Equipped Items: \n");
            for (int i = 0; i < Data.saveData.Items.Length; i++)
            {
                if (Data.saveData.Items[i] < 99999)
                {
                    if (Items.TryGetValue(Data.saveData.Items[i], out var item))
                    {
                        if (slotName.TryGetValue(i + 1, out var slot))
                        {
                            Console.WriteLine("{0}: {1}", slot, item.Name);
                        }
                    }
                }
                else
                {
                    if (slotName.TryGetValue(i + 1, out var slot))
                    {
                        Console.WriteLine("{0}: None", slot);
                    }
                }
            }
            var select = Console.ReadKey(true);
            Inventory();
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
                var place = Console.ReadKey(true);
                var option = Data.MenuCheck(place.Key);
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
                        Inv.Inventory(); break;
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
                    case 11:
                        Inv.Inventory(); break;
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
