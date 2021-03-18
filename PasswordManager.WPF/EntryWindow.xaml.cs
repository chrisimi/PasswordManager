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

            List<Data> data = new List<Data>();
            data.Add(new Data() { IdCheck = 1, RessourceName = "Facebook", Username = "hello" });
            data.Add(new Data() { IdCheck = 2, RessourceName = "Insta", Username = "world123" });
            data.Add(new Data() { IdCheck = 3, RessourceName = "Github", Username = "hi.at" });

            dgData.ItemsSource = data;

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        
        public class Data
        {
            public int IdCheck { get; set; }

            public string RessourceName { get; set; }

            public string Username { get; set; }

            public string Password { get; set; }

            IList<Data> DeleteSth();
            IList<Data> EditSth();

        }

    }
}
