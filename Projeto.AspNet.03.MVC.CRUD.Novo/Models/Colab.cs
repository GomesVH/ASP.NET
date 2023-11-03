using System.ComponentModel.DataAnnotations;

namespace Projeto.AspNet._03.MVC.CRUD.Novo.Models
{
    public class Colab
    {
        // definir os atributos que irão compor o model e, posteriormente, receberão os devidos valores. Ainda, mais tarde, serão referenciados na view
        [Required(ErrorMessage = "Insira, por favor, seu nome")]
        public string? Nome { get; set; } // encapsulamento implicito
        [Range(16, 99, ErrorMessage = "Informe, por favor, uma idade entre 16 e 99 anos")]
        public int Idade { get; set; }
        [RegularExpression(@"\d+(\.\d{1, 2})?", ErrorMessage = "Valor invalido. Uma sugestão $ ou $.$")] // 8.8888888888
                                                    // 888888888888
        public decimal Salario { get; set; }

        public string? Departamento { get; set; }

        [RegularExpression(@"^[MFO]+$", ErrorMessage = "Selecione ao menos 1 valor.")]
        public Char Genero { get; set; }
    }
}
