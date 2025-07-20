using System;
using System.Reflection.Metadata.Ecma335;
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
                Options(options);
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
        public static void Options(string[] options)
        {
            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine("{0}. {1}", i + 1, options[i]);
            }
            Console.WriteLine("\n0. Go Back | I: Inventory | End: Exit \n");
        }
        public static void Invalid(ConsoleKeyInfo option)
        {
            Console.Clear();
            Console.WriteLine($"{option.Key} is not a valid key, try again. \n");
        }
    }
    public class Battle
    {
        public static void Fight(int Id, int Level, int[] Equip)
        {
            Enemies.TryGetValue(Id, out var enemyOut);
            var enemy = EnemyInit(enemyOut, Level, Equip);
            Console.Clear();
            Console.WriteLine("The battle begins! Your enemy is a Lvl {0} {1}.\n", Level, enemy.Name);
            while (true)
            {
                Console.WriteLine("1. Attack \n2. Use Item \n3. Check Enemy \n4. Flee \n");
                var choice = Console.ReadKey(true);
                var option = Data.MenuCheck(choice.Key);
                if (enemy.Stats[0] > 0)
                {
                    switch (option)
                    {
                        case 1:
                            Fight(); break;
                        case 2:
                            Item(); break;
                        case 3:
                            Console.Clear();
                            Check(enemy); continue;
                        case 4:
                            Console.Clear();
                            Flee(enemy.Type, Level); continue;
                        default:
                            Menu.Invalid(choice); continue;
                    }
                }
                else
                {
                    Console.Clear();
                    Win(enemy, Level);
                }
            }
        }
        public static EnemyData EnemyInit(EnemyData enemyData, int Level, int[] Equip)
        {
            var Equipped = new ItemData[Equip.Length];
            var Stat = enemyData.Stats;
            for (int i = 0; i < Equip.Length; i++)
            {
                Items.TryGetValue(Equip[i], out var item);
                Equipped[i] = item;
            }
            var AdjStat = EnemyInit1(Stat, Equipped);
            var HP = AdjStat[0];
            var Str = AdjStat[1];
            var Dam = AdjStat[2];
            var PDef = AdjStat[3];
            var MDef = AdjStat[4];
            var TDef = AdjStat[5];
            var Mana = AdjStat[6];
            var CC = AdjStat[7];
            var CD = AdjStat[8];
            var Regen = AdjStat[9];
            var MRegen = AdjStat[10];
            EnemyData enemyOut = new EnemyData(enemyData.Id, enemyData.Name, Level, enemyData.Type, [HP, Str, Dam, PDef, MDef, TDef, Mana, CC, CD, Regen, MRegen]);
            if (enemyOut != null)
            {
                return enemyOut;
            }

            return null;
        }
        public static int[] EnemyInit1(int[] Stat, ItemData[] Equip)
        {
            if (Equip.Length != 0)
            {
                for (int i = 0; i < Equip.Length; i++)
                {
                    for (int j = 0; j < Equip[i].MultPercent.Length; j++)
                    {
                        switch (Equip[i].MultPercent[j])
                        {
                            case 0:
                                Stat[j] += Equip[i].Stats[j];
                                break;
                            case 1:
                                Stat[j] *= Equip[i].Stats[j];
                                break;
                            case 2:
                                Stat[j] += (Stat[j] * Equip[i].Stats[j]) / 100;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            if (Stat != null)
            {
                return Stat;
            }
            return null;
        }
        public static void Fight()
        {
            //Yeah right, I have no clue.
        }
        public static void Item()
        {
            //See comment on line 195
        }
        public static void Check(EnemyData enemy)
        {
            Console.WriteLine("Enemy Name: {0}, Level: {1}, HP: {2}, Strength: {3}, Defense: {4}", enemy.Name, enemy.Level, enemy.Stats[0], enemy.Stats[1], enemy.Stats[3]);
        }
        public static void Flee(int EType, int ELevel)
        {
            int PLevel = Data.saveData.Levels[0];
            int LevelDelta = (PLevel * 6) - ELevel;
            int Min = 0 + (LevelDelta / 10);
            int Max = 25 + LevelDelta - (EType * 4);
            int Escape = 0;
            switch (EType)
            {
                case 0:
                    Escape = 0; break;
                case 1:
                    Escape = 20; break;
                case 2:
                    Escape = 40; break;
                case 3:
                    Escape = 60; break;
                case 4:
                    Escape = 80; break;
                case 5:
                    Escape = 9000; break;
            }
            if (Min < 0)
            {
                Min = 0;
            }
            if (Min > 100 && EType != 5)
            {
                Console.WriteLine("You successfully managed to escape.");
                Data.Back();
            }
            if (Min > 9000 && EType == 5)
            {
                Console.WriteLine("Despite your immense strength, their godly powers keep you stuck here.");
            }
            if (Max > 100)
            {
                Max = 100;
            }
            int roll = rnd.Next(Min, Max);
            if (roll > Escape)
            {
                Console.WriteLine("You successfully managed to escape.");
                Data.Back();
            }
            else
            {
                Console.WriteLine("You tried your best, but failed to escape.");
            }
        }
        public static void Win(EnemyData enemy, int Level)
        {
            Console.WriteLine("The {0} has been defeated!", enemy.Name);
            Data.BattleReward(enemy.Id, Level);
            Data.Back();
        }
    }
    public class Inv
    {
        public static void Inventory()
        {
            while (true)
            {
                options =
                    [
                    "Select Item",
                    "Show Equipped Items"
                    ];
                Console.WriteLine("Inventory \n");
                Menu.Options(options);
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
                        Console.Clear();
                        EqippedItems(); break;
                    case 99:
                        Exit(); break;
                    default:
                        Menu.Invalid(key); continue;
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
                    Console.WriteLine("{0} is not a valid option, try again. \n", select); continue;
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
        public static bool IsEquipped(ItemData Item)
        {
            for (int i = 0; i < Data.saveData.Items.Length; i++)
            {
                if (Data.saveData.Items[i] == Item.Id)
                {
                    return true;
                }
            }    
            return false;
        }
        public static void Selected(ItemData item)
        {
            Console.Clear();
            if (item.ArmourType > 0)
            {
                while (true)
                {
                    Console.WriteLine("What would you like to do with {0}? \n", item.Name);
                    Console.WriteLine("1. Inspect \n2. Delete \n3. Equip/Unequip \n \n0. Cancel \n");
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
                            Console.Clear();
                            if (!IsEquipped(item))
                            {
                                Data.Equip(item);
                            }
                            else
                            {
                                Data.Unequip(item);
                            }
                            break;
                        default:
                            Menu.Invalid(action); continue;
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
                            Menu.Invalid(action); continue;
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
            Console.WriteLine("Description: {0}\n", item.Description);
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
            while (true)
            {
                Console.WriteLine("1. Select Slot \n \n0. Go Back \n");
                Console.WriteLine("Equipped Items: \n");
                Data.EquipList();
                var select = Console.ReadKey(true);
                var option = Data.MenuCheck(select.Key);
                switch (option)
                {
                    case 0:
                        Console.Clear();
                        Inventory(); break;
                    case 1:
                        Console.Clear();
                        SelectSlot(); break;
                    default:
                        Menu.Invalid(select); continue;
                }
            }
        }
        public static void SelectSlot()
        {
            while(true)
            {
                Console.WriteLine("Select which slot?: \n0. Cancel");
                Data.EquipList();
                var select = Console.ReadKey(true);
                var option = Data.MenuCheck(select.Key);
                switch (option)
                {
                    case 0:
                        Console.Clear();
                        Inventory(); break;
                    case 1:
                        Console.Clear();
                        IsFilled(option); break;
                    default:
                        Menu.Invalid(select); continue;
                }
            }
        }
        public static bool IsFilled(int slot)
        {
            if (Data.saveData.Items[slot] > -1)
            {
                return true;
            }
            return false;
        }
        public static void SelectedItem(int slot)
        {
            bool SlotNotEmpty = IsFilled(slot);
            if (SlotNotEmpty)
            {
                 
            }
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
                Menu.Options(options);
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
                Menu.Options(options);
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