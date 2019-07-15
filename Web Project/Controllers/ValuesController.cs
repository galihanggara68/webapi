using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web_Project.DTO;
using Web_Project.Models;

namespace Web_Project.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<ItemDTO> Get()
        {
            using(APIEntities api = new APIEntities())
            {
                return api.Items.Select(i => 
                    new ItemDTO {
                        ItemId = i.item_id,
                        Name = i.name,
                        Price = i.price,
                        Stock = i.stock
                    }
                ).ToList();
            }
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
