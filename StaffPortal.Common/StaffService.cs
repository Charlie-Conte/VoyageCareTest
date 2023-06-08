#nullable disable
using System.Data;
using Dapper;
using DapperExtensions;
using Microsoft.Data.SqlClient;
using StaffPortal.DataAccess.Core.Models;

namespace StaffPortal.Services
{
    public class StaffService : IDisposable
    {
        private IDbConnection db;
        public StaffService()
        {
            db = new SqlConnection(ConnectionString);

            db.Open();
        }

        public void Dispose()
        {
            db.Close();
        }

        private static readonly string ConnectionString = "Server=localhost;Database=VoyageCare;Trusted_Connection=True;MultipleActiveResultSets=true";
        
        public List<UserDetail> GetAllStaff()
        {
            var result = db.GetList<UserDetail>().ToList();

            return result;
        }

        public UserDetail GetStaffById(string id)
        {
            var result = db.Get<UserDetail>(id);

            return result;
        }        
        
        public void CreateStaff(UserDetail user)
        {
            db.Insert(user);
        }        
        
        public bool UpdateStaff(UserDetail user)
        {
            var result = db.Update(user);
            
            return result;
        }
        
        public List<Qualification> GetAllQualifications()
        {
            var result = db.GetList<Qualification>().ToList();
            
            return result;
        }

        public Qualification GetQualificationById(Guid qualificationId)
        {
            var result = db.Get<Qualification>(qualificationId);

            return result;
        }

        public void CreateQualification(Qualification qualification)
        {
            db.Insert(qualification);
        }

        public bool UpdateQualification(Qualification qualification)
        {
            var result = db.Update(qualification);
            
            return result;
        }

        public void DeleteQualification(Qualification qualification)
        {
            db.Delete(qualification);
        }
    }
}
