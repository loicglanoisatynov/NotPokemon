using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CommunityToolkit.Mvvm.Input;
using PokeWish.MVVM.ViewModel;
using PokeWish.MVVM.View;
using Microsoft.Data.SqlClient;
using PokeWish;

namespace PokeWish.MVVM.View
{
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
            DataContext = new MainViewVM();
        }
    }
}
