using CMDRPG;

public class Game
{
    public static string[] options;
    public static int MenuID = 0;
    public static Random rnd = new Random();
    public static Dictionary<int, EnemyData> Enemies = new Dictionary<int, EnemyData>();
    public static Dictionary<int, ItemData> Items = new Dictionary<int, ItemData>();
    public static Dictionary<int, Attack> Attacks = new Dictionary<int, Attack>();
    public static Dictionary<int, string> slotName = new Dictionary<int, string>()
    {
        { 0, "N/A" },
        { 1, "Head" },
        { 2, "Neck" },
        { 3, "Chest" },
        { 4, "Belt" },
        { 5, "Pants" },
        { 6, "Boots" },
        { 7, "Gloves" },
        { 8, "Tool/Weapon" },
        { 9, "Special" },
        { 10, "Bracelet 1" },
        { 11, "Bracelet 2" },
        { 12, "Ring 1" },
        { 13, "Ring 2" },
        { 14, "Ring 3" },
        { 15, "Ring 4" },
    };
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
        { ConsoleKey.Escape, 0 },
        { ConsoleKey.I, 11 },
        { ConsoleKey.End, 99 }
    };
    public static void DictAdd()
    {
        EnemyData[] enemies = {
                new EnemyData(0, "Dummy", 1, 0, [1,1,1,1,1,1,1,1,1,1,1])
            };
        foreach (EnemyData enemy in enemies)
        {
            Enemies.Add(enemy.Id, enemy);
        }
        ItemData[] items = {
                new ItemData(0,"Dummy's Defense","+1,000,000,000 HP",1000000000,1,[1000000000],[0]),
                new ItemData(1,"Oak Wood","A simple material for crafting.",0,0,[],[]),
                new ItemData(11,"Oak Stick","A simple stick from the first available wood in the game.",1,8,[0,0,1],[0,0,0]),
                new ItemData(21,"Stone 1","A simple material for crafting.",0,0,[],[]),
                new ItemData(51,"Ore 1","The first ore used for crafting simple metal items.",0,0,[],[]),
                new ItemData(76,"Ore 1 Bar","An ingot form of the first ore",0,0,[],[]),
                new ItemData(101,"Oak Helmet","A basic helmet whittled from Oak Wood.",1,1,[0,0,0,5],[0,0,0,0])
            };
        foreach (ItemData item in items)
        {
            Items.Add(item.Id, item);
        }
        Attack[] attacks = {
                new Attack(0,"Dummy's Laziness",0,0,0,100),
                new Attack(1, "Whack", 0, 1000000, -1, 100)
        };
        foreach (Attack attack in attacks)
        {
            Attacks.Add(attack.Id, attack);
        }
    }
    public static void Main()
    {
        DictAdd();
        Console.Title = "CMDRPG";
        Directory.CreateDirectory(@"./Saves/");
        Console.WriteLine("Hewwo :3 \nPress any key to continue ^w^ \n");
        Console.ReadKey(true);
        Console.Clear();
        StartUp();
    }
    public static void MakeNone()
    {
        for (int i = 0; i < Data.saveData.Items.Length; i++)
        {
            Data.saveData.Items[i] = 99999;
        }
    }
    public static void StartUp()
    {
        while (true)
        {
            Console.WriteLine("Load Game or start a new adventure? \n \n1. New Game \n2. Load Game \n");
            var load = Console.ReadKey(true);
            var option = Data.MenuCheck(load.Key);
            switch (option)
            {
                case 1:
                    Console.Clear();
                    NewPlayer();  break;
                case 2:
                    Console.Clear();
                    Data.load(); break;
                case 11:
                    Console.Clear();
                    Console.WriteLine("How did you find this...? \n"); continue;
                case 99:
                    Exit(); break;
                default:
                    Console.Clear();
                    Console.WriteLine($"{load.Key} is not a valid key, try again.");
                    continue;
            }
            break;
        }
    }
    public static void NewPlayer()
    {
        MakeNone();
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
        Tutorial();
    }
    public static void Welcome()
    {
        Console.WriteLine($"Welcome back {Data.saveData.Name}! \nPress any key to continue UwU \n");
        Console.ReadKey(true);
        bool newPlayer = Data.saveData.New;
        if (newPlayer)
        {
            Console.Clear();
            while (true)
            {
                Console.WriteLine("Hold up, it looks like you didn't complete the tutorial yet? \nJust in case as a refresher I'll have you restart it. \nUnless you want to skip that of course. \n \nPress T for tutorial or N for no tutorial. \n");
                var tutorial = Console.ReadKey(true);
                switch (tutorial.Key)
                {
                    case ConsoleKey.T:
                        Tutorial(); break;
                    case ConsoleKey.N:
                        Menu.MainM(); break;
                    case ConsoleKey.End:
                        Exit(); break;
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
        Console.WriteLine("Welcome to the tutorial. \nThe first task is to get wood and craft your first item: some sticks. \nPress any key to continue. \n");
        Console.ReadKey(true);
        Console.Clear();
        while (true)
        {
            Console.WriteLine("Here you will see a mock up of the main menu to get you used to the main controls.");
            Console.WriteLine("After that you can explore around for yourself. For now go to places and go to the woods in the village to chop some trees. \n");
            Console.WriteLine("Main Menu: \n");
            Console.WriteLine("P: Places \nI: Inventory \nQ: Quests \nEnd: Exit \n");
            var next = Console.ReadKey(true);
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
                        var option = Data.MenuCheck(place.Key);
                        switch (option)
                        {
                            case 1:
                                Console.Clear();
                                while(true)
                                {
                                    Console.WriteLine("You arrive in the town square.");
                                    Console.WriteLine("Where would you like to go? \n \nPlaces: \n");
                                    Console.WriteLine("1. Forge \n2. Workbench \n3. Armourer \n4. Weaponsmith \n5. Tavern \n6. Woods \n");
                                    var place2 = Console.ReadKey(true);
                                    var option2 = Data.MenuCheck(place2.Key);
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
                                            Console.WriteLine("Good, now go chop an Oak Tree. \n");
                                            while (true)
                                            {
                                                Console.WriteLine("Wandering in the woods you think of what to do: \n");
                                                Console.WriteLine("1. Chop an Oak Tree \n2. Chop a Birch Tree \n3. Gather Sticks \n4. Pick up Stones \n5. Travel Deeper \n \n0. Go Back \n");
                                                var action = Console.ReadKey(true);
                                                var option3 = Data.MenuCheck(action.Key);
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
                                                                var place3 = Console.ReadKey(true);
                                                                var option4 = Data.MenuCheck(place3.Key);
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
                                                                            var option5 = Data.MenuCheck(craft.Key);
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
                                                        Console.WriteLine($"{action.Key} is an invalid input, try again. \n"); continue;
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
                case ConsoleKey.End:
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
        Console.ReadKey(true);
    }
}