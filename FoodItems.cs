using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_DataContract
{
    public class FoodItems
    {
        public int id { get; set; }

        public int Item_Quantity { get; set; }

        public string Item_Name { get; set; }

        public string Item_Customization { get; set; }

        public string Item_Price { get; set; }
    }
}
