using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using RM.Views.Controls;

namespace RM.Commands
{
    public class OpenPositionsCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var grid = parameter as Grid;
            if (grid == null )
                return;

            grid.Children.Clear();
            var positionsControl = new PositionsControl();
            grid.Children.Add( positionsControl );
        }
    }
}
