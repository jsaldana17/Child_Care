using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using Child_Care_App.Database;
using System.Data.SQLite;


namespace Child_Care_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer dispatcherTimer; //using this timer to avoid threading issues
        private DatabaseContext db;
        private List<Client> items;

        public MainWindow()
        {
            InitializeComponent();
            testingBGColors();
            Init();
            InitDB();
        }

        //TESTING ONLY
        public void InitDB()
        {
            string cmdText = "CREATE TABLE MainMenuTable (id varchar(100) primary key, name varchar(100), Date varchar(100), CheckIn varchar(100), CheckIn_Minutes real, " +
                    "CheckOut varchar(100), CheckOut_Minutes real, TotalTime varchar(100), TotalCost real, Complete);";
            db = new DatabaseContext("MainDatabase.db", cmdText);
            //generateDBDummyData();
        }


        public void Init()
        {
            //Intialize timer
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatchTimerTick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

            //intialize fields
            items = new List<Client>();
        }

        private void dispatchTimerTick(object sender, EventArgs e)
        {
            string date = DateTime.Now.ToShortDateString() + "\n";
            this.lbl_DateTime.Content = date + DateTime.Now.ToLongTimeString();
        }

        #region TESTING ; REMOVE
        public void generateDBDummyData()
        { 
            db.InsertAll(new Client()
            {
                ID = Guid.NewGuid(),
                Name = "John Smith",
                Date = DateTime.Now.ToShortDateString(),
                CheckIn = "5:01 PM",
                CheckIn_Minutes = "995.98641165",
                CheckOut = "",
                CheckOut_Minutes = "",
                Total_Time = "",
                Total_Cost = 0.00,
                Transaction_Complete = "false"
            });

            db.InsertAll(new Client()
            {
                ID = Guid.NewGuid(),
                Name = "Emily Long",
                Date = DateTime.Now.ToShortDateString(),
                CheckIn = "3:34 PM",
                CheckIn_Minutes = "875.85416563",
                CheckOut = "",
                CheckOut_Minutes = "",
                Total_Time = "",
                Total_Cost = 0.00,
                Transaction_Complete = "false"
            });
            db.InsertAll(new Client()
            {
                ID = Guid.NewGuid(),
                Name = "Emily Johnson",
                Date = DateTime.Now.ToShortDateString(),
                CheckIn = "12:01 PM",
                CheckIn_Minutes = "652.51864165685",
                CheckOut = "",
                CheckOut_Minutes = "",
                Total_Time = "",
                Total_Cost = 0.00,
                Transaction_Complete = "false"
            });
            db.InsertAll(new Client()
            {
                ID = Guid.NewGuid(),
                Name = "Emily Stroller",
                Date = DateTime.Now.ToShortDateString(),
                CheckIn = "9:49 AM",
                CheckIn_Minutes = "406.8412616451",
                CheckOut = "",
                CheckOut_Minutes = "",
                Total_Time = "",
                Total_Cost = 0.00,
                Transaction_Complete = "false"
            });
            db.InsertAll(new Client()
            {
                ID = Guid.NewGuid(),
                Name = "Vanessa Rogers",
                Date = DateTime.Now.ToShortDateString(),
                CheckIn = "6:59 PM",
                CheckIn_Minutes = "1125.6541656351",
                CheckOut = "",
                CheckOut_Minutes = "",
                Total_Time = "",
                Total_Cost = 0.00,
                Transaction_Complete = "false"
            });
            db.InsertAll(new Client()
            {
                ID = Guid.NewGuid(),
                Name = "Jessica Alcala",
                Date = DateTime.Now.ToShortDateString(),
                CheckIn = "6:00 PM",
                CheckIn_Minutes = "1099.564165",
                CheckOut = "",
                CheckOut_Minutes = "",
                Total_Time = "",
                Total_Cost = 0.00,
                Transaction_Complete = "false"
            });
            db.InsertAll(new Client()
            {
                ID = Guid.NewGuid(),
                Name = "Connor James",
                Date = DateTime.Now.ToShortDateString(),
                CheckIn = "2:30 PM",
                CheckIn_Minutes = "799.964168541",
                CheckOut = "",
                CheckOut_Minutes = "",
                Total_Time = "",
                Total_Cost = 0.00,
                Transaction_Complete = "false"
            });
            db.InsertAll(new Client()
            {
                ID = Guid.NewGuid(),
                Name = "Amy Worth",
                Date = DateTime.Now.ToShortDateString(),
                CheckIn = "5:0 PM",
                CheckIn_Minutes = "994.1237853",
                CheckOut = "",
                CheckOut_Minutes = "",
                Total_Time = "",
                Total_Cost = 0.00,
                Transaction_Complete = "false"
            });
            db.InsertAll(new Client()
            {
                ID = Guid.NewGuid(),
                Name = "Amy Palmes",
                Date = DateTime.Now.ToShortDateString(),
                CheckIn = "4:21 PM",
                CheckIn_Minutes = "854.5641651",
                CheckOut = "",
                CheckOut_Minutes = "",
                Total_Time = "",
                Total_Cost = 0.00,
                Transaction_Complete = "false"
            });
            db.InsertAll(new Client()
            {
                ID = Guid.NewGuid(),
                Name = "Sabrina Jenkins",
                Date = DateTime.Now.ToShortDateString(),
                CheckIn = "4:35 PM",
                CheckIn_Minutes = "865.68515631",
                CheckOut = "",
                CheckOut_Minutes = "",
                Total_Time = "",
                Total_Cost = 0.00,
                Transaction_Complete = "false"
            });


        }

        public void testingBGColors()
        {
            this.lbl_DateTime.Background = new SolidColorBrush(Colors.Cyan);
            
        }
        #endregion

        private void btn_CheckIn_Click(object sender, RoutedEventArgs e)
        {

            if(list_NamesList.SelectedItem == null)
            {

                Popup myPopup = new Popup("No Item Selected!!");

                myPopup.Left = this.Left + 160;
                myPopup.Top = this.Top + 300;
                myPopup.ShowDialog();
            }
            else
            {
                var name = txt_EnterNamebox.Text.ToString();
                var date = DateTime.Now.ToShortDateString();
                var checkIn = DateTime.Now.ToShortTimeString();
                var mins = DateTime.Now.TimeOfDay.TotalMinutes.ToString();
                db.InsertAll(new Client() { ID = Guid.NewGuid(), Name = name, Date = date, CheckIn = checkIn, CheckIn_Minutes = mins, CheckOut = "", CheckOut_Minutes = "",
                                            Total_Time = "", Total_Cost = 0.00, Transaction_Complete = "false"});
                readFromDB();
            }

            
        }

        private void insertHelper()
        {
            sqlite_cmd = new SQLiteCommand("INSERT INTO MainMenuTable (id, name, Date, CheckIn, CheckIn_Minutes, CheckOut, CheckOut_Minutes," +
                "TotalTime, TotalCost, Complete) VALUES (@id,@name,@date,@checkin,@checkinminutes,@checkout,@checkoutminutes,@totaltime,@totalcost,@complete)", sqlite_conn);

            sqlite_cmd.Parameters.Add(new SQLiteParameter("@id", client.ID.ToString()));
            sqlite_cmd.Parameters.Add(new SQLiteParameter("@name", client.Name));
            sqlite_cmd.Parameters.Add(new SQLiteParameter("@date", client.Date));
            sqlite_cmd.Parameters.Add(new SQLiteParameter("@checkin", client.CheckIn));
            sqlite_cmd.Parameters.Add(new SQLiteParameter("@checkinminutes", client.CheckIn_Minutes));
            sqlite_cmd.Parameters.Add(new SQLiteParameter("@checkout", client.CheckOut));
            sqlite_cmd.Parameters.Add(new SQLiteParameter("@checkoutminutes", client.CheckOut_Minutes));
            sqlite_cmd.Parameters.Add(new SQLiteParameter("@totaltime", client.Total_Time));
            sqlite_cmd.Parameters.Add(new SQLiteParameter("@totalcost", client.Total_Cost));
            sqlite_cmd.Parameters.Add(new SQLiteParameter("@complete", client.Transaction_Complete));

        }

        private void readFromDB()
        {
            items.Clear();
            string qry = "SELECT * FROM MainMenuTable";
            readDataToArray(qry);
        }

        private void btn_Enter_Click(object sender, RoutedEventArgs e)
        {
            //read entered name from DB
            items.Clear();
            list_NamesList.ItemsSource = null;
            string qry = "SELECT * FROM MainMenuTable WHERE name LIKE '%" + txt_EnterNamebox.Text.ToString() + "%' and Complete = 'false'";
            readDataToArray(qry);
        }

        private void btn_CheckOut_Click(object sender, RoutedEventArgs e)
        {
            Client selected = (Client)list_NamesList.SelectedItem;
            var name = selected.Name;
            db.Update(name);

            string msg = "Checked out " + name + " at " + DateTime.Now.ToShortTimeString();
            Popup myPopup = new Popup(msg);
            myPopup.Left = this.Left + 160;
            myPopup.Top = this.Top + 300;
            myPopup.ShowDialog();

        }

        private void readDataToArray(string qry)
        {
            using (SQLiteConnection con = new SQLiteConnection("Data Source=MainDatabase.db" + ";Version=3;New=True;Compress=True;"))
            {
                con.Open();


                using (SQLiteCommand cmd = new SQLiteCommand(qry, con))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(new Client() { Name = reader.GetString(1), CheckIn = reader.GetString(3), CheckOut = reader.GetString(5), Total_Cost = reader.GetDouble(8) });
                        }
                        reader.Close();
                    }
                }
                con.Close();
            }
            list_NamesList.ItemsSource = items;
        }
    }

    public class Client
    {
        public Guid ID { get; set; }

        public String Name { get; set; }

        public String Date { get; set; }

        public String CheckIn { get; set; }

        public String CheckIn_Minutes { get; set; }

        public String CheckOut { get; set; }

        public String CheckOut_Minutes { get; set; }

        public String Total_Time { get; set; }

        public double Total_Cost { get; set; }

        public String Transaction_Complete { get; set; }
    }
}
