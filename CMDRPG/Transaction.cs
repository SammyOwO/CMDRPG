using System.Numerics;

namespace CMDRPG
{
    public class Transaction
    {
        private static BigInteger max = BigInteger.Parse("179769313486231590772930519078902473361797697894230657273430081157732675805500963132708477322407536021120113879871393357658789768814416622492847430639474124377767893424865485276302219601246094119453082952085005768838150682342462881473913110540827237163350510684586298239947245938479716304835356329624224137215");
        private static BigInteger money = Data.saveData.Money;
        public static void Add(BigInteger value)
        {
            money += value;
            money = BigInteger.Clamp(value, 0, max);
        }

        public static void Buy(BigInteger value)
        {
            money -= value;
            if (money < 0)
            {
                money += value;
                Console.WriteLine("Broke bitch");
            }
            money = BigInteger.Clamp(money, 0, max);
        }
    }
}
