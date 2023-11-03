using System.ComponentModel.DataAnnotations;

namespace Projeto.AspNet._04.MVC.Entity.Identity.DB.Models
{
    // esta classe assume o papel de ser um elemento lógico que opere como um "conjunto de props credenciais" - "como se fosse um cartão de acesso com informações de um usuario para area restrita
    public class Login
    {
        // é necessario definir as props - obrigatorias - que irão compor a estrutura credencial
        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

        public string? ReturnUrl { get; set; }

        // por padrão, o Asp.Net vai adotar uma URL para o acesso ao espaço de inserção de credenciais:
        // http://localhost:xxxxx/NomeQualquer/Login
        // ao utilizar a prop ReturnUrl, estamos dizendo que é possivel customizar a rota para uma área restrita da aplicação -ou seja, a aplicação pode "fugir" do padrão imposto pelo Asp.Net
    }
}
