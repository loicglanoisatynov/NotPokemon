using CommunityToolkit.Mvvm.Input;
using PokeWish;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using PokeWish.MVVM.View;
using Microsoft.Data.SqlClient;
using PokeWish.MVVM.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PokeWish.MVVM.ViewModel
{
    public class SettingsVM : BaseVM
    {
        private string _dataSource;
        private string _initialCatalog;
        public SqlConnection? _currentConnection;


        public string DataSource
        {
            get => _dataSource;
            set
            {
                _dataSource = value;
                OnPropertyChanged();
            }
        }

        public string InitialCatalog
        {
            get => _initialCatalog;
            set
            {
                _initialCatalog = value;
                OnPropertyChanged();
            }
        }

        public ICommand LogAttemptToDBCommand { get; }
        public ICommand LogAttemptToDBCommandAsDefault { get; }

        public SettingsVM()
        {
            _dataSource = string.Empty; // Une chaîne vide comme valeur par défaut
            _initialCatalog = string.Empty;
            LogAttemptToDBCommand = new RelayCommand(LogAttemptToDB);
            LogAttemptToDBCommandAsDefault = new RelayCommand(LogAttemptToDBAsDefault);
        }


        private void LogAttemptToDB()
        {
            try
            {
                _currentConnection = DbService.OpenDb(_currentConnection, DataSource, InitialCatalog);
                if (_currentConnection != null)
                {
                    // HP-PAVILLON-LAP\SQLEXPRESS  ExerciceMonster
                    MessageBox.Show("Connected to database successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    //MainWindowVM mainWindowVM = (MainWindowVM)Application.Current.MainWindow.DataContext;

                    // Inside the LogAttemptToDB method, replace the problematic line with the following code:
                    var mainWindow = Application.Current.MainWindow as MainWindow;
                    if (mainWindow != null)
                    {
                        MessageBox.Show("MainWindow found");
                        mainWindow.MainContent.Content = new LoginRegister();

                    }
                    else
                    {
                        MessageBox.Show("Main window not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    //mainWindowVM.CurrentView = new LoginRegister();
                    
                }
                else
                {
                    MessageBox.Show("Failed to connect to database.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LogAttemptToDBAsDefault()
        {
            try
            {
                _currentConnection = DbService.OpenDb(_currentConnection, "HP-PAVILLON-LAP\\SQLEXPRESS", "ExerciceMonster");
                if (_currentConnection != null)
                {
                    // HP-PAVILLON-LAP\SQLEXPRESS  ExerciceMonster
                    MessageBox.Show("Connected to database successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    //MainWindowVM mainWindowVM = (MainWindowVM)Application.Current.MainWindow.DataContext;

                    // Inside the LogAttemptToDB method, replace the problematic line with the following code:
                    var mainWindow = Application.Current.MainWindow as MainWindow;
                    if (mainWindow != null)
                    {
                        mainWindow.MainContent.Content = new LoginRegister();

                    }
                    else
                    {
                        MessageBox.Show("Main window not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    //mainWindowVM.CurrentView = new LoginRegister();

                }
                else
                {
                    MessageBox.Show("Failed to connect to database.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public new event PropertyChangedEventHandler PropertyChanged = delegate { }; // Délégué vide


        protected new void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand_Bis : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand_Bis(Action execute, Func<bool>? canExecute = null) =>
    (_execute, _canExecute) = (execute, canExecute);


        public bool CanExecute(object parameter) => _canExecute == null || _canExecute();

        public void Execute(object parameter) => _execute();

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}