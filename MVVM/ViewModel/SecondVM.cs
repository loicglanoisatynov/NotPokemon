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

namespace PokeWish.MVVM.ViewModel
{
	public class SecondVM : BaseVM
	{
		public ICommand ExempleCommand { get; set; }

		public SecondVM()
		{
			ExempleCommand = new RelayCommand(ExecuteExempleCommand);
		}

		private void ExecuteExempleCommand()
		{
			MessageBox.Show("Text from SecondVM");
		}
	}
}