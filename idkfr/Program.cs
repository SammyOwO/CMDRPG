using System;
using System.Timers;
using Newtonsoft.Json;

public class GameIg
{
    private static System.Timers.Timer skillTimer;
    public static class Globals
    {
        public static string saveFileOutline = @"{
        'Name': '',
        //hp, strength, defense, mana, crit chance, crit damage, hp regen
        'Stats': [100, 25, 0, 50, 20, 150, 25],
        //combat, mining, farming, fishing, foraging, woodworking, enchanting, alchemy
        'Levels': [1, 1, 1, 1, 1, 1, 1, 1],
        //skill level exp in same order as skill
        'Exp': [0, 0, 0, 0, 0, 0, 0, 0],
        'Inventory': [],
        'Quantity': [],
        //equipped items
        'Items': [],
        //is the player new or not, duhh
        'New': true
        }";
    }
    public class Save
    {
        public static void save()
        {
            SaveFile saveFileMake = JsonConvert.DeserializeObject<SaveFile>(Globals.saveFileOutline);
            string fullPath = @"C:\Users\hatma\source\repos\CMD-RPG\idkfr\Saves\" + saveFileMake.Name + ".json";
            File.WriteAllText(fullPath, Globals.saveFileOutline);
        }
    }
    public class SaveFile
    {
        public string Name;
        public int[] Stats;
        public int[] Levels;
        public int[] Exp;
        public int[] Inventory;
        public Int16[] Quantity;
        public int[] Items;
        public bool New;
    }

    public class ItemData
    {
        public Int16 Id;
        public string Name;
        public string Description;
        public int Level;
        public bool Equipable;
    }

    public static void Main()
    {
        SaveFile saveFileMake = JsonConvert.DeserializeObject<SaveFile>(Globals.saveFileOutline);
        SetTimer();
        skillTimer.Enabled = false;
        Console.WriteLine("Hewwo :3 \nPress any key to continue ^w^");
        Console.ReadKey();
        Console.Clear();
        Console.WriteLine("Load Game or start a new adventure? \nType 'New' or 'Load' to continue!");
        string load = Console.ReadLine();
        if (load == "New")
        {
            Console.Clear();
            Console.WriteLine("Welcome 2 the game, hehe :3 \nYour goal is to just get better stuff! \nPress any key!");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("First, let's get started by making a name for yourself! \nType your name below to continue:");
            string name = Console.ReadLine();
            Console.Clear();
            saveFileMake.Name = name;
            Console.Write("Hi " + saveFileMake.Name + "! \nWelcome to CMD RPG \nPress any key!");
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
            Globals.saveFileOutline = JsonConvert.SerializeObject(saveFileMake);
            Save.save();
            Tutorial();
        }
        else if (load == "Load")
        {
            Console.Clear();
            string SaveTest = @"C:\Users\hatma\source\repos\CMD-RPG\idkfr\Saves\";
            string SaveDir = @".\Saves\";
            var SaveList = Directory.EnumerateFiles(SaveTest);
            Console.WriteLine("Select a save to continue:");
            foreach (string saves in SaveList)
            {
                Console.WriteLine(Path.GetFileNameWithoutExtension(saves));
            }
            Console.WriteLine();
            string saveName = Console.ReadLine();
            string fullName = saveName = ".json";
            bool valid = File.Exists(SaveTest + fullName);
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Not a valid expression! Reopen the game to try again.");
        }
    }

    public static void SetTimer()
    {
        skillTimer = new System.Timers.Timer(250);
    }

    public static void Tutorial()
    {
        Console.Clear();
        Console.WriteLine(Globals.saveFileOutline);
        Console.ReadKey();
    }

    public static void menuMain()
    {

    }
}