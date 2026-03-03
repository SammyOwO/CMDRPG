namespace CMDRPG
{
    public class Modifier
    {
        public int Id;
        public string Name;
        //0 perma, 1 restore, 2 x actions left, 3 until end of battle, 4 While equipped
        public int DurationType;
        public int TurnDuration;
        public int[] Stats;
        public int[] MultPercent;

        public Modifier(int Id, string Name, int DurationType, int TurnDuration, int[] Stats, int[] MultPercent)
        {
            this.Id = Id;
            this.Name = Name;
            this.DurationType = DurationType;
            this.TurnDuration = TurnDuration;
            this.Stats = Stats;
            this.MultPercent = MultPercent;
        }
    }
}