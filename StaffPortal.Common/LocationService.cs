#nullable disable
using System.Data;
using DapperExtensions;
using Microsoft.Data.SqlClient;
using StaffPortal.DataAccess.Core.Models;

namespace StaffPortal.Services
{
    public class LocationService : IDisposable
    {
        private IDbConnection db;
        public LocationService()
        {
            db = new SqlConnection(ConnectionString);

            db.Open();
        }

        public void Dispose()
        {
            db.Close();
        }

        private static readonly string ConnectionString = "Server=localhost;Database=VoyageCare;Trusted_Connection=True;MultipleActiveResultSets=true";

        public List<CareHome> GetAllCareHomes()
        {
            var result = db.GetList<CareHome>().ToList();
            return result;
        }
        public void CreateCareHome(CareHome careHome)
        {
            db.Insert(careHome);
        }

        public CareHome GetCareHomeById(Guid id)
        {
            var result = db.Get<CareHome>(id);

            return result;
        }          
        
        public bool DeleteCareHome(CareHome careHome)
        {
            var result = db.Delete<CareHome>(careHome);

            return result;
        }   

        public bool UpdateCareHome(CareHome careHome)
        {
            var result = db.Update(careHome);
            
            return result;
        }

        public List<CareHome_Staff> GetAllCareHome_Staffs()
        {
            var result = db.GetList<CareHome_Staff>().ToList();
            return result;
        }
    }
}
