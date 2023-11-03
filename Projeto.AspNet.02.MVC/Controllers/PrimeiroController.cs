using Microsoft.AspNetCore.Mvc;
using Projeto.AspNet._02.MVC.Models;

namespace Projeto.AspNet._02.MVC.Controllers
{
    // praticando o mecanismo de herança com a superclasse/pai Controller
    // é neste momento que a classe criada por nós assume o papel de Controller
    // da aplicação
    public class PrimeiroController : Controller
    {
        // o que esta classe/Controller vai controlar?
        // R: Vai controlar a chamada de um método que tem o objetivo de exibir uma
        // mensagem no browser. Aqui, numa aplicação Asp.Net - função = método ( por estar dentro
        // de um controller
        // numa aplicação Asp.Net )
       /* public string Index()
        {
            // expressão de retorno do método
            return "Ola Mundo Asp.Net! É nois !!!!!!!";
        
       public IActionResult Ola() // este método é diferente de void - que ele espera uma expressão de retorno; o uso da interface determina que está fazendo uso de particularidades do          AspNet.Core
        {
            // vamos vincular na View, uma mensagem devidamente "empacotada" a partir da camada lógica deste componente
            ViewBag.Message = "Agora, eu to sacando como a coisa funciona!";
            return View();
        }}*/

        // definir uma nova action para que seja possivel, estabelecer, a partir do controller, comunicação, tambem com o model
        public IActionResult Infos()
        {
            // para estabelecer a dita comunicação é necessario praticar a instancia da classe Perfil 
            Perfil infos = new Perfil();

            // fazer uso do objeto Infos para acessar so atributos da classe Perfil(){}
            infos.Nome = "Zé Pequeno";
            infos.Idade = 22;
            infos.Endereco = "Cidade de Deus";

            // definir a expressão de retorno - é necessario indicar como argumento do método View o objeto que conte todos valores dos atribuidos da classe model Perfil
            return View(infos);
        }
    }
}
