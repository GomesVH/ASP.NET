namespace Projeto.AspNet._02.MVC.Models
{
    // esta é uma classe comum c#. Aqui, serão estabelecidos alguns atributos que, devidamente acessados, poderão receber valores ao longo da implementação da aplicação
    public class Perfil
    {
       /* private string _nome; // esta prop só pode ser acessada de dentro desta classe
        // a instrução abaixo é o encapsulamento da prop private

        // criar uma instrução que dê a possibilidade de manipular a prop private - É a "pelicula" de proteção - tecnicamente chamada de encapsulamento

         // encapsulamento implicito
           public int Id { get; set; }
       
        public string Nome // o elemento publico - "pelicula" recebe o valor que será atribuido a prop
        {
            // usar os métodos acessores
            get { return _nome; }
            set { _nome = value; }
        }*/

        // com o uso do operador optional que o encapsulamento Nome é passivel de nulidade
        // - ENCAPSULAMENTO IMPLICITO
        public string? Nome { get; set; }
        public int Idade { get; set; }
        public string? Endereco { get; set;}

    }
}
