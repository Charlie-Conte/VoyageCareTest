#nullable disable
using System.Data;
using DapperExtensions;
using Microsoft.Data.SqlClient;
using StaffPortal.DataAccess.Core.Models;

namespace StaffPortal.Services
{
    public class StaffService
    {
        private static readonly string ConnectionString = "Server=localhost;Database=VoyageCare;Trusted_Connection=True;MultipleActiveResultSets=true";
        
        public List<UserDetail> GetAllStaff()
        {
            using IDbConnection db = new SqlConnection(ConnectionString);

            db.Open();

            var result = db.GetList<UserDetail>().ToList();

            db.Close();

            return result;
        }
        
        public List<Qualification> GetAllQualifications()
        {
            using IDbConnection db = new SqlConnection(ConnectionString);

            db.Open();

            var result = db.GetList<Qualification>().ToList();

            db.Close();

            return result;
        }
    }
}
