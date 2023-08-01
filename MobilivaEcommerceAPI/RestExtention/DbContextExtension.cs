using Microsoft.EntityFrameworkCore;
using Mobiliva.Ecommerce.Data.Context;


namespace MobilivaEcommerceAPI.RestExtention
{
    public static class DbContextExtension
    {
        public static void AddDbContextExtension(this IServiceCollection services, IConfiguration Configuration)
        {
            var dbType = Configuration.GetConnectionString("DbType");
            if (dbType == "SQL")
            {
                var dbConfig = Configuration.GetConnectionString("MsSqlConnection");
                services.AddDbContext<EcommerceEfDbContext>(opts =>
                opts.UseSqlServer(dbConfig));
            }
            else if (dbType == "PostgreSql")
            {
                var dbConfig = Configuration.GetConnectionString("PostgreSqlConnection");
                services.AddDbContext<EcommerceEfDbContext>(opts =>
                  opts.UseNpgsql(dbConfig));
            }
            else if(dbType == "MySql")
            {
                var dbConfig = Configuration.GetConnectionString("MySqlConnection");
                services.AddDbContext<EcommerceEfDbContext>(opts =>
                  opts.UseMySQL(dbConfig));
            }
        }
    }
}
