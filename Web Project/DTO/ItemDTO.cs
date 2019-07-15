using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Project.DTO
{
    public class ItemDTO
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public long Price { get; set; }
        public int Stock { get; set; }
    }
}