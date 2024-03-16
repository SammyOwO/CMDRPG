using System;
using System.Timers;
using Newtonsoft.Json;

public class GameIg
{
    private static System.Timers.Timer skillTimer;

    public class SaveFile
    {
        public string Name;
        public int[] Stats;
        public byte[] Levels;
        public Int16[] Inventory;
        public Int16[] ItemQuant;
        public Int16[] Items;
    }

    public class ItemData
    {
        public Int16 Id;
        public string Name;
        public string Description;
        public int Level;
    }

    public static void Main()
    {
        string saveFileOutline = @"{
        'Name': '',
        //hp, strength, defense, mana, crit chance, crit damage, hp regen
        'Stats': [100, 25, 0, 50, 20, 150, 25],
        //combat, mining, farming, fishing, foraging, woodworking, enchanting, alchemy
        'Levels': [1, 1, 1, 1, 1, 1, 1, 1],
        'Inventory': [],
        'ItemQuants': [],
        //equipped items
        'Items': []
        }";
        SaveFile saveFileMake = JsonConvert.DeserializeObject<SaveFile>(saveFileOutline);
        SetTimer();
        skillTimer.Enabled = false;
        Console.WriteLine("Hewwo :3 \nPress enter to continue ^w^");
        Console.ReadKey();
        Console.Clear();
        Console.WriteLine("Load Game or start a new adventure? \nType 'New' or 'Load' to continue!");
        string save = Console.ReadLine();
        if (save == "New")
        {
            Console.Clear();
            Console.WriteLine("Welcome 2 the game, hehe :3 \nYour goal is to just get better stuff! \nPress enter!");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("First, let's get started by making a name for yourself! \nType your name below to continue:");
            string name = Console.ReadLine();
            Console.Clear();
            saveFileMake.Name = name;
            Console.Write("Hi " + saveFileMake.Name + "! \nWelcome to CMD RPG \nPress enter!");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("In this game you have 7 main stats. HP or Hit/health Points, Strength which determines your damage, Defense for how much incoming damage you block and Mana for magic weapons. \nThen there is Crit Chance for how likely you are to score a critical hit and Crit Damage for how much more damage that hit does. \nLast but not least there is HP regen for how much HP you get back per turn.");
            Console.WriteLine("Beyond that you also have to work on leveling skills. There are 8 of them: Combat, Mining, Farming, Fishing, Foraging, Woodworking, Enchanting and Alchemy \nPress enter!");
            Console.ReadKey();
            Console.Clear();
        }
        else
        {
            Console.Clear();
        }
    }

    public static void SetTimer()
    {
        skillTimer = new System.Timers.Timer(250);
    }
}