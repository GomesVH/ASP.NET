using Projeto.AspNet._03.MVC.CRUD.Novo.Models;

namespace Projeto.AspNet._03.MVC.CRUD.Models
{
    public static class Repository
    {
        /*1º passo: - definir todos os elementos da classe-repositorio como static
           - qual seria o tipo de coleção de dados adequada para o armazenamento temporario da classe Repository? R: Tipo List<T>
         */
        private static List<Colab> _todosOsColabs = new List<Colab>();
        // acima, foi criado um objeto do tipo List - seguindo a especifcação do model Colaborador

        // 2º passo: o objeto criado é private - portanto é necessário encapsula-lo. Além de encapsula-lo, será necessário enumerar cada um dos registro que a lista - criada acima vai salvar. Então, o elemento publico a ser criado precisa ser definido como enumeravel.
        // definindo encapsulamento como IEnumerable<Colaborador> basta, agora, fazer referencia a este elemento publico para que todos os dados sejam recuperados e exibidos em tela - na view
        public static IEnumerable<Colab> TodosOsColabs
        {
            get { return _todosOsColabs; }
        }

        // 3º passo: definir um método para inserir dados na lista. Ao definir o método será necessário fazer de um outro método - oferecido pelo AspNet  - para que seja inserir os tais dados na lista. Este método necessita de um parametro para que os dados possam ser recebidos e armazenados
        public static void Inserir(Colab registroColab)
        {
            // adicionando o registro à lista. O método Add() adiciona cada um dos registros dentro da lista. Abaixo - o objeto _todosOsColabs (que nada mais é do que a propria lista) - está sendo associado ao método Add(); é dessa forma que cada registro será adicionado

            _todosOsColabs.Add(registroColab);
        }

        // 4º passo: definir um método que, efetivamente, é chamado para fazer a exclusão de um determinado registro
        public static void Excluir(Colab registroColab)
        {
            // acessar a lista de registros e - a partir do uso do método embarcado Remove() - excluir o registro
            _todosOsColabs.Remove(registroColab);
        }

        
    }
}
