using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Projeto.AspNet._04.MVC.Entity.Identity.DB.Models;


namespace Projeto.AspNet._04.MVC.Entity.Identity.DB.Controllers
{
    // este controller será responsavel pelas operações CRUD do "cadastro" de dados do usuario
    public class AdminController : Controller
    {
        /*
         ============================================================
         1ª PARTE - DEFINIÇÃO DOS ELEMENTOS REFERENCIAIS E INJENÇÃO DE DEPENDENCIA (DI)
         ============================================================
         */
        // 1º passo: definir uma prop - private - para criar um elemento referencial. Neste momento, é importante criar um elemento para que seja usado no auxilio da manipulação de dados da base - com as quais o controller vai lidar. Para a definição deste elemento referencial será usada a classe embarcada UserManager - recurso oriundo do Asp.Net (é uma classe que oferecve recursos para gerenciar armazenamento de dados na base)
        private UserManager<AppUser> userManager;

        // 2º passo: definir uma nova prop para auxiliar na recuperação/leitura da senha/password em Hash. Para definir esta prop será usado o recurso de interace IPasswordHasher 
        private IPasswordHasher<AppUser> passwordHasher;
        
        // 3º passo: será a definição da injeção de dependencia fazendo uso das prop referenciais definidas nos passos anteriores. Dessa forma, será definido um elemento pulblico para que - se necessário for -  a DI possa ser referenciada em outros "pedaços" do projeto.
        // é necessario - para a pratica da DI - a definição do construtor da classe
        public AdminController(
            UserManager<AppUser> usrMgr,
            IPasswordHasher<AppUser> passWH
            ) {
                //aqui, a prop private userManager será acessada e receberá como valor o parametro usrMgr do construtor
                this.userManager = usrMgr;
                this.passwordHasher = passWH;
              }
/*
====================================================================
   2ª PARTE -  CRIAÇÃO DAS ACTIONS - definição das operações CRUD da aplicação
    C - Create (criar), R - Read (ler), U - Update(alterar/atualizar), D - Delete (Excluir)
    
        fazendo uso dos models:
    
        AppUser: representação da table - aqui na aplicação - do DB. Neste contexto será responsavel por receber do model User os dados necessarios para o armazenamento e, posteriormente, os processo de autenticação/autorização de acesso á area restrita

        User: responsavel por direcionar a forma com a qual os dados de cadastro do usuario serem manipulados
====================================================================
*/

        // 1ª OP Crud - Read - será responsavel pela recuperação/acesso e exibição de todos os dados armazenados/persitidos na base
        public IActionResult Index()
        {
            // O que está action vai retornar nesta View()?
            return View(userManager.Users);
            // R: o elemento lógico Users(método get) foi, acima, referenciado por ser um método get - que, por definição - recupera dados da base. Este método é de uso exclusivo da classe UserManager
        }

        // 2ª OP Crud - Create: será responsavel pela inserção de dados na base
        // esta action precisa, também, retornar uma view
        /*public IActionResult Create()
        {
            return View();
        }*/

        // esta é uma nova forma que definir uma action que trará como resultado a mesma situação indicada acima
        public ViewResult Create() => View();

        //... continuanda 2ª OP: praticar a sobrecarga da action para que os dados possam ser enviados para o armazenamento
        // definir o uso do atributo [HttpPost]
        [HttpPost]
        // definir - de forma explicita - uma tarefa assincrona para o envio - com sucesso total - dos dados para a base
        public async Task<IActionResult> Create(User user)
        {
            // verificar se o ModelState é valido
            if (ModelState.IsValid)// se a avaliação for TRUE
            {
                // definir um objeto - será definido a partir do model AppUser - que representa a table do db aqui, na aplicação - para fins de autorização/autenticação/armazenamento
                AppUser appUser = new AppUser
                {
                    UserName = user.Name,
                    Email = user.Email,
                    // agora, o objeto appUser possui valores para as duas props/atributos que a compõem
                };

                // manipular a prop password para fins de completude de autenticação/armazenamento
                IdentityResult result = await userManager.CreateAsync(appUser, user.Password); // aqui esta o conjunto de dados com suas 3 props definidas no model User

                // é necessário, agora, aninhar um novo if - para que os recursos embarcados de sucesso possam ser usados para os devidos objetivos
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    // estabelecer um loop para investigar/iterar sobre os eventuais erros que podem ter ocorrido
                    foreach (IdentityError erro in result.Errors)
                    {
                        ModelState.AddModelError("", erro.Description);
                    }
                }
           
            }
            return View(user);

        }

      
        // 3ª OP Crud - Update: será responsavel pela REinserção de dados na base - desde que esteja devidamente armazenado e identificado na base

