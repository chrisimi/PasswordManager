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

namespace PasswordManager.WPF
{
    /// <summary>
    /// Interaktionslogik für EntryWindow.xaml
    /// </summary>
    public partial class EntryWindow : Window
    {
        public EntryWindow()
        {
            InitializeComponent();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
