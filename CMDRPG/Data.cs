using System.Text.Json;
using static Game;

namespace CMDRPG
{
    internal class Data
    {
        public static void Save()
        {
            string gamemodes = "";
            switch (saveData.Gamemode)
            {
                case 0:
                    gamemodes = "_RPG"; break;
                case 1:
                    gamemodes = "_Surv"; break;
            }
            var json = JsonSerializer.Serialize(saveData);
            string fullPath = @".\Saves\" + saveData.Name + gamemodes + ".json";
            File.WriteAllText(fullPath, json);
        }
        public static void Load()
        {
            string saveDir = @".\Saves\";
            var saveList = Directory.EnumerateFiles(saveDir);
            if (!saveList.Any())
            {
                Console.Clear();
                Console.WriteLine("There are no saves found, try again. \n");
                StartUp();
            }
            Console.WriteLine("Select a save to continue: \n");
            foreach (string saves in saveList)
            {
                Path.GetFileNameWithoutExtension(saves);
                Console.WriteLine("RPG Saves:");
                if (saves.Contains("_RPG"))
                {
                    Console.WriteLine(saves);
                }
            }
            Console.WriteLine();
            foreach (string saves in saveList)
            {
                Path.GetFileNameWithoutExtension(saves);
                Console.WriteLine("Survival Saves:");
                if (saves.Contains("_Surv"))
                {
                    Console.WriteLine(saves);
                }
            }
            Console.WriteLine();
            while (true)
            {
                var saveFile = Console.ReadLine();
                bool valid = File.Exists(saveDir + saveFile + ".json");
                if (valid == true)
                {
                    Console.Clear();
                    using (StreamReader r = new(saveDir + saveFile + ".json"))
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
}