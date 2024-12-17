using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PokeWish.Classes;
using PokeWish.MVVM.View;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace PokeWish.MVVM.ViewModel
{
    public partial class PokemonsSubviewVM : BaseVM
    {
        private readonly AppDbContext _dbContext;

        public int ConnectedPlayerLoginID { get; }

        // Collections pour afficher les joueurs, Pokémon et attaques
        public ObservableCollection<Player> Players { get; set; } = [];
        public ObservableCollection<Monster> CurrentPlayerMonsters { get; set; } = []; // Pokémon du joueur sélectionné
        public ObservableCollection<Spell> Spells { get; set; } = [];

        // Propriétés sélectionnées
        [ObservableProperty]
        private Player? selectedPlayer;

        [ObservableProperty]
        private Monster? selectedMonster;

        [ObservableProperty]
        private Spell? selectedSpell;

        // Propriété pour le nom du nouveau joueur
        [ObservableProperty]
        private string newPlayerName = string.Empty;

        // Propriété pour le nom du nouveau Pokémon
        [ObservableProperty]
        private string newPokemonName = string.Empty;

        public PokemonsSubviewVM(int connectedPlayerLoginID)
        {
            ConnectedPlayerLoginID = connectedPlayerLoginID;
            _dbContext = ((App)Application.Current).DbContext ?? throw new InvalidOperationException("Database context is null!");

            LoadData();

            AddPlayerCommand = new RelayCommand(AddPlayer);
            RemovePlayerCommand = new RelayCommand(RemovePlayer);

            AddPokemonCommand = new RelayCommand(AddPokemon, () => SelectedPlayer != null);
            RemovePokemonCommand = new RelayCommand(RemovePokemon, () => SelectedMonster != null);
        }

        // Commandes
        public RelayCommand AddPlayerCommand { get; }
        public RelayCommand RemovePlayerCommand { get; }
        public RelayCommand AddPokemonCommand { get; }
        public RelayCommand RemovePokemonCommand { get; }

        // Chargement des données
        private void LoadData()
        {
            Players.Clear();
            Spells.Clear();

            if (_dbContext.Players != null)
            {
                var filteredPlayers = _dbContext.Players
                .Where(player => player.LoginID == ConnectedPlayerLoginID)
                .ToList();
                foreach (var player in _dbContext.Players)
                {
                    Players.Add(player);
                }
            }

            if (_dbContext.Spells != null)
            {
                foreach (var spell in _dbContext.Spells)
                    Spells.Add(spell);
            }

            LoadMonstersForSelectedPlayer();
        }

        private void LoadMonstersForSelectedPlayer()
        {
            CurrentPlayerMonsters.Clear();

            if (SelectedPlayer != null && _dbContext.PlayerMonsters != null)
            {
                var playerMonsters = _dbContext.PlayerMonsters
                    .Where(pm => pm.PlayerID == SelectedPlayer.ID)
                    .Select(pm => pm.Monster)
                    .ToList();

                foreach (var monster in playerMonsters)
                {
                    CurrentPlayerMonsters.Add(monster);
                }
            }
        }

        // Ajout d'un joueur
        private void AddPlayer()
        {
            if (string.IsNullOrWhiteSpace(NewPlayerName))
            {
                MessageBox.Show("Player name cannot be empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var newPlayer = new Player(NewPlayerName);
            _dbContext.Players?.Add(newPlayer); // Utilisation de l'opérateur de condition nulle
            _dbContext.SaveChanges();

            Players.Add(newPlayer);
            NewPlayerName = string.Empty;
        }

        // Suppression d'un joueur
        private void RemovePlayer()
        {
            if (SelectedPlayer != null)
            {
                // Supprimer également les Pokémon associés au joueur
                var playerMonsters = _dbContext.PlayerMonsters?.Where(pm => pm.PlayerID == SelectedPlayer.ID).ToList();
                if (playerMonsters != null && playerMonsters.Any())
                {
                    _dbContext.PlayerMonsters?.RemoveRange(playerMonsters);
                }

                _dbContext.Players?.Remove(SelectedPlayer);
                _dbContext.SaveChanges();

                Players.Remove(SelectedPlayer);
                SelectedPlayer = null; // Réinitialiser la sélection
                LoadMonstersForSelectedPlayer();
            }
        }

        // Ajout d'un Pokémon au joueur sélectionné
        private void AddPokemon()
        {
            if (string.IsNullOrWhiteSpace(NewPokemonName))
            {
                MessageBox.Show("Pokemon name cannot be empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (SelectedPlayer == null)
            {
                MessageBox.Show("Select a player to add a Pokemon.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var newMonster = new Monster { Name = NewPokemonName };
            _dbContext.Monsters?.Add(newMonster); // Utilisation de l'opérateur de condition nulle
            _dbContext.SaveChanges();

            var playerMonster = new PlayerMonster { PlayerID = SelectedPlayer.ID, MonsterID = newMonster.ID };
            _dbContext.PlayerMonsters?.Add(playerMonster); // Utilisation de l'opérateur de condition nulle
            _dbContext.SaveChanges();

            CurrentPlayerMonsters.Add(newMonster);
            NewPokemonName = string.Empty;
        }

        // Suppression d'un Pokémon
        private void RemovePokemon()
        {
            if (SelectedMonster != null)
            {
                _dbContext.Monsters.Remove(SelectedMonster);
                _dbContext.SaveChanges();

                CurrentPlayerMonsters.Remove(SelectedMonster);
                SelectedMonster = null;
            }
        }

        partial void OnSelectedPlayerChanged(Player? oldValue, Player? newValue)
        {
            LoadMonstersForSelectedPlayer();
        }
    }
}
