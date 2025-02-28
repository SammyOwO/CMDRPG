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
            Console.Clear();
            string saveDir = @".\Saves\";
            var saveList = Directory.EnumerateFiles(saveDir);
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
            saveData.Exp[Skill] += rnd.Next(EMin, EMax + 1);
            Console.WriteLine("Done! You now have {0} {1}. \n", saveData.Inventory[ID], Item);
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
        public static void BattleData(int EId, int Level, int Items)
        {

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
                    Village.Woods(); break;
            }
        }
        public static void Equip(int Id, int Type)
        {
            int Slot = Type - 1;
            switch (Type)
            {
                case < 11:
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
                    break;
                case 11:
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
                    break;
                case 12:
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
        //HP, Strength, Defense, Mana, Crit Chance, Crit Damage, HP Regen
        public int[] Stats { get; set; } = [100, 25, 0, 50, 20, 150, 25];
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
        public int EId;
        public string Name;
        public int Level;
        public int[] Stats;
        public int[] Items;

        public EnemyData(int EId, string Name, int Level, int[] Stats, int[] Items)
        {
            this.EId = EId;
            this.Name = Name;
            this.Level = Level;
            this.Stats = Stats;
            this.Items = Items;
        }
    }
    public class ItemData
    {
        public int Id;
        public string Name;
        public string Description;
        public int Level;
        public int ArmourType;

        public ItemData(int Id, string Name, string Description, int Level, int ArmourType)
        {
            this.Id = Id;
            this.Name = Name;
            this.Description = Description;
            this.Level = Level;
            this.ArmourType = ArmourType;
        }
    }
}
