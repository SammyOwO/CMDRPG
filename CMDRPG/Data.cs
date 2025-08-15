using System.Text.Json;
using static Game;

namespace CMDRPG
{
    internal class Data
    {
        public static SaveFile saveData = new();
        public static void Save()
        {
            var json = JsonSerializer.Serialize(saveData);
            string fullPath = @".\Saves\" + saveData.Name + ".json";
            File.WriteAllText(fullPath, json);
        }
        public static void Load()
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
            if (Enemy.Type <= 0)
            {
                if (Enemy.Type < 0)
                {
                    Console.WriteLine("What? You get no reward for this, how did you do this?");
                }
                else
                {
                    ExpAward(0, Pmin, Pmax);
                }
            }
            else
            {
                ExpAward(0, Emin, Emax);
            }
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
        public static void EquipList()
        {
            for (int i = 0; i < saveData.Items.Length; i++)
            {
                if (saveData.Items[i] > -1)
                {
                    if (Items.TryGetValue(saveData.Items[i], out var item))
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
        }
        public static void Back()
        {
            Console.Clear();
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
                    Village.Woods(); break;
            }
        }
        public static void Equip(ItemData Item)
        {
            var Id = Item.Id;
            var itemLevels = Item.Levels;
            var playerLevels = saveData.Levels;
            var Slot = Item.ArmourType - 1;
            switch (Item.ArmourType)
            {
                case < 11:
                    if (itemLevels > playerLevels)
                    {
                        Console.WriteLine("You are not a high enough level to use that, you must have combat level {0} to equip it.", itemLevels);
                    }
                    else
                    {
                        if (saveData.Items[Slot] > -1)
                        {
                            while (true)
                            {
                                if (Items.TryGetValue(saveData.Items[Slot], out var EItem))
                                {
                                    string Name = EItem.Name;
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
                                        Menu.Invalid(confirm); continue;
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
                    if (itemLevels > playerLevels)
                    {
                        Console.WriteLine("You are not a high enough level to use that, you must have combat level {0} to equip it.", itemLevels);
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
                                    if (saveData.Items[Slot] > -1)
                                    {
                                        while (true)
                                        {
                                            if (Items.TryGetValue(saveData.Items[Slot], out var EItem))
                                            {
                                                string Name = EItem.Name;
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
                                                    Menu.Invalid(confirm); continue;
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
                                    if (saveData.Items[Slot + 1] > -1)
                                    {
                                        while (true)
                                        {
                                            if (Items.TryGetValue(saveData.Items[Slot + 1], out var EItem))
                                            {
                                                string Name = EItem.Name;
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
                                                    Menu.Invalid(confirm); continue;
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
                                    Menu.Invalid(eslot); continue;
                            }
                            break;
                        }
                    }
                    break;
                case 12:
                    if (itemLevels > playerLevels)
                    {
                        Console.WriteLine("You are not a high enough level to use that, you must have combat level {0} to equip it.", itemLevels);
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
                                    if (saveData.Items[Slot] > -1)
                                    {
                                        while (true)
                                        {
                                            if (Items.TryGetValue(saveData.Items[Slot], out var EItem))
                                            {
                                                string Name = EItem.Name;
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
                                                    Menu.Invalid(confirm); continue;
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
                                    if (saveData.Items[Slot + 1] > -1)
                                    {
                                        while (true)
                                        {
                                            if (Items.TryGetValue(saveData.Items[Slot + 1], out var EItem))
                                            {
                                                string Name = EItem.Name;
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
                                                    Menu.Invalid(confirm); continue;
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
                                    if (saveData.Items[Slot + 2] > -1)
                                    {
                                        while (true)
                                        {
                                            if (Items.TryGetValue(saveData.Items[Slot + 2], out var EItem))
                                            {
                                                string Name = EItem.Name;
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
                                                    Menu.Invalid(confirm); continue;
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
                                    if (saveData.Items[Slot + 3] > -1)
                                    {
                                        while (true)
                                        {
                                            if (Items.TryGetValue(saveData.Items[Slot + 3], out var EItem))
                                            {
                                                string Name = EItem.Name;
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
                                                    Menu.Invalid(confirm); continue;
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
                                    Menu.Invalid(eslot); continue;
                            }
                            break;
                        }
                    }
                    break;
            }
        }
        public static void Unequip(ItemData Item)
        {
            bool Many = false;
            int Count = 0;
            var Id = Item.Id;
            var Type = Item.ArmourType;
            for (int i = 0; i < saveData.Items.Length; i++)
            {
                if (saveData.Items[i] == Id)
                {
                    Count++;
                }
            }
            if (Count > 1)
            {
                Many = true;
            }
            switch (Type)
            {
                case < 11:
                    for (int i = 0; i < saveData.Items.Length; i++)
                    {
                        if (saveData.Items[i] == Id)
                        {
                            saveData.Items[i] = 0;
                        }
                    }
                    Console.Clear();
                    Console.WriteLine("You unequipped your {0}.", Item.Name);
                    break;
                case 11:
                    if (Many)
                    {
                        Multiple(Item);
                    }
                    else
                    {
                        for (int i = 0; i < saveData.Items.Length; i++)
                        {
                            if (saveData.Items[i] == Id)
                            {
                                saveData.Items[i] = 0;
                            }
                        }
                    }
                    break;
                case 12:
                    if (Many)
                    {
                        Multiple(Item);
                    }
                    else
                    {
                        for (int i = 0; i < saveData.Items.Length; i++)
                        {
                            if (saveData.Items[i] == Id)
                            {
                                saveData.Items[i] = 0;
                            }
                        }
                    }
                    break;
            }
        }
        public static void Multiple(ItemData Item)
        {
            Console.Clear();
            var Id = Item.Id;
            bool All = false;
            while (true)
            {
                Console.WriteLine("You have more than 1 {0} equipped, would you like to unequip them all?", Item.Name);
                var choice = Console.ReadKey(true);
                var option = MenuCheck(choice.Key);
                switch (option)
                {
                    case 1:
                        for (int i = 0; i < saveData.Items.Length; i++)
                        {
                            if (saveData.Items[i] == Id)
                            {
                                saveData.Items[i] = 0;
                            }
                        }
                        Console.Clear();
                        Console.WriteLine("You unequipped all of your {0}.", Item.Name);
                        break;
                    case 2:
                        for (int i = 0; i < saveData.Items.Length; i++)
                        {
                            if (!All)
                            {
                                if (saveData.Items[i] == Id)
                                {
                                    saveData.Items[i] = 0;
                                    All = true;
                                }
                            }
                        }
                        Console.Clear();
                        Console.WriteLine("You unequipped 1 of your {0}.", Item.Name);
                        break;
                    default:
                        Menu.Invalid(choice); continue;
                }
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
        //-1 Team, 0 Passive, 1 Normal, 2 Mini-Boss, 3 Boss, 4 Super-Boss, 5 God
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
        //Combat, Mining, Farming, Fishing, Foraging, Woodworking, Enchanting and Alchemy
        public int[] Levels;
        //0 none, 1 Head, 2 Neck, 3 Chest, 4 Belt, 5 Pants, 6 Boots, 7 Gloves, 8 Tool/Weapon, 9 Special, 10 Bracelet, 11 Ring
        public int ArmourType;
        //HP, Strength, Damage, Physical Defense, Magic Defense, True Defense, Mana, Crit Chance, Crit Damage, HP Regen, Mana Regen
        public int[] Stats;
        //0 Flat Increase, 1 Multiplicitive Increase, 2 Percentage Increase
        public int[] MultPercent;

        public ItemData(int Id, string Name, string Description, int[] Levels, int ArmourType, int[] Stats, int[] MultPercent)
        {
            this.Id = Id;
            this.Name = Name;
            this.Description = Description;
            this.Levels = Levels;
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