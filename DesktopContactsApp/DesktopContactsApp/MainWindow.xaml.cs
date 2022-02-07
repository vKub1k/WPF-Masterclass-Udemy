using DesktopContactsApp.Classes;
using SQLite;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesktopContactsApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Contact> contacts = new List<Contact>();

        public MainWindow()
        {
            InitializeComponent();

            ReadDatabase();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewContactWindow newContactWindow = new NewContactWindow();
            newContactWindow.ShowDialog();

            ReadDatabase();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {

        }

        void ReadDatabase()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.databasePath))
            {
                conn.CreateTable<Contact>();
                contacts = conn.Table<Contact>().ToList().OrderBy(c => c.Name).ToList();

                //var variable = from c2 in contacts
                //               orderby c2.Name
                //               select c2;
            }

            if (contacts != null)
            {
                //foreach (var item in contacts)
                //{
                //    contactsListView.Items.Add(new ListViewItem()
                //    { 
                //        Content = item
                //    });
                //}

                contactsListView.ItemsSource = contacts;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox searchTextBox = sender as TextBox;

            var filteredList = contacts.Where(c => c.Name.ToLower().Contains(searchTextBox.Text.ToLower())).ToList();

            //var filterdList2 = (from c2 in contacts
            //                   where c2.Name.ToLower().Contains(searchTextBox.Text.ToLower())
            //                   orderby c2.Email
            //                   select c2.Id).ToList();

            contactsListView.ItemsSource = filteredList;
        }

        private void contactsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Contact selectedContact = contactsListView.SelectedItem as Contact;

            if (selectedContact != null)
            {
                var newContactWindow = new ContactDetailsWindow(selectedContact);
                newContactWindow.ShowDialog();
            }

            ReadDatabase();
        }
    }
}
