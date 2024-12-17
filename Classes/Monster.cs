namespace PokeWish.Classes
{
    public class Monster
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Health { get; set; }
        public string? ImageURL { get; set; }

        public ICollection<PlayerMonster> PlayerMonsters { get; set; } = new List<PlayerMonster>();
        public ICollection<MonsterSpell> MonsterSpells { get; set; } = new List<MonsterSpell>();
    }
}
