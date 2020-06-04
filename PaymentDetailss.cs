using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;


namespace BAL
{
   public class PaymentDetailss
    {
        DAL.DAL.DatabaseHelper databaseHelper = null;
        public int BALPayments(int Year, string Cardnumber, int CVV, double TotalCost,string OrderNumber)
        {
            
            int Responce = 10;
            DateTime dateTime = new DateTime();

            databaseHelper = new DAL.DAL.DatabaseHelper();
            int i = databaseHelper.ExecuteNonQuery("insert into Payments(TotalCost,Year,CardNumber,CVV,OrderNumber,DateTime) values ('" + TotalCost+"','"+Year+"','"+Cardnumber+"','"+CVV+"','"+ OrderNumber + "','"+ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')", CommandType.Text);
            Responce = i;
            return Responce;
        }

    }
}
