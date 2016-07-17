using CSC_Assignment2.Models;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Web;
using System.Web.Mvc;

namespace CSC_Assignment2
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ApplicationDbContext>());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
