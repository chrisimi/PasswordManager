using PasswordManager.Domain;
using PasswordManager.Web;
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
        public static ILogic logic = new TestLogic();
        private Guid userId;

        public EntryWindow(Guid userId)
        {
            InitializeComponent();

            this.userId = userId;

            dgData.ItemsSource = logic.GetFromUser(userId);

            

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            var wnd = new AddWindow(Guid.NewGuid());
            wnd.Show();

            this.Close();

        
        }

        private void delBtn_Click(object sender, RoutedEventArgs e)
        {
            Entry entry = dgData.SelectedItem as Entry;
            logic.Remove(entry) ;

            dgData.ItemsSource = logic.GetFromUser(userId);
        }


        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            var wnd = new editWindow(Guid.NewGuid());
            wnd.Show();

            this.Close();
        }

      
    }
}