        // definição da action Update - explicitamente como uma tarefa assincrona atua para que o registro possa ser selecionado e, posteriormente, ser manipulado e reenviado à base
        public async Task<IActionResult> Update(string id)
        {
            // definir uma consulta - à base - para a obtenção de um registro para atualização
            AppUser user = await userManager.FindByIdAsync(id);

            // avaliar a consulta
            if (user != null)
            {
                return View(user);
            }
            else
            {
                return View("Index");
            }
        }

        // sobrecarga da action/método Update para que seja possivel REenviar os dados - alterados/atuaizados - para a base
        [HttpPost]
        public async Task<IActionResult> Update(string id, string email, string password)
        {
            // repetir a consulta a base - aqui, está sendo observado se o registro selecionaodo é o mesmo que está sendo alterado e será, ainda, reenviado
            AppUser user = await userManager.FindByIdAsync(id);

            // agora, é necessario lidar com as props e seus valores que serão reenviados para a base
            if (user != null) // a consulta trouxe resultado - o resultado é um conjunto de dados que, a partir deste momento precisa ser particionado e observado ponto-a-ponto
            {
                // observar o 1º ponto: o valor da propriedade email
                if (!string.IsNullOrEmpty(email))// aqui, a prop email é um parametro do método IsNullOrEmpty()
                    user.Email = email;
                else
                    ModelState.AddModelError("", "O campo email não pode ser vazio!");

                // observar o 2º ponto: o valor da propriedade password
                if (!string.IsNullOrEmpty(password))
                    user.PasswordHash = passwordHasher.HashPassword(user, password);
                else
                    ModelState.AddModelError("", "O campo senha/password não pode ser vazio.");

                // observar o 3º ponto: consite em verificar se os dados  -agora, em conjunto - permanecem diferentes de vazios ou nulos. Assim, de forma assincrona será possivel enviar os dados à base
                if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
                {
                    IdentityResult result = await userManager.UpdateAsync(user);
                    // verificar o sucesso desta transação de dados
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        // chamada do método Errors
                        Errors(result);
                    }

                }


            }
            else
                ModelState.AddModelError("", "Usuario não encontrado.");

            return View(user);
        }

        // definição do método void Errors()
       private void Errors(IdentityResult result)
        {
            // estabelecer um loop para iterar todos os erros - caso eles existam
            foreach (IdentityError erro in result.Errors)
            {
                ModelState.AddModelError("", erro.Description);
            }
        }


        // 4ª OP Crud - Delete: será responsavel pela exclusão de dados na base - desde que esteja devidamente armazenado e identificado na base
        [HttpPost]
        // de forma explicita será definida a tarefa assincrona de exclusão de registro
        public async Task<IActionResult> Delete(string id)
        {
            // definir a consulta à base de dados para a seleçã do registro que será excluido
            AppUser user = await userManager.FindByIdAsync(id);

            // avaliar a consulta para observar se a variavel user possui algum valor satisfatorio
            if (user != null)
            {
                // criar a instrução de exclusão do registro
                IdentityResult result = await userManager.DeleteAsync(user);

                // implementar as mensagens relativas ao sucesso ou falha da execução da tarefa assincrona definida para a exclusão do registro
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                    ;
                }
                else
                    Errors(result);
            }
            else
                ModelState.AddModelError("", "Usuario, infelizmente, não foi encontrado.");

            return View("Index", userManager.Users);
        }


    }

}
