using ProjetoCrud.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoCrud.Services
{
    /// <summary>
    /// Classe de serviço para realizar os fluxos do sistema relacionados à entidade Pessoa.
    /// </summary>
    public class PessoaService
    {
        public void ExecutarMenuPrincipal()
        {
            Console.WriteLine("\nSISTEMA PARA GERENCIAMENTO DE PESSOAS:\n");
            Console.WriteLine("(1) Cadastrar Pessoa");
            Console.WriteLine("(2) Atualizar Pessoa");
            Console.WriteLine("(3) Excluir Pessoa");
            Console.WriteLine("(4) Consultar Pessoa");
            Console.WriteLine("(5) Sair");

            Console.Write("Informe a opção desejada...:");
            var opcao = int.Parse(Console.ReadLine() ?? string.Empty);

            switch (opcao)
            {
                case 1: CadastrarPessoa(); break;
                case 2: AtualizarPessoa(); break;
                case 3: ExcluirPessoa(); break;
                case 4: ConsultarPessoa(); break;
                case 5:
                    Console.WriteLine("\nFim do programa!");
                    break;
                default:
                    Console.WriteLine("\nOpção inválida!");
                    break;
            }

            Console.WriteLine("\nPressione uma tecla para continuar...");
            Console.ReadKey();

            if (opcao != 5)
            {
                Console.Clear(); // Limpar o console
                ExecutarMenuPrincipal(); // Chamada recursiva para exibir o menu novamente
            }
        }

        private void CadastrarPessoa()
        {
            var pessoa = new Pessoa();

            Console.WriteLine("\nCADASTRO DE PESSOA:\n");

            Console.WriteLine("Informe o nome:");
            pessoa.Nome = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Informe o email:");
            pessoa.Email = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Informe o cpf:");
            pessoa.Cpf = Console.ReadLine() ?? string.Empty;   
        }

        private void AtualizarPessoa()
        {

        }

        private void ExcluirPessoa()
        {

        }

        private void ConsultarPessoa()
        {

        }
    }
}
