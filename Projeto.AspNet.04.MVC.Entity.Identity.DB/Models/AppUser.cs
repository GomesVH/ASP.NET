using Microsoft.AspNetCore.Identity;

namespace Projeto.AspNet._04.MVC.Entity.Identity.DB.Models
{
    // praticar o mecanismo de herança com a classe IdentityUser
    // estrutura da table do database
    public class AppUser : IdentityUser // superclasse/pai/base
    {
        // algumas das props que a superclasse IdentityUser disponibiliza
        // Id
        // Username
        // Email
        // Fone
        // Hash de senha
    }
}
