using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Restaurant_DataContract;
using DAL;
using System.Data;
using System.Drawing;
using System.IO;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;
using BAL;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

namespace Restaurant.Controllers
{
    public class RestaurantController : Controller
    {
        List<FoodItems> listadd = null;
        BALFood objAdd = null;
        BALPassword objPassword = null;
        PaymentDetailss Payments = null;
        //BALImportFiles balImport = null;
        // GET: Restaurant
        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }
        //[HttpPost]
        public ActionResult Login(string Username, string Password)
        {
            string responce = null;

            //string CUname = Username;
            //string CPwd = Password;
            //if (responce != null)
            //{
                objPassword = new BALPassword();
                responce = objPassword.Credentials(Username, Password);
                //if (Username == "sai" && Password == "sai")
                //{
                //    return Json("Success");
                //}
                if (responce == "Success")
                {
                    return Json("Success");
                }
                else{
                    return View();
                }
            //}
            //return View();
        }
        public ActionResult HomeIndex()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetData()
        {
            try
            {
                objAdd = new BALFood();
                string Foodobj = objAdd.GetFoodDetails();
                return Json(Foodobj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public ActionResult PaymentDetails(int Year, string Cardnumber, int CVV, float TotalCost,string OrderNumber)
        {

            Payments = new PaymentDetailss();
            int c = Payments.BALPayments(Year, Cardnumber, CVV, TotalCost, OrderNumber);
            return Json(c);
        }


        public ActionResult Register()
        {
            
            return View();
        }
        public int SaveRegister(string Username, string Password, string Email)
        {
            int responce = 10;

            objPassword = new BALPassword();
            responce = objPassword.SaveCredentials(Username, Password,Email);
            
            if (responce == 1)
            {
                 return 1;
            }
            return 0;
        }


    //    protected void Upload(object sender, EventArgs e)
    //    {
    //        //Upload and save the file
            
    //        string excelPath = Server.MapPath("~/Files/") + Path.GetFileName(FileUpload1.PostedFile.FileName);
    //        FileUpload1.SaveAs(excelPath);

    //        string conString = string.Empty;
    //        string extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
    //        switch (extension)
    //        {
    //            case ".xls": //Excel 97-03
    //                conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
    //                break;
    //            case ".xlsx": //Excel 07 or higher
    //                conString = ConfigurationManager.ConnectionStrings["Excel07+ConString"].ConnectionString;
    //                break;

    //        }
    //        conString = string.Format(conString, excelPath);
    //        using (OleDbConnection excel_con = new OleDbConnection(conString))
    //        {
    //            excel_con.Open();
    //            string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
    //            DataTable dtExcelData = new DataTable();

    //            //[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.
    //            dtExcelData.Columns.AddRange(new DataColumn[3] { new DataColumn("Id", typeof(int)),
    //            new DataColumn("Name", typeof(string)),
    //            new DataColumn("Salary", typeof(decimal)) });

    //            using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "]", excel_con))
    //            {
    //                oda.Fill(dtExcelData);
    //            }
    //            excel_con.Close();

    //            string consString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    //            using (SqlConnection con = new SqlConnection(consString))
    //            {
    //                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
    //                {
    //                    //Set the database table name
    //                    sqlBulkCopy.DestinationTableName = "dbo.tblPersons";

    //                    //[OPTIONAL]: Map the Excel columns with that of the database table
    //                    sqlBulkCopy.ColumnMappings.Add("Id", "PersonId");
    //                    sqlBulkCopy.ColumnMappings.Add("Name", "Name");
    //                    sqlBulkCopy.ColumnMappings.Add("Salary", "Salary");
    //                    con.Open();
    //                    sqlBulkCopy.WriteToServer(dtExcelData);
    //                    con.Close();
    //                }
    //            }
    //        }
    //    }
        


    }



   
}