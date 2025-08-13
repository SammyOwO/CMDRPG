using static Game;

namespace CMDRPG
{
    public class Battle
    {
        public static void Fight(int Id, int Level, int[] Equip)
        {
            Enemies.TryGetValue(Id, out var enemyOut);
            var enemy = EnemyInit(enemyOut, Level, Equip);
            Console.Clear();
            Console.WriteLine("The battle begins! Your enemy is a Lvl {0} {1}.\n", Level, enemy.Name);
            while (true)
            {
                Console.WriteLine("Lvl. {0} {1} \n", Level, enemy.Name);
                Console.WriteLine("1. Attack \n2. Use Item \n3. Check Enemy \n4. Flee \n");
                var choice = Console.ReadKey(true);
                var option = Data.MenuCheck(choice.Key);
                if (enemy.Stats[0] > 0)
                {
                    switch (option)
                    {
                        case 1:
                            Fight(); break;
                        case 2:
                            Item(); break;
                        case 3:
                            Console.Clear();
                            Check(enemy); continue;
                        case 4:
                            Console.Clear();
                            Flee(enemy.Type, Level); continue;
                        default:
                            Menu.Invalid(choice); continue;
                    }
                }
                else
                {
                    Console.Clear();
                    Win(enemy, Level);
                }
            }
        }
        public static EnemyData EnemyInit(EnemyData enemyData, int Level, int[] Equip)
        {
            var Equipped = new ItemData[Equip.Length];
            var Stat = enemyData.Stats;
            for (int i = 0; i < Equip.Length; i++)
            {
                Items.TryGetValue(Equip[i], out var item);
                Equipped[i] = item;
            }
            var AdjStat = EnemyInit1(Stat, Equipped);
            var HP = AdjStat[0];
            var Str = AdjStat[1];
            var Dam = AdjStat[2];
            var PDef = AdjStat[3];
            var MDef = AdjStat[4];
            var TDef = AdjStat[5];
            var Mana = AdjStat[6];
            var CC = AdjStat[7];
            var CD = AdjStat[8];
            var Regen = AdjStat[9];
            var MRegen = AdjStat[10];
            EnemyData enemyOut = new EnemyData(enemyData.Id, enemyData.Name, Level, enemyData.Type, [HP, Str, Dam, PDef, MDef, TDef, Mana, CC, CD, Regen, MRegen]);
            if (enemyOut != null)
            {
                return enemyOut;
            }

            return null;
        }
        public static int[] EnemyInit1(int[] Stat, ItemData[] Equip)
        {
            if (Equip.Length != 0)
            {
                for (int i = 0; i < Equip.Length; i++)
                {
                    for (int j = 0; j < Equip[i].MultPercent.Length; j++)
                    {
                        switch (Equip[i].MultPercent[j])
                        {
                            case 0:
                                Stat[j] += Equip[i].Stats[j];
                                break;
                            case 1:
                                Stat[j] *= Equip[i].Stats[j];
                                break;
                            case 2:
                                Stat[j] += (Stat[j] * Equip[i].Stats[j]) / 100;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            if (Stat != null)
            {
                return Stat;
            }
            return null;
        }
        public static void Fight()
        {
            //Yeah right, I have no clue.
        }
        public static void Item()
        {
            //See comment on line 105.
        }
        public static void Check(EnemyData enemy)
        {
            Console.WriteLine("Enemy Name: {0}, Level: {1}, HP: {2}, Strength: {3}, Defense: {4}", enemy.Name, enemy.Level, enemy.Stats[0], enemy.Stats[1], enemy.Stats[3]);
        }
        public static void Flee(int EType, int ELevel)
        {
            int PLevel = Data.saveData.Levels[0];
            int LevelDelta = (PLevel * 6) - ELevel;
            int Min = 0 + (LevelDelta / 10);
            int Max = 25 + LevelDelta - (EType * 4);
            int Escape = 0;
            switch (EType)
            {
                case 0:
                    Escape = 0; break;
                case 1:
                    Escape = 20; break;
                case 2:
                    Escape = 40; break;
                case 3:
                    Escape = 60; break;
                case 4:
                    Escape = 80; break;
                case 5:
                    Escape = 9000; break;
            }
            if (Min < 0)
            {
                Min = 0;
            }
            if (Min > 100 && EType != 5)
            {
                Console.WriteLine("You successfully managed to escape.");
                Data.Back();
            }
            if (Min > 9000 && EType == 5)
            {
                Console.WriteLine("Despite your immense strength, their godly powers keep you stuck here.");
            }
            if (Max > 100)
            {
                Max = 100;
            }
            int roll = rnd.Next(Min, Max);
            if (roll > Escape)
            {
                Console.WriteLine("You successfully managed to escape.");
                Data.Back();
            }
            else
            {
                Console.WriteLine("You tried your best, but failed to escape.");
            }
        }
        public static void Win(EnemyData enemy, int Level)
        {
            Console.WriteLine("The {0} has been defeated!", enemy.Name);
            Data.BattleReward(enemy.Id, Level);
            Data.Back();
        }
    }
}