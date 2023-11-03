using Microsoft.AspNetCore.Mvc;
using Projeto.AspNet._03.MVC.CRUD.Models;
using Projeto.AspNet._03.MVC.CRUD.Novo.Models;

namespace Projeto.AspNet._03.MVC.CRUD.Controllers
{
    // este é o "coração" da aplicação. Aqui, serão executadas as "operações" com os dados obtidos da view e armazenados no repositorio. Esta classe assume o papel de controller porque pratica o mecanismo de herança com a classe Controller
    public class ColabController : Controller
    {
        // 1º passo: recuperar os dados do repository e lista-lo na view. Para este proposito será usada a action Index(): por que? R: Porque ao redenrizar a aplicação, a primeira tela que será exibida é a lista
        public IActionResult Index()
        {
            // é necessário indicar à action como acessar a lista de registros - o acesso a um elemento static dispensa o uso de uma instacia de classe
            return View(Repository.TodosOsColabs);
        }

        // 2º passo: definir a action para renderizar a view inserção/criação de registros dos dados da aplicação 
        public IActionResult Create()
        {
            return View();
        }
        // 2ºA passo: definir a action - de mesmo nome que a anterior - para "lidar" com o Repository - para a inserção de dados na lista - de forma direta; porque - quem é responsavel pela inserção de dados é o método Inserir() definido dentro do Repository.
        // para que estes intrumentos lógicos (instruções) funcionem adequadamente será necessário praticar o method overloading(sobrecarga de método)
        [HttpPost] // necessario o uso do atributo/verbo [HttpPost] para definir que o método/action abaixo pode inserir dados no repositorio
        public IActionResult Create(Colab registroColab)
        {
            // verificar o state de cada um dos inputs que serão utilizados para a obtenção de dados da view
            if (ModelState.IsValid)
            {
                // acessar diretamente a classe static Repository e o método static Inserir()
                Repository.Inserir(registroColab);
                return View("Agradecimento", registroColab);
            }
            else
            {
                // caso ocorra algum problema - a view de inserção continua em exibição
                return View();
            }
        }

        // 3º passo: criar a action referente a modalidade de atualização/alteração de registros
        // a 1ª action vai retornar a view para que seja "deixa-la" a disposição da aplicação; aqui, vale indicar que a action que recupera dados - em muito casos - faz uso do atributo [HttpGet] para recuperar os dados

        // alem de retornar a view, esta action é responsavel por executar uma consulta que recupera um registro - devidamente identificado -que será como argumento da action
        public IActionResult Update(string Identificador)
        {
            // estabelecer a consulta à base - para identificar e acessar o regsitro referente ao valor dado ao parametro Identificador
            Colab consulta = Repository.TodosOsColabs.Where((e) => e.Nome == Identificador).First();

            return View(consulta);
        }

        // 3º A passo: agora, será necessario construir action para reenviar o registro para a base
        [HttpPost] // requisição/atributo de envio de dados

        // esta actio precisará ter como duas props: uma, identifica o registro e a outra recebe como valor o registro com os dados
        public IActionResult Update(string Identificador, Colab registroAlteradoCol)
        {
            // verificar o state de cada um dos inputs que serão utilizados para a obtenção de dados da view
            if (ModelState.IsValid)
            {
                // abaixo foi estabelecida a instrução que altera o valor inicial da prop Idade. A clausula Where identifica o registro pelo elemento identificador - a prop Nome. E, na sequencia, compara o valor da prop Nome com o valor do argumento dados ao parametro Identificador. Se a avaliação for TRUE a prop Idade - do mesmo registro -recebe seu novo valor.
                Repository.TodosOsColabs.Where((e) => e.Nome == Identificador).First().Idade = registroAlteradoCol.Idade;

                Repository.TodosOsColabs.Where((e) => e.Nome == Identificador).First().Salario = registroAlteradoCol.Salario;

                Repository.TodosOsColabs.Where((e) => e.Nome == Identificador).First().Departamento = registroAlteradoCol.Departamento;

                Repository.TodosOsColabs.Where((e) => e.Nome == Identificador).First().Genero = registroAlteradoCol.Genero;

                Repository.TodosOsColabs.Where((e) => e.Nome == Identificador).First().Nome = registroAlteradoCol.Nome;
                // uma vez que esta tarefa esta finalizada - tarefa de alteração dos dados de um registro - será possivel o usuario ser redirecionado para outra view
                return RedirectToAction("Index");
            }
            return View();
        }

        // 4º passo: definir a action de exclusão de um registro - o registro, para ser excluido, precisar ser devidamente identificado
        [HttpPost]
        public IActionResult Delete(string Identificador)
        {
            // estabelecer a consulta à base de dados - para identificar o registro para a exclusão e, posteriormente, acessar o método static que o exclui
            Colab consulta = Repository.TodosOsColabs.Where((e) => e.Nome == Identificador).First();
            //aqui, o método de exclusão será chamado
            Repository.Excluir(consulta);

            // redirecionamento para action Index()
            return RedirectToAction("Index");
        }

    }
}
