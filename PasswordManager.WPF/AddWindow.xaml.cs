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
    /// Interaktionslogik für AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        public Guid userId;

        public AddWindow(Guid userId)
        {
            InitializeComponent();

            this.userId = userId;




        }

        private void addData_Btn_Click(object sender, RoutedEventArgs e)
        {
            Entry entry = new Entry()
            {
                Key = tbKey.Text,
                Email = tbEmail.Text,
                Password = tbPassword.Text,
                Notes = tbNotes.Text,
                UserId = userId
                
            };

            EntryWindow.logic.Add(entry);

            var wnd = new EntryWindow(userId);
            wnd.Show();

            this.Close();

        }
    }
}
