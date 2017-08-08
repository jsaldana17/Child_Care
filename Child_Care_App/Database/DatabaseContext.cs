using System;
using System.Collections.Generic;
using System.IO;
using System.Data.SQLite;

namespace Child_Care_App.Database
{
    public class DatabaseContext
    {
        
        private SQLiteConnection sqlite_conn;
        private SQLiteCommand sqlite_cmd;

        public DatabaseContext(String db, string cmdText)
        {
            openConnection(db, cmdText);
        }

        public void openConnection(string db, string cmdText)
        {
            // We use these three SQLite objects:

            if (!File.Exists(db))
            {
                // create a new database connection:
                sqlite_conn = new SQLiteConnection("Data Source=" + db + ";Version=3;New=True;Compress=True;");

                // open the connection:
                sqlite_conn.Open();

                // create a new SQL command:
                sqlite_cmd = sqlite_conn.CreateCommand();


                // Let the SQLiteCommand object know our SQL-Query:
                //sqlite_cmd.CommandText = "CREATE TABLE test (id integer primary key, extension varchar(100), filename varchar(100), path varchar(100), Date_Time varchar(100));";
                sqlite_cmd.CommandText = cmdText;
                // Now lets execute the SQL ;D
                sqlite_cmd.ExecuteNonQuery();

            }
            else
            {
                // create a new database connection:
                sqlite_conn = new SQLiteConnection("Data Source=" + db + ";Version=3;New=True;Compress=True;");

                // open the connection:
                sqlite_conn.Open();
            }


        }

        public void InsertAll(Client client)
        {
            sqlite_cmd = new SQLiteCommand("INSERT INTO MainMenuTable (id, name, Date, CheckIn, CheckIn_Minutes, CheckOut, CheckOut_Minutes," +
                "TotalTime, TotalCost, Complete) VALUES (@id,@name,@date,@checkin,@checkinminutes,@checkout,@checkoutminutes,@totaltime,@totalcost,@complete)", sqlite_conn);

            /*
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
            */
            try
            {
                sqlite_cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(string name)
        {
            sqlite_cmd = new SQLiteCommand("UPDATE MainMenuTable SET CheckOut=@checkout, CheckOut_Minutes=@checkoutminutes, Complete=@complete WHERE name=@name", sqlite_conn);
            sqlite_cmd.Parameters.Add(new SQLiteParameter("@name", name));
            sqlite_cmd.Parameters.Add(new SQLiteParameter("@checkout", DateTime.Now.ToShortTimeString()));
            sqlite_cmd.Parameters.Add(new SQLiteParameter("@checkoutminutes", DateTime.Now.TimeOfDay.TotalMinutes));
            sqlite_cmd.Parameters.Add(new SQLiteParameter("@complete", "true"));

            try
            {
                sqlite_cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void read()
        {
            sqlite_cmd.CommandText = "SELECT * FROM MainMenuTable";
            SQLiteDataAdapter sda = new SQLiteDataAdapter();
            sda.SelectCommand = sqlite_cmd;
            
        }

        public void ExecuteSQL(string SQL_Text)
        {
            sqlite_cmd.CommandText = SQL_Text;
            sqlite_cmd.ExecuteNonQuery();
            
        }

        public String Get(string sql)
        {
            sqlite_cmd.CommandText = sql;
            String s = sqlite_cmd.ExecuteNonQuery().ToString();
            return s;
        }

        

       
    }

   
}
