using static Game;

namespace CMDRPG
{
    internal class Skills
    {
        public static async Task Skill(int Skill, int Duration, int ID, int Min, int Max, int EMin, int EMax)
        {
            Items.TryGetValue(ID, out var itemName);
            var Item = itemName.Name;
            var Time = Duration - ((Duration * Data.saveData.Levels[Skill]) / 100) + (Duration / 100);
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
                Data.saveData.Inventory[ID] += rnd.Next(Min, Max + 1);
            }
            ExpAward(Skill, EMin, EMax);
            Console.Clear();
            Console.WriteLine("Done! You now have {0} {1}. \n", Data.saveData.Inventory[ID], Item);
        }
        public static void ExpAward(int Skill, int Min, int Max)
        {
            Data.saveData.Exp[Skill] += rnd.Next(Min, Max + 1);
        }
        public static void BattleReward(int Id, int Level)
        {
            Enemies.TryGetValue(Id, out var Enemy);
            //P for Passive E for Enemy. I'm not refactoring this.
            var Pmin = (Level * 1);
            var Pmax = (Level * 2);
            var Emin = (Level * 3) * Enemy.Type;
            var Emax = (Level * 8) * (Enemy.Type + 1);
            if (Enemy.Type <= 0)
            {
                if (Enemy.Type < 0)
                {
                    Console.WriteLine("What? You get no reward for this, how did you do this? \n");
                }
                else
                {
                    ExpAward(0, Pmin, Pmax);
                }
            }
            else
            {
                ExpAward(0, Emin, Emax);
            }
            var Pwnage = rnd.Next(1, 100001);
            if (Pwnage == 23478)
            {
                Data.saveData.Items[2147483647] += 1;
            }
        }
    }
}
