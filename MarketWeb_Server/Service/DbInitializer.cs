using MarketWeb_Server.Service.IService;
using Microsoft.AspNetCore.Identity;
using MarketWeb_DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using MarketWeb_Common;

namespace MarketWeb_Server.Service
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;

        public DbInitializer(UserManager<IdentityUser> userManger, RoleManager<IdentityRole> roleManger, ApplicationDbContext db)
        {
            _userManager = userManger;
            _roleManager = roleManger;
            _db = db;
        }

        public void Initialize()
        {
            try
            {
                //Migration automatique s'il en existe
                if(_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }

                //Creation d'un role qui n'existe pas encore dans la BDD
                if (!_roleManager.RoleExistsAsync(StaticDetails.Role_Admin).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(StaticDetails.Role_Admin)).GetAwaiter().GetResult();
                    _roleManager.CreateAsync(new IdentityRole(StaticDetails.Role_Customer)).GetAwaiter().GetResult();
                }
                else
                {
                    return; //go back
                }
                //----------------------------------------------------------

                //Création de l'utilisateur "SuperAdmin" par defaut de l'application
                IdentityUser user = new()
                {
                    UserName = "superAdmin@MarketWeb.com", 
                    Email = "superAdmin@MarketWeb.com",
                    EmailConfirmed = true
                };
                
                _userManager.CreateAsync(user, "Admin123*").GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(user, StaticDetails.Role_Admin).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
