using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDRPG
{
    public class EnemyData
    {
        public int Id;
        public string Name;
        public int Level;
        //-1 Team, 0 Passive, 1 Normal, 2 Mini-Boss, 3 Boss, 4 Super-Boss, 5 God
        public int Type;
        //HP, Strength, Damage, Physical Defense, Magic Defense, True Defense, Mana, Crit Chance, Crit Damage, HP Regen, Mana Regen
        public int[] Stats;

        public EnemyData(int Id, string Name, int Level, int Type, int[] Stats)
        {
            this.Id = Id;
            this.Name = Name;
            this.Level = Level;
            this.Type = Type;
            this.Stats = Stats;
        }
    }
}
