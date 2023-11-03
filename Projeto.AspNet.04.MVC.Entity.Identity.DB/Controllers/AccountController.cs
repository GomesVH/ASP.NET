using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Projeto.AspNet._04.MVC.Entity.Identity.DB.Models;

namespace Projeto.AspNet._04.MVC.Entity.Identity.DB.Controllers
{
    // este controller será responsavel pelas modalidades de autenticação/autorização/ de usuarios para a área restrita da aplicação

    [Authorize]// este atributo faz com que todas as estruturas de instruções relacionadas à esta classe se tornem inacessiveis. Significa que, qualquer estrutura desta classe não seja acessada - de forma externa - em hipotese alguma
    public class AccountController : Controller
    {
        /*
=================================================================    1ª PARTE - CONFIGURAÇÃO DO CONTROLLER DA ESTRUTURA DE LOGIN  
=================================================================  
*/

        // 1º passo: definir dois "auxiliadores" - objetos referenciais - para DI (Dependency Injection). 
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager; // este 2º objeto referencial nada mais do que um "gerenciador de recursos de acesso" à areas restrita de uma aplicação

        // 2º passo: estabelecer o construtor da classe - de forma customizada. Objetivos desta implementação:
        // - criar o elemento publico para lidar com as props private;
        // - estabelecer a injeção de dependencias os recursos associados aos objetos referenciais;
        // - "priorizar" a inicialização dos recursos  dos objetos referenciais

        public AccountController(
            UserManager<AppUser> userMgr,
            SignInManager<AppUser> sigMgr
            )
        {
            userManager = userMgr;
            signInManager = sigMgr;
        }


    }
}
