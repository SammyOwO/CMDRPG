using static Game;

namespace CMDRPG
{
    internal class Equip
    {
        public static void PreEquip(ItemData Item)
        {
            Console.Clear();
            var Id = Item.Id;
            var Slot = Item.ArmourType - 1; 
            bool isEquipped = false;
            for (int i = 0; i < Data.saveData.Items.Length; i++)
            {
                if (Data.saveData.Items[i] == Id && Item.ArmourType != 10 && Item.ArmourType != 11)
                {
                    isEquipped = true;
                }
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
            var Name = Item.Name;
            while (2 < 3)
            {
                if (isEquipped)
                {
                    Console.WriteLine($"You already have {Name} equipped. Would you like to remove or replace it?");
                    Console.WriteLine("1. Yes \n2. No \n");
                    var option = Console.ReadKey(true);
                    var choice = Data.MenuCheck(option.Key);
                    switch (choice)
                    {
                        case 1:
                            Console.Clear();
                            Init(Type, Item); break;
                        case 2:
                            Console.Clear();
                            Console.WriteLine("Equipping canceled."); break;
                        default:
                            Menu.Invalid(option); break;
                    }
                }
                else
                {
                    Init(Type, Item);
                }
                break;
            }
        }
        public static void Init(int Type, ItemData Item)
        {
            var Slot = Item.ArmourType - 1;
            Console.Clear();
            switch (Type)
            {
                case 0:
                    //Slot
                    break;
                case 1:
                    //2 Slots (10, 11)
                    break;
                case 2:
                    //4 Slots (12, 13, 14, 15)
                    break;
            }
        }
        public static void Unequip(ItemData Item)
        {
            bool Many = false;
            int Count = 0;
            var Id = Item.Id;
            var Type = Item.ArmourType - 1;
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
                case < 9:
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
                case >= 9:
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
                Console.WriteLine("You have more than 1 {0} equipped, would you like to unequip them all?\n", Item.Name);
                Console.WriteLine("1. Unequip All \n2. Unequip 1 \n \n0. Cancel");
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
