using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Newtonsoft.Json;
using Restaurant_DataContract;
using DAL;
namespace BAL
{
    public class BALFood
    {
        DAL.DAL.DatabaseHelper databaseHelper = null;
        List<FoodItems> listAddressBook = null;
        public string GetFoodDetails()
        {
            string response = "";

            try
            {



                using (DAL.DAL.DatabaseHelper dbHelper = new DAL.DAL.DatabaseHelper())
                {
                    using (DataSet ds = dbHelper.ExecuteDataSet("SELECT * FROM Food_Items", CommandType.Text))
                    {
                        response = JsonConvert.SerializeObject(ds, Newtonsoft.Json.Formatting.Indented);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


            return response;
        }
    }
}
