using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Xml;
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
                            Item(0); break;
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
        public static void Item(int Page)
        {
            List<int> Owned = new();
            for (int i = 0; i < Consumable.Count; i++)
                if (Data.saveData.Items[Consumable[i]] > 0)
                {
                    Owned.Add(Consumable[i]);
                }
            decimal Length = Convert.ToDecimal(Owned.Count);
            decimal Fourteen = 14;
            int Pages = (int)Math.Ceiling(Length / Fourteen);
            int Index = 0;
            if (Page > 0)
            {
                Index = (Page * 14);
            }
            Console.Clear();
            Console.WriteLine("Use which item? (Use A-F for 10-16) \n");
            for (int i = Index; i < Owned.Count; i++)
            {
                Items.TryGetValue(Owned[i], out var Consume);
                if (i % 14 != 0)
                {
                    Console.WriteLine($"{(i % 14) + 1}. {Consume.Name}: {Data.saveData.Items[Consume.Id]}");
                }
                else
                {
                    Console.ReadKey(true);
                }
            }
            Console.ReadKey(true);
        }
        public static void ItemSelect(int[] Owned, int Index)
        {
            while (true)
            {
                var choice = Console.ReadKey(true);
                var option = Data.MenuCheck(choice.Key);
                switch (option)
                {
                    case 0:
                        Menu.Canceled();
                        break;
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
                    case 6:
                        break;
                    case 7:
                        break;
                    case 8:
                        break;
                    case 9:
                        break;
                    case 10:
                        break;
                    case 11:
                        break;
                    case 12:
                        break;
                    case 13:
                        break;
                    case 14:
                        break;
                    case 15:
                        break;
                    case 16:
                        break;
                    default:
                        Menu.Invalid(choice);
                        continue;
                }
            }
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
            Skills.BattleReward(enemy.Id, Level);
            Data.Back();
        }
    }
}