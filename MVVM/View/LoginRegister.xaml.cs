using System.Windows;
using System.Windows.Controls;
using PokeWish.MVVM.ViewModel;

namespace PokeWish.MVVM.View
{
    public partial class LoginRegister : UserControl
    {
        public LoginRegister()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginRegisterVM viewModel)
            {
                viewModel.Password = (sender as PasswordBox)?.Password ?? string.Empty;
            }
        }

    }
}
