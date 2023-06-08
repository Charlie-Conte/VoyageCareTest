#nullable disable
using System.Data;
using DapperExtensions;
using Microsoft.Data.SqlClient;
using StaffPortal.DataAccess.Core.Models;

namespace StaffPortal.Services
{
    public class LocationService
    {
        private static readonly string ConnectionString = "Server=localhost;Database=VoyageCare;Trusted_Connection=True;MultipleActiveResultSets=true";
        
        public List<CareHome> GetAllCareHomes()
        {
            using IDbConnection db = new SqlConnection(ConnectionString);
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
        
        public List<CareHome_Staff> GetAllCareHome_Staffs()
        {
            using IDbConnection db = new SqlConnection(ConnectionString);

            db.Open();

            var result = db.GetList<CareHome_Staff>().ToList();

            db.Close();

            return result;
        }
    }
}
