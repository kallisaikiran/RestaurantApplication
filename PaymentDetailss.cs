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
            databaseHelper = new DAL.DAL.DatabaseHelper();
            int i = databaseHelper.ExecuteNonQuery("insert into PaymentDetails(TotalCost,Year,CardNumber,CVV,OrderNumber) values ('" + TotalCost+"','"+Year+"','"+Cardnumber+"','"+CVV+"','"+ OrderNumber + "')", CommandType.Text);
            Responce = i;
            return Responce;
        }

    }
}
