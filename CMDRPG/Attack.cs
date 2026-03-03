namespace CMDRPG
{
    public class Attack
    {
        public int Id;
        public string Name;
        //0 Typeless, 1 Physical, 2 Magical
        //Typeless damage gets no multipliers and has infinite pierce, true defense blocks it.
        //Physical is amplified by Strength and Magical is amplified by max Mana. Both pierce their respective defense types and ignore eachothers'.
        public int Type;
        public int Damage;
        public int Pierce;
        public int Accuracy;

        public Attack(int Id, string Name, int Type, int Damage, int Pierce, int Accuracy)
        {
            this.Id = Id;
            this.Name = Name;
            this.Type = Type;
            this.Damage = Damage;
            this.Pierce = Pierce;
            this.Accuracy = Accuracy;
        }
    }
}