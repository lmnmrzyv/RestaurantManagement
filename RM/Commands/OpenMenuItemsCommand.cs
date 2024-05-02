using RM.Views.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace RM.Commands
{
    public class OpenMenuItemsCommand : ICommand
    {
            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                var grid = parameter as Grid;

                if (grid == null)
                    return;

                grid.Children.Clear();

                var menuitemsControl = new MenuItemsControl();

                grid.Children.Add(menuitemsControl);
            }
        }
    }

