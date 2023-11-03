using System.ComponentModel.DataAnnotations;

namespace Projeto.AspNet._04.MVC.Entity.Identity.DB.Models
{
    // esta classe assume o papel de model domain da aplicação
    // significa que, aqui, serão estabelecidas as regras de manipulação dos dados que circularão pela aplicação
    public class User
    {
        // definir 3 props para este model
        // o proposito deste model é auxiliar na criação de um schema que possa refletir colunas de uma table do DB
        [Required(ErrorMessage = "Por favor, informe seu nome")]
        public string? Name { get; set; }

        [Required]
        [RegularExpression("[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Por favor, insira uma senha bacana!")]
        public string? Password { get; set; }
    }
}
