namespace PokeWish.Classes
{
    public class MonsterSpell
    {
        public int MonsterID { get; set; }
        public Monster Monster { get; set; }

        public int SpellID { get; set; }
        public Spell Spell { get; set; }
    }
}
