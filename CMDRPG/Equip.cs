using static Game;

namespace CMDRPG
{
    internal class Equip
    {
        public static void PreEquip(ItemData Item)
        {
            var Id = Item.Id;
            var Slot = Item.ArmourType - 1;
            var Count = 0;
            bool isEquipped = false;
            for (int i = 0; i < Data.saveData.Items.Length; i++)
            {
                if (Data.saveData.Items[i] == Id)
                {
                    Count++;
                }
            }
            if (Count > 0)
            {
                isEquipped = true;
            }
            switch (Slot)
            {
                case < 8:
                    EquippedCheck(0, isEquipped, Item);
                    break;
                case 9:
                    EquippedCheck(1, isEquipped, Item);
                    break;
                case 10:
                    EquippedCheck(2, isEquipped, Item);
                    break;
            }
        }
        public static void EquippedCheck(int Type, bool isEquipped, ItemData Item)
        {
            if (isEquipped)
            {
                Console.WriteLine("You already have ");
            }
            else
            {
                Init(Type, Item);
            }
        }
        public static void Init(int Type, ItemData Item)
        {

        }
        public static void Unequip(ItemData Item)
        {
            bool Many = false;
            int Count = 0;
            var Id = Item.Id;
            var Type = Item.ArmourType;
            for (int i = 0; i < Data.saveData.Items.Length; i++)
            {
                if (Data.saveData.Items[i] == Id)
                {
                    Count++;
                }
            }
            if (Count > 1)
            {
                Many = true;
            }
            switch (Type)
            {
                case < 11:
                    for (int i = 0; i < Data.saveData.Items.Length; i++)
                    {
                        if (Data.saveData.Items[i] == Id)
                        {
                            Data.saveData.Items[i] = 0;
                        }
                    }
                    Console.Clear();
                    Console.WriteLine("You unequipped your {0}.", Item.Name);
                    break;
                case 11:
                    if (Many)
                    {
                        Multiple(Item);
                    }
                    else
                    {
                        for (int i = 0; i < Data.saveData.Items.Length; i++)
                        {
                            if (Data.saveData.Items[i] == Id)
                            {
                                Data.saveData.Items[i] = 0;
                            }
                        }
                    }
                    break;
                case 12:
                    if (Many)
                    {
                        Multiple(Item);
                    }
                    else
                    {
                        for (int i = 0; i < Data.saveData.Items.Length; i++)
                        {
                            if (Data.saveData.Items[i] == Id)
                            {
                                Data.saveData.Items[i] = 0;
                            }
                        }
                    }
                    break;
            }
        }
        public static void Multiple(ItemData Item)
        {
            Console.Clear();
            var Id = Item.Id;
            bool All = false;
            while (true)
            {
                Console.WriteLine("You have more than 1 {0} equipped, would you like to unequip them all?", Item.Name);
                var choice = Console.ReadKey(true);
                var option = Data.MenuCheck(choice.Key);
                switch (option)
                {
                    case 1:
                        for (int i = 0; i < Data.saveData.Items.Length; i++)
                        {
                            if (Data.saveData.Items[i] == Id)
                            {
                                Data.saveData.Items[i] = 0;
                            }
                        }
                        Console.Clear();
                        Console.WriteLine("You unequipped all of your {0}.", Item.Name);
                        break;
                    case 2:
                        for (int i = 0; i < Data.saveData.Items.Length; i++)
                        {
                            if (!All)
                            {
                                if (Data.saveData.Items[i] == Id)
                                {
                                    Data.saveData.Items[i] = 0;
                                    All = true;
                                }
                            }
                        }
                        Console.Clear();
                        Console.WriteLine("You unequipped 1 of your {0}.", Item.Name);
                        break;
                    default:
                        Menu.Invalid(choice); continue;
                }
            }
        }
    }
}
