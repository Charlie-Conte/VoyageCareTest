using System.Data;
using DapperExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using StaffPortal.DataAccess.Auth;
using StaffPortal.DataAccess.Core.Models;

namespace VoyageCareCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoreLocationController : ControllerBase
    {

        private readonly ILogger<CoreLocationController> _logger;

        public CoreLocationController(ILogger<CoreLocationController> logger)
        {
            _logger = logger;
        }

        public List<CareHome> GetAllCareHomes()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Open();
                //List<AspNetUser> users = db.GetList<AspNetUser>().ToList();

                //db.GetList<UserDetail>().ToList();
                var result = db.GetList<CareHome>().ToList();

                db.Close();

                return result;
                //db.GetList<Qualification>().ToList();

                //var linkList = db.GetList<CareHome_Staff>().ToList();


                //return users;
            }
        }
    }
}