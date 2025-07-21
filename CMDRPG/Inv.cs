using static Game;

namespace CMDRPG
{
    public class Inv
    {
        public static void Inventory()
        {
            while (true)
            {
                options =
                    [
                    "Select Item",
                    "Show Equipped Items"
                    ];
                Console.WriteLine("Inventory \n");
                Menu.Options(options);
                Data.InvList();
                var key = Console.ReadKey(true);
                var option = Data.MenuCheck(key.Key);
                switch (option)
                {
                    case 0:
                        Data.Back(); break;
                    case 1:
                        Console.Clear();
                        SelectItems(); continue;
                    case 2:
                        Console.Clear();
                        EqippedItems(); break;
                    case 99:
                        Exit(); break;
                    default:
                        Menu.Invalid(key); continue;
                }
                break;
            }
        }
        public static void SelectItems()
        {
            Console.WriteLine("Select which item?: \n");
            Data.InvList();
            while (true)
            {
                string select = Console.ReadLine();
                var item = InventorySearch(select);
                if (item != null)
                {
                    Selected(item); break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("{0} is not a valid option, try again. \n", select); continue;
                }
            }
        }
        public static ItemData InventorySearch(string name)
        {
            int Id;
            if (!int.TryParse(name, out Id))
            {
                Id = -1;
            }
            for (int i = 0; i < Data.saveData.Inventory.Length; i++)
            {
                if (Data.saveData.Inventory[i] > 0)
                {
                    if (Items.TryGetValue(i, out var item))
                    {
                        if (name.ToLower() == item.Name.ToLower() || Id == item.Id)
                        {
                            return item;
                        }
                    }
                }
            }
            return null;
        }
        public static bool IsEquipped(ItemData Item)
        {
            for (int i = 0; i < Data.saveData.Items.Length; i++)
            {
                if (Data.saveData.Items[i] == Item.Id)
                {
                    return true;
                }
            }    
            return false;
        }
        public static void Selected(ItemData item)
        {
            Console.Clear();
            if (item.ArmourType > 0)
            {
                while (true)
                {
                    Console.WriteLine("What would you like to do with {0}? \n", item.Name);
                    Console.WriteLine("1. Inspect \n2. Delete \n3. Equip/Unequip \n \n0. Cancel \n");
                    var action = Console.ReadKey(true);
                    var option = Data.MenuCheck(action.Key);
                    switch (option)
                    {
                        case 0:
                            Console.Clear();
                            break;
                        case 1:
                            Inspect(item); continue;
                        case 2:
                            Delete(item); break;
                        case 3:
                            Console.Clear();
                            if (!IsEquipped(item))
                            {
                                Data.Equip(item);
                            }
                            else
                            {
                                Data.Unequip(item);
                            }
                            break;
                        default:
                            Menu.Invalid(action); continue;
                    }
                    break;
                }
            }
            else
            {
                while (true)
                {
                    Console.WriteLine("What would you like to do with {0}? \n", item.Name);
                    Console.WriteLine("1. Inspect \n2. Delete \n \n0. Cancel \n");
                    var action = Console.ReadKey(true);
                    var option = Data.MenuCheck(action.Key);
                    switch (option)
                    {
                        case 0:
                            Console.Clear();
                            break;
                        case 1:
                            Inspect(item); continue;
                        case 2:
                            Delete(item); break;
                        default:
                            Menu.Invalid(action); continue;
                    }
                    break;
                }
            }
        }
        public static void Inspect(ItemData item)
        {
            Console.Clear();
            Console.WriteLine("Level: {0}", item.Level);
            Console.WriteLine("Name: {0}", item.Name);
            Console.WriteLine("Description: {0}\n", item.Description);
        }
        public static void Delete(ItemData item)
        {
            Console.Clear();
            Console.WriteLine("Delete how many?: \n");
            string delamounts = Console.ReadLine();
            int.TryParse(delamounts, out int delamount);
            if (Data.saveData.Inventory[item.Id] - delamount <= 0)
            {
                Data.saveData.Inventory[item.Id] = 0;
                Console.Clear();
                Console.WriteLine("You deleted all of your {0}! \n", item.Name);
            }
            else
            {
                Data.saveData.Inventory[item.Id] -= delamount;
                Console.Clear();
                Console.WriteLine("You deleted {0} of your {1}! \n", delamount, item.Name);
            }
        }
        public static void EqippedItems()
        {
            while (true)
            {
                Console.WriteLine("1. Select Slot \n \n0. Go Back \n");
                Console.WriteLine("Equipped Items: \n");
                Data.EquipList();
                var select = Console.ReadKey(true);
                var option = Data.MenuCheck(select.Key);
                switch (option)
                {
                    case 0:
                        Console.Clear();
                        Inventory(); break;
                    case 1:
                        Console.Clear();
                        SelectSlot(); break;
                    default:
                        Menu.Invalid(select); continue;
                }
            }
        }
        public static void SelectSlot()
        {
            while(true)
            {
                Console.WriteLine("Select which slot?: \nNote: For slots 10 to 15 use 'A' through 'F'. \n0. Cancel");
                Data.EquipList();
                var select = Console.ReadKey(true);
                var option = Data.MenuCheck(select.Key);
                if (option == 0)
                {
                    Console.Clear();
                    Inventory();
                }
                else if (option > 0 && option < 16)
                {
                    Console.Clear();
                    SelectedItem(option); continue;
                }
                else
                {
                    Menu.Invalid(select); continue;
                }
            }
        }
        public static void SelectedItem(int slot)
        {
            if (!Items.TryGetValue(Data.saveData.Items[slot], out var Item))
            {
                Console.WriteLine($"Slot {slot} not selected due to it being empty.");
            }
            Selected(Item);
        }
    }
}