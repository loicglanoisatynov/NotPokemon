using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PokeWish.MVVM.ViewModel;

namespace PokeWish.MVVM.View
{
    /// <summary>
    /// Logique d'interaction pour PokemonsSubview.xaml
    /// </summary>
    public partial class PokemonsSubview : UserControl
    {
        public PokemonsSubview()
        {
            InitializeComponent();
            this.DataContext = new PokemonsSubviewVM(((App)Application.Current).CurrentPlayer.ID);
        }
    }
}
