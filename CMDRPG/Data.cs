using System.Data.SqlTypes;
using System.Numerics;
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
        public static void ConsumeItem(int Id)
        {
            Items.TryGetValue(Id, out var Item);
            var Modifiers = Item.Modifiers;
        }
        public static void Modifiers()
        {
            //
            //Possibly gonna use enums, modifiers for stats. 3 types I want for now:
            //
            //Battle modifiers that last until the end of a fight
            //Skill modifiers that last x actions
            //Instant boosts to stats. HP heal potions, Mana restore potions, Terraria heart crystal type items.
            //
        }
    }
    public class SaveFile
    {
        //0 = RPG, 1 = Survivalcraft, space for more
        public int Gamemode { get; set; } = 0;
        public string Name { get; set; } = "";
        //HP, Strength, Damage, Physical Defense, Magic Defense, True Defense, Mana, Crit Chance, Crit Damage, HP Regen, Mana Regen
        public int[] Stats { get; set; } = [100, 25, 0, 0, 0, 50, 20, 150, 0, 5];
        //Combat, Mining, Farming, Fishing, Foraging, Woodworking, Enchanting and Alchemy
        public int[] Levels { get; set; } = [1, 1, 1, 1, 1, 1, 1, 1];
        public int[] Exp { get; set; } = [0, 0, 0, 0, 0, 0, 0, 0];
        //Max value of 179769313486231590772930519078902473361797697894230657273430081157732675805500963132708477322407536021120113879871393357658789768814416622492847430639474124377767893424865485276302219601246094119453082952085005768838150682342462881473913110540827237163350510684586298239947245938479716304835356329624224137215  (UInt1024)
        public BigInteger Money { get; set; } = 0;
        //Quantities of which item IDs the player has.
        public List<Int128> Inventory { get; set; } = new();
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
        //0 General, 1 Head, 2 Neck, 3 Chest, 4 Belt, 5 Pants, 6 Boots, 7 Gloves, 8 Tool/Weapon, 9 Special, 10 Bracelet, 11 Ring, 12 Consumable
        public int ItemType;
        
        ////Combat, Mining, Farming, Fishing, Foraging, Woodworking, Enchanting and Alchemy
        public int[] Levels;
        //HP, Strength, Damage, Physical Defense, Magic Defense, True Defense, Mana, Crit Chance, Crit Damage, HP Regen, Mana Regen
        public int[] Stats;
        //0 Flat Increase, 1 Multiplicitive Increase, 2 Percentage Increase
        public int[] MultPercent;
        public int[] Modifiers;

        public ItemData(int Id, string Name, string Description, int ItemType, int[] Levels, int[] Stats, int[] MultPercent, int[] Modifiers)
        {
            this.Id = Id;
            this.Name = Name;
            this.Description = Description;
            this.ItemType = ItemType;
            this.Levels = Levels;
            this.Stats = Stats;
            this.MultPercent = MultPercent;
            this.Modifiers = Modifiers;
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
    public class Modifier
    {
        public int Id;
        public string Name;
        //0 perma, 1 restore, 2 x actions left, 3 until end of battle, 4 While equipped
        public int DurationType;
        public int TurnDuration;
        public int[] Stats;
        public int[] MultPercent;

        public Modifier(int Id, string Name, int DurationType, int TurnDuration, int[] Stats, int[] MultPercent)
        {
            this.Id = Id;
            this.Name = Name;
            this.DurationType = DurationType;
            this.TurnDuration = TurnDuration;
            this.Stats = Stats;
            this.MultPercent = MultPercent;
        }
    }
}