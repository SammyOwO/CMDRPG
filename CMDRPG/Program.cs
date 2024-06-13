using System;
using System.Data;
using System.Net.Security;
using System.Reflection.Metadata.Ecma335;
using System.Timers;
using Newtonsoft.Json;

public class Game
{
    private static System.Timers.Timer skillTimer;
    public static Dictionary<short, ItemData> Items = new Dictionary<short, ItemData>();
    public class Data
    {
        public static SaveFile saveData = new();
        public static void save()
        {
            var json = JsonConvert.SerializeObject(saveData);
            string fullPath = @".\Saves\" + saveData.Name + ".json";
            File.WriteAllText(fullPath, json);
        }
        public static void load()
        {
            Console.Clear();
            string saveDir = @".\Saves\";
            var saveList = Directory.EnumerateFiles(saveDir);
            Console.WriteLine("Select a save to continue:");
            foreach (string saves in saveList)
            {
                Console.WriteLine(Path.GetFileNameWithoutExtension(saves));
            }
            Console.WriteLine();
            while (true)
            {
                string saveFile = Console.ReadLine();
                bool valid = File.Exists(saveDir + saveFile + ".json");
                if (valid == true)
                {
                    Console.Clear();
                    using (StreamReader r = new StreamReader(saveDir + saveFile + ".json"))
                    {
                        string loadFile = r.ReadToEnd();
                        saveData = JsonConvert.DeserializeObject<SaveFile>(loadFile);
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
    }
    public class SaveFile
    {
        public string Name = "";
        public int[] Stats = [100, 25, 0, 50, 20, 150, 25];
        public int[] Levels = [1, 1, 1, 1, 1, 1, 1, 1];
        public int[] Exp = [0, 0, 0, 0, 0, 0, 0, 0];
        public short[] Inventory = new short[2000];
        public short[] Quantity = new short[2000];
        public short[] Items = new short[2000];
        public bool New = true;
    }
    public class ItemData
    {
        public short Id;
        public string Name;
        public string Description;
        public int Level;
        public bool Equipable;

        public ItemData(short Id, string Name, string Description, int Level, bool Equipable)
        {
            this.Id = Id;
            this.Name = Name;
            this.Description = Description;
            this.Level = Level;
            this.Equipable = Equipable;
        }
    }
    public static void SetTimer()
    {
        skillTimer = new System.Timers.Timer(250);
    }
    public static void DictAdd()
    {
        ItemData[] items = {
        new ItemData(1,"Oak Wood","A simple material for crafting.",0,false),
        new ItemData(11,"Stone","A simple material for crafting.",0,false),
        new ItemData(26,"Ore 1","The first ore used for crafting simple metal items.",0,false),
        new ItemData(51,"Ore 1 Bar","An ingot form of the first ore",0,false)
        };
        foreach (ItemData item in items)
        {
            Items.Add(item.Id, item);
        }
    }
    public static void Main()
    {
        Directory.CreateDirectory(@"./Saves/");
        SetTimer();
        DictAdd();
        skillTimer.Enabled = false;
        Console.WriteLine("Hewwo :3 \nPress any key to continue ^w^");
        Console.ReadKey();
        Console.Clear();
        while (true)
        {
            Console.WriteLine("Load Game or start a new adventure? \nType 'New' or 'Load' to continue! (Case sensitive)");
            string load = Console.ReadLine();
            switch (load)
            {
                case "New":
                    Console.Clear();
                    Console.WriteLine("Welcome 2 the game, hehe :3 \nYour goal is to just get better stuff! \nPress any key!");
                    Console.ReadKey();
                    Console.Clear();
                    Console.WriteLine("First, let's get started by making a name for yourself! \nType your name below to continue:");
                    string name = Console.ReadLine();
                    Console.Clear();
                    Data.saveData.Name = name;
                    Console.Write("Hi " + Data.saveData.Name + "! \nWelcome to CMD RPG \nPress any key!");
                    Console.ReadKey();
                    Console.Clear();
                    Console.WriteLine("In this game you have 7 main stats. HP or Hit/health Points, Strength which determines your damage, Defense for how much incoming damage you block and Mana for magic weapons. \nThen there is Crit Chance for how likely you are to score a critical hit and Crit Damage for how much more damage that hit does. \nLast but not least there is HP regen for how much HP you get back per turn.");
                    Console.WriteLine("\nBeyond that you also have to work on leveling skills. There are 8 of them: Combat, Mining, Farming, Fishing, Foraging, Woodworking, Enchanting and Alchemy \nPress any key!");
                    Console.ReadKey();
                    Console.Clear();
                    Console.WriteLine("Now, you might be asking, how do I get these skills leveled up? Good Question! It'll come with time to master, but starting off it's gonna be pretty easy. \nTo start off there will be basic tasks you can do in the first area, from there you'll be able to go and grind skills in their respective areas.");
                    Console.WriteLine("When you get higher skill levels you'll be able to use better tools and such. \nNow when you get to the first screen there might look like there's a lot to do, which there is, but it's not as scary as it seems. \nThere will be a few main buttons for your inventory and such, but it should all be pretty intuitive.");
                    Console.WriteLine("Now, when you get in, your first objective will be to get some wood to craft your first tools! \nKind of reminds me of some other game, but I'm not sure, hehe :3 \nPress any key to continue to the main attraction!");
                    Console.ReadKey();
                    Data.save();
                    Tutorial();
                    break;

                case "Load":
                    Data.load();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Invalid expression, try again. \n");
                    continue;
            }
            break;
        }
    }
    public static void Welcome()
    {
        Console.WriteLine("Welcome back " + Data.saveData.Name + "! \nPress any key to continue UwU");
        Console.ReadKey();
        bool newPlayer = Data.saveData.New;
        if (newPlayer == true)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Hold up, it looks like you didn't complete the tutorial yet? \nJust in case as a refresher I'll have you restart it. \nUnless you want to skip that of course. \n \nPress T for tutorial or N for no tutorial.");
                string tutorial = Console.ReadLine();
                if (tutorial == "t" || tutorial == "T")
                {
                    Tutorial();
                    break;
                }
                else if (tutorial == "n" || tutorial == "N")
                {
                    MenuMain();
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine(tutorial);
                    Console.WriteLine("Invalid input, try again. \n");
                    continue;
                }
            }
        }
        else
        {
            MenuMain();
        }
    }
    public static void Tutorial()
    {
        Console.Clear();
        Console.WriteLine("Welcome to the tutorial. \nThe first task is to get wood and craft your first set of tools. \nPress any key to continue.");
        Console.ReadKey();
        Console.Clear();
        while (true)
        {
            Console.WriteLine("Here you will see a mock up of the main menu to get you used to the main controls.");
            Console.WriteLine("After that you can explore around for yourself. For now go to places and go to the woods in the village to chop some trees. \n");
            Console.WriteLine("Main Menu: \n");
            Console.WriteLine("P: Places \nI: Inventory \nA: Achievements \nX: Exit \n");
            string next = Console.ReadLine();
            switch (next)
            {
                case "p": case "P":
                    
                    while(true)
                    {
                        Console.Clear();
                        Console.WriteLine("Where would you like to go? \n \nPlaces:");
                        Console.WriteLine("1. Village (Lvl 0-5) \n2. Caves (Lvl 5-15) \n3. Mines (Lvl 15-25) \n4. Mountains (Lvl 25-40) \n");
                        Console.WriteLine("Type the number of where you would like to go below:");
                        string place = Console.ReadLine();
                        switch (place)
                        {
                            case "1":
                                Console.Clear();
                                Console.WriteLine(); break;
                            case "2": 
                                Console.Clear();
                                Console.WriteLine("Not unlocked yet! Level is too low (Also it's the still tutorial silly). \n"); continue;
                            case "3":
                                Console.Clear();
                                Console.WriteLine("Not unlocked yet! Level is too low (Also it's the still tutorial silly). \n"); continue;
                            case "4":
                                Console.Clear();
                                Console.WriteLine("Not unlocked yet! Level is too low (Also it's the still tutorial silly). \n"); continue;
                        }
                        break;
                    }
                    break;
                case "i": case "I":
                    Console.Clear();
                    Console.WriteLine("Wait until after the tutorial goober. :3 \n"); continue;
                case "a": case "A":
                    Console.Clear();
                    Console.WriteLine("Wait until after the tutorial goober. :3 \n"); continue;
                case "x": case "X":
                    Exit(); break;
                default:
                    Console.Clear();
                    Console.WriteLine("Not a valid key, try again. \n");
                    continue;
            }
            break;
        }
    }
    public static void MenuMain()
    {
        Console.Clear();
        Console.WriteLine("Main Menu: \n");
        Console.WriteLine("P: Places \nI: Inventory \nA: Achievements \nX: Exit");
        string next = Console.ReadLine();
        while (true)
        {
            switch(next)
            {
                case "p": case "P":
                    MenuPlaces(); break;
                case "i": case "I":
                    MenuInventory(); break;
                case "a": case "A":
                    MenuAchievements(); break;
                case "x": case "X":
                    Exit(); break;
                default:
                    Console.WriteLine("Not a valid key, try again.");
                    continue;
            }
            break;
        }
    }
    public static void MenuPlaces()
    {

    }
    public static void MenuInventory()
    {

    }
    public static void MenuAchievements()
    {

    }
    public static void Exit()
    {
        Console.Clear();
        Data.save();
        Console.WriteLine("Game saved! Press any key to exit. :3");
        Console.ReadKey();
    }
}