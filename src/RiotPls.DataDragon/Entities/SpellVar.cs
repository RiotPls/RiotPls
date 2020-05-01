namespace RiotPls.DataDragon.Entities
{
    public class SpellVar
    {
        public string Link { get; }
        
        public double Coeff { get; }
        
        public string Key { get; }

        internal SpellVar(SpellVarDto dto)
        {
            Link = dto.Link;
            Coeff = dto.Coeff;
            Key = dto.Key;
        }
    }
}