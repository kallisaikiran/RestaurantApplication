using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data;
using System.Data.Common;
namespace BAL
{
  public class BALPassword
    {
        DAL.DAL.DatabaseHelper databaseHelper = null;
        
        public string Credentials(string Username,string Password) {
            string dbpwd=null;
            string dbusername=null;
            string Responce="";
            databaseHelper = new DAL.DAL.DatabaseHelper();
            DbDataReader drReader = databaseHelper.ExecuteReader("SELECT * FROM Registration WHERE Username='" + Username + "'", CommandType.Text);

            if (drReader.HasRows)
            {
                if (drReader.Read())
                {
                    dbpwd = drReader.IsDBNull(drReader.GetOrdinal("Password")) ? string.Empty : drReader.GetString(drReader.GetOrdinal("Password"));
                    dbusername = drReader.IsDBNull(drReader.GetOrdinal("Username")) ? string.Empty : drReader.GetString(drReader.GetOrdinal("Username"));
                    
                }
            }
            if (Username != null && Password !=null) {
                if (Username == dbusername && Password == dbpwd) {
                    return "Success";
                }
            }
            return "Fail";
        }
        
        public int SaveCredentials(string Username, string Password,string Email)
        {

            int Responce = 10;
            databaseHelper = new DAL.DAL.DatabaseHelper();
            int i = databaseHelper.ExecuteNonQuery("insert into Registration(Username,Password,Email) values ('" + Username + "','" + Password + "','" + Email + "')", CommandType.Text);
            Responce = i;
            return Responce;
        }


        public string ICredentials(string Username, string Password)
        {
            string dbpwd = null;
            string dbusername = null;
            string Responce = "";
            databaseHelper = new DAL.DAL.DatabaseHelper();
            DbDataReader drReader = databaseHelper.ExecuteReader("SELECT * FROM IRegistration WHERE Username='" + Username + "'", CommandType.Text);

            if (drReader.HasRows)
            {
                if (drReader.Read())
                {
                    dbpwd = drReader.IsDBNull(drReader.GetOrdinal("Password")) ? string.Empty : drReader.GetString(drReader.GetOrdinal("Password"));
                    dbusername = drReader.IsDBNull(drReader.GetOrdinal("Username")) ? string.Empty : drReader.GetString(drReader.GetOrdinal("Username"));

                }
            }
            if (Username != null && Password != null)
            {
                if (Username == dbusername && Password == dbpwd)
                {
                    return "Success";
                }
            }
            return "Fail";
        }
    }
}
