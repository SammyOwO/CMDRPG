using System;
using System.Timers;

public class GameIg
{
    private static System.Timers.Timer skillTimer;

    public static void Main()
    {
        SetTimer();
        skillTimer.Enabled = false;
        //hp, strength, defense, mana, crit chance, crit damage, hp regen
        int[] stats = { 100, 25, 0, 50, 20, 150, 25 };
        Console.WriteLine("Hewwo :3 \nPress enter to continue ^w^");
        Console.ReadLine();
        Console.Clear();
        Console.WriteLine("Load Game or start a new adventure? \nType 'New' or 'Load' to continue!");
        string save = Console.ReadLine();
        if (save == "New")
        {
            Console.WriteLine("Welcome 2 the game, hehe :3 \nYour goal is to just get better stuff!");
            Console.ReadLine();
            Console.Clear();
        }
        else
        {
            Console.WriteLine("Welcome back, hehe :3");
        }
    }

    public static void SetTimer()
    {
        skillTimer = new System.Timers.Timer(250);
    }
}