using static Game;

namespace CMDRPG
{
    internal class MenuList
    {
        public static void Inv()
        {
            for (int i = 0; i < Data.saveData.Inventory.Length; i++)
            {
                if (Data.saveData.Inventory[i] <= 0) continue;
                if (Items.TryGetValue(i, out var item))
                {
                    Console.WriteLine("{0}: {1}", item.Name, Data.saveData.Inventory[i]);
                }
            }
            Console.WriteLine();
        }
        public static void Equips()
        {
            for (int i = 0; i < Data.saveData.Items.Length; i++)
            {
                if (Data.saveData.Items[i] > -1)
                {
                    if (Items.TryGetValue(Data.saveData.Items[i], out var item))
                    {
                        if (slotName.TryGetValue(i + 1, out var slot))
                        {
                            Console.WriteLine("{0}: {1}", slot, item.Name);
                        }
                    }
                }
                else
                {
                    if (slotName.TryGetValue(i + 1, out var slot))
                    {
                        Console.WriteLine("{0}: None", slot);
                    }
                }
            }
        }
        public static void Options(string[] options)
        {
            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine("{0}. {1}", i + 1, options[i]);
            }
            Console.WriteLine("\n0. Go Back | I: Inventory | End: Exit \n");
        }
    }
}
