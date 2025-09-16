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