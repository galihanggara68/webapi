using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web_API_Project.DTO;
using Web_API_Project.Models;
using Web_API_Project.Filters;
using System.Threading;

namespace Web_API_Project.Controllers
{
    [AuthenticationFilter]
    public class ValuesController : ApiController
    {
        [Route("getprincipal")]
        public string GetPrincipal() {
            return Thread.CurrentPrincipal.Identity.Name;
        }

        // GET api/values
        public IEnumerable<EmployeeDTO> Get()
        {
            using(HREntities hr = new HREntities())
            {
                
                return hr.COPY_EMP.Select(e =>
                    new EmployeeDTO
                    {
                        EmployeeId = (int)e.EMPLOYEE_ID,
                        FirstName = e.FIRST_NAME,
                        LastName = e.LAST_NAME
                    }
                ).ToList();
            }
        }

        // GET api/values/5
        public EmployeeDTO Get(int id)
        {
            using (HREntities hr = new HREntities())
            {
                COPY_EMP emp = hr.COPY_EMP.Find(id);
                return new EmployeeDTO {
                    EmployeeId = (int)emp.EMPLOYEE_ID,
                    FirstName = emp.FIRST_NAME,
                    LastName = emp.LAST_NAME
                };
            }
        }

        // POST api/values
        public IHttpActionResult Post([FromBody]EmployeeDTO dto)
        {
            if (!ActionContext.ModelState.IsValid) return BadRequest("Data Tidak Valid");
            using (HREntities hr = new HREntities())
            {
                try
                {
                    if (dto == null) return BadRequest();
                    COPY_EMP employee = new COPY_EMP {
                        EMPLOYEE_ID = dto.EmployeeId,
                        FIRST_NAME = dto.FirstName,
                        LAST_NAME = dto.LastName
                    };
                    hr.COPY_EMP.Add(employee);
                    hr.SaveChanges();
                }catch(Exception e)
                {
                    return InternalServerError();
                }
                return Json("{msg: 'Success', status: 200}");
            }
        }

        // PUT api/values/5
        public IHttpActionResult Put([FromUri]int id, [FromBody]EmployeeDTO dto)
        {
            using (HREntities hr = new HREntities())
            {
                try
                {
                    if (dto == null) return BadRequest();
                    COPY_EMP employee = new COPY_EMP
                    {
                        EMPLOYEE_ID = dto.EmployeeId,
                        FIRST_NAME = dto.FirstName,
                        LAST_NAME = dto.LastName
                    };
                    COPY_EMP emp = hr.COPY_EMP.Find(id);
                    emp.FIRST_NAME = employee.FIRST_NAME;
                    emp.LAST_NAME = employee.LAST_NAME;
                    hr.SaveChanges();
                }
                catch (Exception e)
                {
                    return InternalServerError();
                }
                return Json("{msg: 'Success', status: 200}");
            }
        }

        // DELETE api/values/5
        public IHttpActionResult Delete(int id)
        {
            using (HREntities hr = new HREntities())
            {
                try
                {
                    if (id == 0) return BadRequest();
                    COPY_EMP emp = hr.COPY_EMP.Find(id);
                    hr.COPY_EMP.Remove(emp);
                    hr.SaveChanges();
                }
                catch (Exception e)
                {
                    return InternalServerError();
                }
                return Json("{msg: 'Success', status: 200}");
            }
        }
    }
}
