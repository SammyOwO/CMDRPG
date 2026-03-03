using System.Numerics;

namespace CMDRPG
{
    public class SaveFile
    {
        public int Gamemode { get; set; } = 0;
        public string Name { get; set; } = "";
        //[0] HP, [1] Strength, [3] Damage, [4] Physical Defense, [5] Magic Defense, [6] True Defense, [7] Mana, [8] Crit Chance, [9] Crit Damage, [10] HP Regen, [11] Mana Regen
        public int[] Stats { get; set; } = [100, 25, 0, 0, 0, 50, 20, 150, 0, 5];
        //[0] Combat, [1] Mining, [2] Farming, [3] Fishing, [4] Foraging, [5] Woodworking, [6] Enchanting and [7] Alchemy
        public int[] Levels { get; set; } = [1, 1, 1, 1, 1, 1, 1, 1];
        public int[] Exp { get; set; } = [0, 0, 0, 0, 0, 0, 0, 0];
        //Max value of 179769313486231590772930519078902473361797697894230657273430081157732675805500963132708477322407536021120113879871393357658789768814416622492847430639474124377767893424865485276302219601246094119453082952085005768838150682342462881473913110540827237163350510684586298239947245938479716304835356329624224137215  (UInt1024)
        public BigInteger Money { get; set; } = 0;
        //Quantities of which item IDs the player has.
        public List<Int128> Inventory { get; set; } = new();
        //Equipped Items
        public int[] Items { get; set; } = new int[15];
        public bool New { get; set; } = true;
    }
}
