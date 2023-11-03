using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Projeto.AspNet._04.MVC.Entity.Identity.DB.Models
{
    // praticar o mecanismo de herança com a superclasse IdentityDbContext<>
    // o objetivo é: oferecer a subclasse todos os recursos necessários para que o contexto de integração com a  base de dados SqlServer possa existir plenamente
    public class AppEntityIdentityDbContext: IdentityDbContext<AppUser>
    {
        // definir construtor/método
        // porque é necessario "priorizar" a referencia daquilo que se usa como parametro do construtor
        public AppEntityIdentityDbContext(DbContextOptions<AppEntityIdentityDbContext> options) : base(options) { }
    }
}
