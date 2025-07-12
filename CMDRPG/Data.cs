using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using static Game;

namespace CMDRPG
{
    internal class Data
    {
        public static SaveFile saveData = new();
        public static void save()
        {
            var json = JsonSerializer.Serialize(saveData);
            string fullPath = @".\Saves\" + saveData.Name + ".json";
            File.WriteAllText(fullPath, json);
        }
        public static void load()
        {
            string saveDir = @".\Saves\";
            var saveList = Directory.EnumerateFiles(saveDir);
            if (saveList.Count() == 0)
            {
                Console.Clear();
                Console.WriteLine("There are no saves found, try again. \n");
                Game.StartUp();
            }
            Console.WriteLine("Select a save to continue: \n");
            foreach (string saves in saveList)
            {
                Console.WriteLine(Path.GetFileNameWithoutExtension(saves));
            }
            Console.WriteLine();
            while (true)
            {
                var saveFile = Console.ReadLine();
                bool valid = File.Exists(saveDir + saveFile + ".json");
                if (valid == true)
                {
                    Console.Clear();
                    using (StreamReader r = new StreamReader(saveDir + saveFile + ".json"))
                    {
                        string loadFile = r.ReadToEnd();
                        saveData = JsonSerializer.Deserialize<SaveFile>(loadFile);
                    }
                    Welcome();
                }
                else
                {
                    Console.WriteLine("\n" + saveFile + ".json is not a valid file.");
                    Console.WriteLine("Please try again. \n");
                    continue;
                }
                break;
            }
        }
        public static async Task Skill(int Skill, int Duration, int ID, int Min, int Max, int EMin, int EMax)
        {
            Items.TryGetValue(ID, out var itemName);
            var Item = itemName.Name;
            var Time = Duration - ((Duration * saveData.Levels[Skill]) / 100) + (Duration / 100);
            switch (Skill)
            {
                case 1:
                    Console.WriteLine("Mining...");
                    break;
                case 2:
                    Console.WriteLine("Farming...");
                    break;
                case 3:
                    Console.WriteLine("Fishing...");
                    break;
                case 4:
                    Console.WriteLine("Foraging...");
                    break;
                case 5:
                    Console.WriteLine("Crafting...");
                    break;
                case 6:
                    Console.WriteLine("Enchanting...");
                    break;
                case 7:
                    Console.WriteLine("Brewing...");
                    break;
            }
            await Task.Delay(Time);
            if (Items.ContainsKey(ID))
            {
                 saveData.Inventory[ID] += rnd.Next(Min, Max + 1);
            }
            ExpAward(Skill, EMin, EMax);
            Console.Clear();
            Console.WriteLine("Done! You now have {0} {1}. \n", saveData.Inventory[ID], Item);
        }
        public static void ExpAward(int Skill, int Min, int Max)
        {
            saveData.Exp[Skill] += rnd.Next(Min, Max + 1);
        }
        public static void BattleReward(int Id, int Level)
        {
            Enemies.TryGetValue(Id, out var Enemy);
            //P for Passive E for Enemy. I'm not refactoring this.
            var Pmin = (Level * 1);
            var Pmax = (Level * 2);
            var Emin = (Level * 3) * Enemy.Type;
            var Emax = (Level * 8) * (Enemy.Type + 1);
            if (Enemy.Type != 0 || Enemy.Type != -1)
            {
                ExpAward(0, Emin, Emax);
            }
            ExpAward(0, Pmin, Pmax);
        }
        public static void InvList()
        {
            for (int i = 0; i < saveData.Inventory.Length; i++)
            {
                if (saveData.Inventory[i] <= 0) continue;
                if (Items.TryGetValue(i, out var item))
                {
                    Console.WriteLine("{0}: {1}", item.Name, saveData.Inventory[i]);
                }
            }
            Console.WriteLine();
        }
        public static void Back()
        {
            switch (MenuID)
            {
                case 0:
                    Menu.MainM(); break;
                case 1:
                    Menu.Places(); break;
                case 2:
                    Village.Square(); break;
                case 3:
                    Village.Forge(); break;
                case 4:
                    Village.Workbench(); break;
                case 5:
                    Village.Armour(); break;
                case 6:
                    Village.Weapons(); break;
                case 7:
                    Village.Tavern(); break;
                case 8:
                    Village.Woods0(); break;
            }
        }
        public static void Equip(int Id, int Type)
        {
            Items.TryGetValue(Id, out var itemCheck);
            var itemLevel = itemCheck.Level;
            var playerLevel = saveData.Levels[0];
            var Slot = Type - 1;
            switch (Type)
            {
                case < 11:
                    if (itemLevel > playerLevel)
                    {
                        Console.WriteLine("You are not a high enough level to use that, you must have combat level {0} to equip it.", itemLevel);
                    }
                    else
                    {
                        if (saveData.Items[Slot] < 99999)
                        {
                            while (true)
                            {
                                if (Items.TryGetValue(saveData.Items[Slot], out var Item))
                                {
                                    string Name = Item.Name;
                                    Console.WriteLine("You already have {0} equipped in that slot. Would you like to replace it? \n \n1. Yes \n2. No", Name);
                                }
                                var confirm = Console.ReadKey(true);
                                var option = MenuCheck(confirm.Key);
                                switch (option)
                                {
                                    case 1:
                                        saveData.Items[Slot] = Id;
                                        break;
                                    case 2:
                                        break;
                                    default:
                                        Console.Clear();
                                        Console.WriteLine($"{confirm.Key} is not a valid key, try again. \n");
                                        continue;
                                }
                                break;
                            }
                        }
                        else
                        {
                            saveData.Items[Slot] = Id;
                        }
                    }
                    break;
                case 11:
                    if (itemLevel > playerLevel)
                    {
                        Console.WriteLine("You are not a high enough level to use that, you must have combat level {0} to equip it.", itemLevel);
                    }
                    else
                    {
                        while (true)
                        {
                            Console.WriteLine("Equip in which slot, 1 or 2? \n");
                            var eslot = Console.ReadKey(true);
                            var option = MenuCheck(eslot.Key);
                            switch (option)
                            {
                                case 1:
                                    if (saveData.Items[Slot] < 99999)
                                    {
                                        while (true)
                                        {
                                            if (Items.TryGetValue(saveData.Items[Slot], out var Item))
                                            {
                                                string Name = Item.Name;
                                                Console.WriteLine("You already have {0} equipped in that slot. Would you like to replace it? \n \n1. Yes \n2. No", Name);
                                            }
                                            var confirm = Console.ReadKey(true);
                                            var bracelet = MenuCheck(confirm.Key);
                                            switch (bracelet)
                                            {
                                                case 1:
                                                    saveData.Items[Slot] = Id;
                                                    break;
                                                case 2:
                                                    break;
                                                default:
                                                    Console.Clear();
                                                    Console.WriteLine($"{confirm.Key} is not a valid key, try again. \n");
                                                    continue;
                                            }
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        saveData.Items[Slot] = Id;
                                    }
                                    break;
                                case 2:
                                    if (saveData.Items[Slot + 1] < 99999)
                                    {
                                        while (true)
                                        {
                                            if (Items.TryGetValue(saveData.Items[Slot + 1], out var Item))
                                            {
                                                string Name = Item.Name;
                                                Console.WriteLine("You already have {0} equipped in that slot. Would you like to replace it? \n \n1. Yes \n2. No", Name);
                                            }
                                            var confirm = Console.ReadKey(true);
                                            var bracelet = MenuCheck(confirm.Key);
                                            switch (bracelet)
                                            {
                                                case 1:
                                                    saveData.Items[Slot + 1] = Id;
                                                    break;
                                                case 2:
                                                    break;
                                                default:
                                                    Console.Clear();
                                                    Console.WriteLine($"{confirm.Key} is not a valid key, try again. \n");
                                                    continue;
                                            }
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        saveData.Items[Slot + 1] = Id;
                                    }
                                    break;
                                default:
                                    Console.Clear();
                                    Console.WriteLine($"{eslot.Key} is not a valid key, try again. \n");
                                    continue;
                            }
                            break;
                        }
                    }
                        break;
                case 12:
                    if (itemLevel > playerLevel)
                    {
                        Console.WriteLine("You are not a high enough level to use that, you must have combat level {0} to equip it.", itemLevel);
                    }
                    else
                    {
                        while (true)
                        {
                            Console.WriteLine("Equip in which slot, 1 or 2? \n");
                            var eslot = Console.ReadKey(true);
                            var option = MenuCheck(eslot.Key);
                            switch (option)
                            {
                                case 1:
                                    if (saveData.Items[Slot] < 99999)
                                    {
                                        while (true)
                                        {
                                            if (Items.TryGetValue(saveData.Items[Slot], out var Item))
                                            {
                                                string Name = Item.Name;
                                                Console.WriteLine("You already have {0} equipped in that slot. Would you like to replace it? \n \n1. Yes \n2. No", Name);
                                            }
                                            var confirm = Console.ReadKey(true);
                                            var ring = MenuCheck(confirm.Key);
                                            switch (ring)
                                            {
                                                case 1:
                                                    saveData.Items[Slot] = Id;
                                                    break;
                                                case 2:
                                                    break;
                                                default:
                                                    Console.Clear();
                                                    Console.WriteLine($"{confirm.Key} is not a valid key, try again. \n");
                                                    continue;
                                            }
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        saveData.Items[Slot] = Id;
                                    }
                                    break;
                                case 2:
                                    if (saveData.Items[Slot + 1] < 99999)
                                    {
                                        while (true)
                                        {
                                            if (Items.TryGetValue(saveData.Items[Slot + 1], out var Item))
                                            {
                                                string Name = Item.Name;
                                                Console.WriteLine("You already have {0} equipped in that slot. Would you like to replace it? \n \n1. Yes \n2. No", Name);
                                            }
                                            var confirm = Console.ReadKey(true);
                                            var ring = MenuCheck(confirm.Key);
                                            switch (ring)
                                            {
                                                case 1:
                                                    saveData.Items[Slot + 1] = Id;
                                                    break;
                                                case 2:
                                                    break;
                                                default:
                                                    Console.Clear();
                                                    Console.WriteLine($"{confirm.Key} is not a valid key, try again. \n");
                                                    continue;
                                            }
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        saveData.Items[Slot + 1] = Id;
                                    }
                                    break;
                                case 3:
                                    if (saveData.Items[Slot + 2] < 99999)
                                    {
                                        while (true)
                                        {
                                            if (Items.TryGetValue(saveData.Items[Slot + 2], out var Item))
                                            {
                                                string Name = Item.Name;
                                                Console.WriteLine("You already have {0} equipped in that slot. Would you like to replace it? \n \n1. Yes \n2. No", Name);
                                            }
                                            var confirm = Console.ReadKey(true);
                                            var ring = MenuCheck(confirm.Key);
                                            switch (ring)
                                            {
                                                case 1:
                                                    saveData.Items[Slot + 2] = Id;
                                                    break;
                                                case 2:
                                                    break;
                                                default:
                                                    Console.Clear();
                                                    Console.WriteLine($"{confirm.Key} is not a valid key, try again. \n");
                                                    continue;
                                            }
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        saveData.Items[Slot + 2] = Id;
                                    }
                                    break;
                                case 4:
                                    if (saveData.Items[Slot + 3] < 99999)
                                    {
                                        while (true)
                                        {
                                            if (Items.TryGetValue(saveData.Items[Slot + 3], out var Item))
                                            {
                                                string Name = Item.Name;
                                                Console.WriteLine("You already have {0} equipped in that slot. Would you like to replace it? \n \n1. Yes \n2. No", Name);
                                            }
                                            var confirm = Console.ReadKey(true);
                                            var ring = MenuCheck(confirm.Key);
                                            switch (ring)
                                            {
                                                case 1:
                                                    saveData.Items[Slot + 3] = Id;
                                                    break;
                                                case 2:
                                                    break;
                                                default:
                                                    Console.Clear();
                                                    Console.WriteLine($"{confirm.Key} is not a valid key, try again. \n");
                                                    continue;
                                            }
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        saveData.Items[Slot + 3] = Id;
                                    }
                                    break;
                                default:
                                    Console.Clear();
                                    Console.WriteLine($"{eslot.Key} is not a valid key, try again. \n");
                                    continue;
                            }
                            break;
                        }
                    }
                    break;
            }
        }
        public static int MenuCheck(ConsoleKey Key)
        {
            if (menuOption.TryGetValue(Key, out var option))
            {
                return option;
            }
            return -1;
        }
    }
    public class SaveFile
    {
        public string Name { get; set; } = "";
        //HP, Strength, Damage, Physical Defense, Magic Defense, True Defense, Mana, Crit Chance, Crit Damage, HP Regen, Mana Regen
        public int[] Stats { get; set; } = [100, 25, 0, 0, 0, 50, 20, 150, 0, 5];
        //Combat, Mining, Farming, Fishing, Foraging, Woodworking, Enchanting and Alchemy
        public int[] Levels { get; set; } = [1, 1, 1, 1, 1, 1, 1, 1];
        public int[] Exp { get; set; } = [0, 0, 0, 0, 0, 0, 0, 0];
        //Quantities of which item IDs the player has.
        public int[] Inventory { get; set; } = new int[5001];
        //Equipped Items
        public int[] Items { get; set; } = new int[15];
        public bool New { get; set; } = true;
    }
    public class EnemyData
    {
        public int Id;
        public string Name;
        public int Level;
        //-1 Team, 0 Passive, 1 Normal, 2 Heavy, 3 Mini-Boss, 4 Boss, 5 God
        public int Type;
        //HP, Strength, Damage, Physical Defense, Magic Defense, True Defense, Mana, Crit Chance, Crit Damage, HP Regen, Mana Regen
        public int[] Stats;

        public EnemyData(int Id, string Name, int Level, int Type, int[] Stats)
        {
            this.Id = Id;
            this.Name = Name;
            this.Level = Level;
            this.Type = Type;
            this.Stats = Stats;
        }
    }
    public class ItemData
    {
        public int Id;
        public string Name;
        public string Description;
        public int Level;
        //0 none, 1 Head, 2 Neck, 3 Chest, 4 Belt, 5 Pants, 6 Boots, 7 Gloves, 8 Tool/Weapon, 9 Special, 10 Bracelet, 11 Ring
        public int ArmourType;
        //HP, Strength, Damage, Physical Defense, Magic Defense, True Defense, Mana, Crit Chance, Crit Damage, HP Regen, Mana Regen
        public int[] Stats;
        //0 Flat Increase, 1 Multiplicitive Increase, 2 Percentage Increase
        public int[] MultPercent;

        public ItemData(int Id, string Name, string Description, int Level, int ArmourType, int[] Stats, int[] MultPercent)
        {
            this.Id = Id;
            this.Name = Name;
            this.Description = Description;
            this.Level = Level;
            this.ArmourType = ArmourType;
            this.Stats = Stats;
            this.MultPercent = MultPercent;
        }
    }
    public class Attack
    {
        public int Id;
        public string Name;
        //0 Typeless, 1 Physical, 2 Magical
        //Typeless damage gets no multipliers and has infinite pierce, true defense blocks it.
        //Physical is amplified by Strength and Magical is amplified by max Mana. Both pierce their respective defense types and ignore eachothers'.
        public int Type;
        public int Damage;
        public int Pierce;
        public int Accuracy;

        public Attack(int Id, string Name, int Type, int Damage, int Pierce, int Accuracy)
        {
            this.Id = Id;
            this.Name = Name;
            this.Type = Type;
            this.Damage = Damage;
            this.Pierce = Pierce;
            this.Accuracy = Accuracy;
        }
    }
}
