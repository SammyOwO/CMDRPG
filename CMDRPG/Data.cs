using System.Text.Json;
using static Game;

namespace CMDRPG
{
    internal class Data
    {

        public static Dictionary<int, EnemyData> Enemies = new Dictionary<int, EnemyData>();
        public static Dictionary<int, ItemData> Items = new Dictionary<int, ItemData>();
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
        public static void DictAdd()
        {
            EnemyData[] enemies = {
                new EnemyData(0, "Dummy", 1, [1,1,1,1,1,1,1,1], [0])
            };
            foreach (EnemyData enemy in enemies)
            {
                Enemies.Add(enemy.EId, enemy);
            }
            ItemData[] items = {
                new ItemData(0, "Dummy's Defense", "+1,000,000 HP", 1000000, true),
                new ItemData(1,"Oak Wood","A simple material for crafting.",0,false),
                new ItemData(11,"Oak Stick","A simple stick from the first available wood in the game.",0,false),
                new ItemData(21,"Stone","A simple material for crafting.",0,false),
                new ItemData(36,"Ore 1","The first ore used for crafting simple metal items.",0,false),
                new ItemData(61,"Ore 1 Bar","An ingot form of the first ore",0,false)
            };
            foreach (ItemData item in items)
            {
                Items.Add(item.Id, item);
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
        public int[] Items { get; set; } = new int[12];
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
        public bool Equipable;

        public ItemData(int Id, string Name, string Description, int Level, bool Equipable)
        {
            this.Id = Id;
            this.Name = Name;
            this.Description = Description;
            this.Level = Level;
            this.Equipable = Equipable;
        }
    }
}
