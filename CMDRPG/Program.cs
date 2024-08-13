using System;
using System.Text.Json;

public class Game
{
    public static int MenuID = 0;
    public static Random rnd = new Random();
    public static Dictionary<short, ItemData> Items = new Dictionary<short, ItemData>();
    public static Dictionary<ConsoleKey, int> menuOption = new Dictionary<ConsoleKey, int>()
    {
        { ConsoleKey.D1, 1 },
        { ConsoleKey.NumPad1, 1 },
        { ConsoleKey.D2, 2 },
        { ConsoleKey.NumPad2, 2 },
        { ConsoleKey.D3, 3 },
        { ConsoleKey.NumPad3, 3 },
        { ConsoleKey.D4, 4 },
        { ConsoleKey.NumPad4, 4 },
        { ConsoleKey.D5, 5 },
        { ConsoleKey.NumPad5, 5 },
        { ConsoleKey.D6, 6 },
        { ConsoleKey.NumPad6, 6 },
        { ConsoleKey.D7, 7 },
        { ConsoleKey.NumPad7, 7 },
        { ConsoleKey.D8, 8 },
        { ConsoleKey.NumPad8, 8 },
        { ConsoleKey.D9, 9 },
        { ConsoleKey.NumPad9, 9 },
        { ConsoleKey.D0, 0 },
        { ConsoleKey.NumPad0, 0 },
        { ConsoleKey.I, 11 }
    };
    public class Data
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
            Console.WriteLine("Select a save to continue:");
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
        public static async Task Skill(byte Skill, int Duration, short ID, short Min, short Max, int EMin, int EMax)
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
            Reward(Skill, ID, Min, Max, EMin, EMax);
            Console.WriteLine("Done! You now have {0} {1}. \n", saveData.Inventory[ID], Item);
        }
        public static void Reward(byte Skill, short ID, short Min, short Max, int EMin, int EMax)
        {
            if (Items.ContainsKey(ID))
            {
                saveData.Inventory[ID] += (short)rnd.Next(Min, Max + 1);
            }
            saveData.Exp[Skill] += rnd.Next(EMin, EMax + 1);
        }
        public static void BattleData()
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
        public short[] Levels { get; set; } = [1, 1, 1, 1, 1, 1, 1, 1];
        public int[] Exp { get; set; } = [0, 0, 0, 0, 0, 0, 0, 0];
        //Quantities of which item IDs the player has.
        public short[] Inventory { get; set; } = new short[5000];
        //Equipped Items
        public short[] Items { get; set; } = new short[12];
        public bool New { get; set; } = true;
    }
    public class ItemData
    {
        public short Id;
        public string Name;
        public string Description;
        public short Level;
        public bool Equipable;

        public ItemData(short Id, string Name, string Description, short Level, bool Equipable)
        {
            this.Id = Id;
            this.Name = Name;
            this.Description = Description;
            this.Level = Level;
            this.Equipable = Equipable;
        }
    }
    public class Menu
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
                        Inventory(); break;
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
                if (!menuOption.TryGetValue(place.Key, out var option))
                {
                    Console.Clear();
                    Console.WriteLine($"{place.Key} is not a valid key, try again.");
                    continue;
                }
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
                        Inventory(); break;
                }
                break;
            }
        }
        public static void Inventory()
        {
            var key = Console.ReadKey(true);
            while (true)
            {
                if (!menuOption.TryGetValue(key.Key, out var option))
                {
                    Console.Clear();
                    Console.WriteLine($"{key.Key} is not a valid key, try again.");
                    continue;
                }
                switch(option)
                {
                    case 0:
                        Data.Back(); break;
                    case 1:
                        Console.WriteLine("Stuff"); break;
                    case 99:
                        Exit(); break;
                }
            }
        }
        public static void Quests()
        {
            Console.Clear();
            Console.WriteLine("This doesn't exist, probably never will.");
            Console.ReadKey();
            MainM();
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
                var place = Console.ReadKey();
                if (!menuOption.TryGetValue(place.Key, out var option))
                {
                    Console.Clear();
                    Console.WriteLine($"{place.Key} is not a valid key, try again.");
                    continue;
                }
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
                        Weapons();  break;
                    case 5:
                        Tavern();  break;
                    case 6:
                        Woods(); break;
                    case 11:
                        Menu.Inventory(); break;
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
                var act = Console.ReadKey(true);
                if (!menuOption.TryGetValue(act.Key, out var option))
                {
                    Console.Clear();
                    Console.WriteLine($"{act.Key} is not a valid key, try again.");
                    continue;
                }
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
                        break;
                    case 11:
                        Menu.Inventory(); break;
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
    public static void DictAdd()
    {
        ItemData[] items = {
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
    public static void Main()
    {
        Console.Title = "CMDRPG";
        Directory.CreateDirectory(@"./Saves/");
        DictAdd();
        Console.WriteLine("Hewwo :3 \nPress any key to continue ^w^");
        Console.ReadKey();
        Console.Clear();
        while (true)
        {
            Console.WriteLine("Load Game or start a new adventure? \n \n1. New Game \n2. Load Game \n");
            var load = Console.ReadKey();
            if (!menuOption.TryGetValue(load.Key, out var option))
            {
                Console.Clear();
                Console.WriteLine($"{load.Key} is not a valid key, try again.");
                continue;
            }
            switch (option)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("Welcome 2 the game, hehe :3 \nYour goal is to just get better stuff! \nPress any key! \n");
                    Console.ReadKey();
                    Console.Clear();
                    Console.WriteLine("First, let's get started by making a name for yourself! \nType your name below to continue: \n");
                    var name = Console.ReadLine();
                    Console.Clear();
                    Data.saveData.Name = name;
                    Console.Write("Hi " + Data.saveData.Name + "! \nWelcome to CMD RPG \nPress any key! \n \n");
                    Console.ReadKey();
                    Console.Clear();
                    Console.WriteLine("In this game you have 7 main stats. HP or Hit/health Points, Strength which determines your damage, Defense for how much incoming damage you block and Mana for magic weapons. \nThen there is Crit Chance for how likely you are to score a critical hit and Crit Damage for how much more damage that hit does. \nLast but not least there is HP regen for how much HP you get back per turn. \n");
                    Console.WriteLine("Beyond that you also have to work on leveling skills. There are 8 of them: Combat, Mining, Farming, Fishing, Foraging, Woodworking, Enchanting and Alchemy \nPress any key! \n");
                    Console.ReadKey();
                    Console.Clear();
                    Console.WriteLine("Now, you might be asking, how do I get these skills leveled up? Good Question! It'll come with time to master, but starting off it's gonna be pretty easy. \nTo start off there will be basic tasks you can do in the first area, from there you'll be able to go and grind skills in their respective areas. \n");
                    Console.WriteLine("When you get higher skill levels you'll be able to use better tools and such. \nNow when you get to the first screen there might look like there's a lot to do, which there is, but it's not as scary as it seems. \nThere will be a few main buttons for your inventory and such, but it should all be pretty intuitive. \n");
                    Console.WriteLine("Now, when you get in, your first objective will be to get some wood to craft your first tools! \nKind of reminds me of some other game, but I'm not sure, hehe :3 \nPress any key to continue to the main attraction! \n");
                    Data.save();
                    Console.ReadKey();
                    Tutorial(); break;
                case 2:
                    Data.load(); break;
                case 11:
                    Console.WriteLine("How did you find this...? \n"); continue;
            }
            break;
        }
    }
    public static void Welcome()
    {
        Console.WriteLine($"Welcome back {Data.saveData.Name}! \nPress any key to continue UwU \n");
        Console.ReadKey();
        bool newPlayer = Data.saveData.New;
        if (newPlayer)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Hold up, it looks like you didn't complete the tutorial yet? \nJust in case as a refresher I'll have you restart it. \nUnless you want to skip that of course. \n \nPress T for tutorial or N for no tutorial. \n");
                var tutorial = Console.ReadKey();
                switch (tutorial.Key)
                {
                    case ConsoleKey.T:
                        Tutorial(); break;
                    case ConsoleKey.N:
                        Menu.MainM(); break;
                    default:
                        Console.Clear();
                        Console.WriteLine($"{tutorial.Key} is an invalid input, try again. \n");
                        continue;  
                }
                break;
            }
        }
        else
        {
            Menu.MainM();
        }
    }
    public static void Tutorial()
    {
        Console.Clear();
        Console.WriteLine("Welcome to the tutorial. \nThe first task is to get wood and craft your first set of tools. \nPress any key to continue. \n");
        Console.ReadKey();
        Console.Clear();
        while (true)
        {
            Console.WriteLine("Here you will see a mock up of the main menu to get you used to the main controls.");
            Console.WriteLine("After that you can explore around for yourself. For now go to places and go to the woods in the village to chop some trees. \n");
            Console.WriteLine("Main Menu: \n");
            Console.WriteLine("P: Places \nI: Inventory \nQ: Quests \nX: Exit \n");
            var next = Console.ReadKey();
            switch (next.Key)
            {
                case ConsoleKey.P:
                    Console.Clear();
                    while (true)
                    {
                        Console.WriteLine("Where would you like to go? \n \nPlaces: \n");
                        Console.WriteLine("1. Village (Lvl 0-5) \n2. Caves (Lvl 5-15) \n3. Mines (Lvl 15-25) \n4. Mountains (Lvl 25-40) \n");
                        Console.WriteLine("Type the number of where you would like to go below:");
                        var place = Console.ReadKey(true);
                        if (!menuOption.TryGetValue(place.Key, out var option))
                        {
                            Console.Clear();
                            Console.WriteLine($"{place.Key} is an invalid input, try again. \n");
                            continue;
                        }
                        switch (option)
                        {
                            case 1:
                                Console.Clear();
                                while(true)
                                {
                                    Console.WriteLine("You arrive in the town square.");
                                    Console.WriteLine("Where would you like to go? \n \nPlaces: \n");
                                    Console.WriteLine("1. Forge \n2. Workbench \n3. Armourer \n4. Weaponsmith \n5. Tavern \n6. Woods \n");
                                    var place2 = Console.ReadKey();
                                    if (!menuOption.TryGetValue(place2.Key, out var option2))
                                    {
                                        Console.Clear();
                                        Console.WriteLine($"{place2.Key} is an invalid input, try again. \n");
                                        continue;
                                    }
                                    switch (option2)
                                    {
                                        case 1:
                                            Console.Clear();
                                            Console.WriteLine("Wait until after the tutorial. \n"); continue;
                                        case 2:
                                            Console.Clear();
                                            Console.WriteLine("This comes at a later part of the tutorial. :3 \n"); continue;
                                        case 3:
                                            Console.Clear();
                                            Console.WriteLine("Wait until after the tutorial. \n"); continue;
                                        case 4:
                                            Console.Clear();
                                            Console.WriteLine("Wait until after the tutorial. \n"); continue;
                                        case 5:
                                            Console.Clear();
                                            Console.WriteLine("Wait until after the tutorial. \n"); continue;
                                        case 6:
                                            Console.Clear();
                                            Console.WriteLine("Good, now go chop and Oak Tree. \n");
                                            while (true)
                                            {
                                                Console.WriteLine("Wandering in the woods you think of what to do: \n");
                                                Console.WriteLine("1. Chop an Oak Tree \n2. Chop a Birch Tree \n3. Gather Sticks \n4. Pick up Stones \n5. Travel Deeper \n \n0. Go Back \n");
                                                var act = Console.ReadKey(true);
                                                if (!menuOption.TryGetValue(act.Key, out var option3))
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine($"{act.Key} is not a valid key, try again.");
                                                    continue;
                                                }
                                                switch (option3)
                                                {
                                                    case 0:
                                                        Console.Clear();
                                                        if (Data.saveData.Inventory[1] > 0)
                                                        {
                                                            while (true)
                                                            {
                                                                Console.WriteLine("Now that you're back, go to the work bench and craft a bushel of sticks. \n");
                                                                Console.WriteLine("You arrive in the town square.");
                                                                Console.WriteLine("Where would you like to go? \n \nPlaces: \n");
                                                                Console.WriteLine("1. Forge \n2. Workbench \n3. Armourer \n4. Weaponsmith \n5. Tavern \n6. Woods \n");
                                                                var place3 = Console.ReadKey();
                                                                if (!menuOption.TryGetValue(place3.Key, out var option4))
                                                                {
                                                                    Console.Clear();
                                                                    Console.WriteLine($"{place3.Key} is an invalid input, try again. \n");
                                                                    continue;
                                                                }
                                                                switch (option4)
                                                                {
                                                                    case 1:
                                                                        Console.Clear();
                                                                        Console.WriteLine("Wait until after the tutorial. \n"); continue;
                                                                    case 2:
                                                                        Console.Clear();
                                                                        Console.WriteLine("Now that you're here, you can craft a bushel of sticks. \n");
                                                                        while (true)
                                                                        {
                                                                            Console.WriteLine("Sitting in front of the workbench you think of what to craft:");
                                                                            Console.WriteLine("Oak Wood: {0}, Oak Sticks: {1}, Stone: {2}, Metal 1: {3} \n", Data.saveData.Inventory[1], Data.saveData.Inventory[11], Data.saveData.Inventory[21], Data.saveData.Inventory[61]);
                                                                            Console.WriteLine("1. Oak Sticks (Oak Wood: 1, Yield: 5) \nNo other options until after the tutorial. \n \n0. Go back \n");
                                                                            var craft = Console.ReadKey(true);
                                                                            if (!menuOption.TryGetValue(craft.Key, out var option5))
                                                                            {
                                                                                Console.Clear();
                                                                                Console.WriteLine($"{craft.Key} is an invalid input, try again. \n");
                                                                            }
                                                                            switch (option5)
                                                                            {
                                                                                case 1:
                                                                                    Console.Clear();
                                                                                    if (Data.saveData.Inventory[1] != 0)
                                                                                    {
                                                                                        Data.saveData.Inventory[1] -= 1;
                                                                                        Task.Run(() => Data.Skill(5, 5000, 11, 5, 5, 10, 10)).Wait();
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        Console.WriteLine("Not enough materials to craft... \n");
                                                                                    }
                                                                                    continue;
                                                                                case 0:
                                                                                    Console.Clear();
                                                                                    if (Data.saveData.Inventory[11] >= 5)
                                                                                    {
                                                                                        Console.WriteLine("Great Job, you finished the tutorial! \nNow time to play the real game, good luck and have fun.");
                                                                                        Console.WriteLine("Press any key to go to the main menu! \n");
                                                                                        Data.saveData.New = false;
                                                                                        Data.save();
                                                                                        Console.ReadKey(true);
                                                                                        Console.Clear();
                                                                                        Welcome();
                                                                                        break;
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        Console.WriteLine("Craft some sticks first silly! \n");
                                                                                        continue;
                                                                                    }
                                                                                default:
                                                                                    Console.Clear();
                                                                                    Console.WriteLine($"{craft.Key} is an invalid input, try again. \n");
                                                                                    continue;
                                                                            }
                                                                            break;
                                                                        }
                                                                        break;
                                                                    case 3:
                                                                        Console.Clear();
                                                                        Console.WriteLine("Wait until after the tutorial. \n"); continue;
                                                                    case 4:
                                                                        Console.Clear();
                                                                        Console.WriteLine("Wait until after the tutorial. \n"); continue;
                                                                    case 5:
                                                                        Console.Clear();
                                                                        Console.WriteLine("Wait until after the tutorial. \n"); continue;
                                                                    case 6:
                                                                        Console.Clear();
                                                                        Console.WriteLine("You were just here, silly. :3 \n"); continue;
                                                                    default:
                                                                        Console.Clear();
                                                                        Console.WriteLine($"{place3.Key} is an invalid input, try again. \n"); continue;
                                                                }
                                                                break;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("Chop some wood first :3 \n"); continue;
                                                        }
                                                        break;
                                                    case 1:
                                                        Console.Clear();
                                                        Task.Run(() => Data.Skill(4, 10000, 1, 1, 3, 10, 15)).Wait(); continue;
                                                    case 2:
                                                        Console.Clear();
                                                        Console.WriteLine("Wait until after the tutorial. \n"); continue;
                                                    case 3:
                                                        Console.Clear();
                                                        Console.WriteLine("Wait until after the tutorial. \n"); continue;
                                                    case 4:
                                                        Console.Clear();
                                                        Console.WriteLine("Wait until after the tutorial. \n"); continue;
                                                    case 5:
                                                        Console.Clear();
                                                        Console.WriteLine("Wait until after the tutorial. \n"); continue;
                                                    default:
                                                        Console.Clear();
                                                        Console.WriteLine($"{act.Key} is an invalid input, try again. \n"); continue;
                                                }
                                                break;
                                            }
                                            break;
                                        default:
                                            Console.Clear();
                                            Console.WriteLine($"{place2.Key} is an invalid input, try again. \n"); continue;
                                    }
                                    break;
                                }
                                break;
                            case 2:
                                Console.Clear();
                                Console.WriteLine("Not unlocked yet! Level is too low (Also it's the still tutorial silly). \n"); continue;
                            case 3:
                                Console.Clear();
                                Console.WriteLine("Not unlocked yet! Level is too low (Also it's the still tutorial silly). \n"); continue;
                            case 4:
                                Console.Clear();
                                Console.WriteLine("Not unlocked yet! Level is too low (Also it's the still tutorial silly). \n"); continue;
                            default:
                                Console.Clear();
                                Console.WriteLine($"{place.Key} is an invalid input, try again. \n"); continue;
                        }
                        break;
                    }
                    break;
                case ConsoleKey.I:
                    Console.Clear();
                    Console.WriteLine("Wait until after the tutorial goober. :3 \n"); continue;
                case ConsoleKey.Q:
                    Console.Clear();
                    Console.WriteLine("Wait until after the tutorial goober. :3 \n"); continue;
                case ConsoleKey.X:
                    Exit(); break;
                default:
                    Console.Clear();
                    Console.WriteLine($"{next.Key} is an invalid input, try again. \n"); continue;
            }
            break;
        }
    }
    public static void Exit()
    {
        Console.Clear();
        Data.save();
        Console.WriteLine("Game saved! Press any key to exit. :3 \n");
        Console.ReadKey();
    }
}