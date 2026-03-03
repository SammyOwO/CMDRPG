namespace CMDRPG
{
    public class ItemData
    {
        public int Id;
        public string Name;
        public string Description;
        //0 General, 1 Head, 2 Neck, 3 Chest, 4 Belt, 5 Pants, 6 Boots, 7 Gloves, 8 Tool/Weapon, 9 Special, 10 Bracelet, 11 Ring, 12 Consumable
        public int ItemType;
        
        ////Combat, Mining, Farming, Fishing, Foraging, Woodworking, Enchanting and Alchemy
        public int[] Levels;
        //HP, Strength, Damage, Physical Defense, Magic Defense, True Defense, Mana, Crit Chance, Crit Damage, HP Regen, Mana Regen
        public int[] Stats;
        //0 Flat Increase, 1 Multiplicitive Increase, 2 Percentage Increase
        public int[] MultPercent;
        public int[] Modifiers;

        public ItemData(int Id, string Name, string Description, int ItemType, int[] Levels, int[] Stats, int[] MultPercent, int[] Modifiers)
        {
            this.Id = Id;
            this.Name = Name;
            this.Description = Description;
            this.ItemType = ItemType;
            this.Levels = Levels;
            this.Stats = Stats;
            this.MultPercent = MultPercent;
            this.Modifiers = Modifiers;
        }
    }
}