using QuickAccessPlugin.ViewModels;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace QuickAccessPlugin.Views
{
    /// <summary>
    /// Interaction logic for DescriptionWindow.xaml
    /// </summary>
    public partial class DescriptionWindow : Window
    {
        public DescriptionWindow(DescriptionViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
            ((DescriptionViewModel)DataContext).CloseAction = Close;
            ((DescriptionViewModel)DataContext).AcceptAction = () => {
                DialogResult = true;
                Close();
            };
        }
    }
}
