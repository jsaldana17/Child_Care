using Child_Care_App.Database;
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

namespace Child_Care_App
{
    /// <summary>
    /// Interaction logic for LogIn.xaml
    /// </summary>
    public partial class LogIn : Window
    {
        private DatabaseContext db;

        public LogIn()
        {
            InitializeComponent();
        }

        //TESTING ONLY?
        public void InitDB()
        {
            string cmdText = "CREATE TABLE LogInTable (id varchar(100) primary key, username varchar(100), password varchar(100), admin varchar(100));";
            db = new DatabaseContext("LogInInformation.db", cmdText);
        }

        //TESTING ONLY?
        public void fillDummyData()
        {

        }

        private void btn_LogIn_Click(object sender, RoutedEventArgs e)
        {
            string text = txt_Username.Text;
            string password = txt_Password.Text;

        }


    }

    public class HeadOfHousehold
    {
        private Guid ID { get; set; }

        private string Name { get; set; }
    }
}
