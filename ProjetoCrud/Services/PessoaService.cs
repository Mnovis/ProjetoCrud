using Microsoft.IdentityModel.Tokens.Experimental;
using ProjetoCrud.Entities;
using ProjetoCrud.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

            Console.WriteLine("\nCADASTRAR PESSOA:\n");

            Console.Write("Informe o nome:");
            pessoa.Nome = Console.ReadLine() ?? string.Empty;

            Console.Write("Informe o email:");
            pessoa.Email = Console.ReadLine() ?? string.Empty;

            Console.Write("Informe o cpf:");
            pessoa.Cpf = Console.ReadLine() ?? string.Empty;

            if (ValidarPessoa(pessoa))
            {
                var pessoaRepository = new PessoaRepository();
                pessoaRepository.Inserir(pessoa);

                Console.WriteLine("\nPESSOA CADASTRADA COM SUCESSO!");
            }
        }

        private void AtualizarPessoa()
        {
            Console.WriteLine("\nATUALIZAR PESSOA:\n");

            Console.Write("Informe o ID da pessoa que deseja atualizar: ");
            var id = int.Parse(Console.ReadLine() ?? string.Empty);

            var pessoaRepository = new PessoaRepository();
            var pessoa = pessoaRepository.ObterPorId(id);

            if(pessoa != null)
            {
                Console.WriteLine("\nDados da Pessoa: ");
                Console.WriteLine("\tNome: " + pessoa.Nome);
                Console.WriteLine("\tEmail: " + pessoa.Email);
                Console.WriteLine("\tCPF: " + pessoa.Cpf);

                Console.WriteLine("\nInforme os novos dados: ");

                Console.Write("Informe o nome: ");
                pessoa.Nome = Console.ReadLine() ?? string.Empty;

                Console.Write("Informe o email: ");
                pessoa.Email = Console.ReadLine() ?? string.Empty;

                Console.Write("Informe o cpf: ");
                pessoa.Cpf = Console.ReadLine() ?? string.Empty;

                if (ValidarPessoa(pessoa)) 
                {
                    pessoaRepository.Atualizar(pessoa);

                    Console.WriteLine("\nPESSOA ATUALIZADA COM SUCESSO!");
                }
            }
            else
            {
                Console.WriteLine("\nPESSOA NÃO ENCONTRADA.");
            }
        }

        private void ExcluirPessoa()
        {
            Console.WriteLine("\nEXCLUIR PESSOA:\n");

            Console.Write("Informe o ID da pessoa que deseja excluir: ");
            var id = int.Parse(Console.ReadLine() ?? string.Empty);

            var pessoaRepository = new PessoaRepository();
            var pessoa = pessoaRepository.ObterPorId(id);

            if(pessoa != null)
            {
                Console.WriteLine("\nDados da Pessoa:");
                Console.WriteLine("\tNome: " + pessoa.Nome);
                Console.WriteLine("\tEmail: " + pessoa.Email);
                Console.WriteLine("\tCPF: " + pessoa.Cpf);

                Console.Write("\nDESEJA REALMENTE EXCLUIR? (S/N): ");
                var opcao = Console.ReadLine() ?? string.Empty;

                if(opcao.Equals("S", StringComparison.OrdinalIgnoreCase))
                {
                    pessoaRepository.Excluir(id);

                    Console.WriteLine("\nPESSOA EXCLUÍDA COM SUCESSO!");
                }
            }
            else
            {
                Console.WriteLine("\nPESSOA NÃO ENCONTRADA.");
            }
        }

        private void ConsultarPessoa()
        {
            Console.WriteLine("\nCONSULTAR PESSOAS:\n");

            var pessoaRepository = new PessoaRepository();
            var pessoas = pessoaRepository.ObterTodos();

            foreach(var pessoa in pessoas)
            {
                Console.WriteLine($"ID: {pessoa.Id}, Nome: {pessoa.Nome}, Email: {pessoa.Email}, CPF: {pessoa.Cpf}");
            }
        }

        private bool ValidarPessoa(Pessoa pessoa)
        {
            #region Executar as regras de validaçãp (Data Annotations)

            var validation = new ValidationContext(pessoa);
            var errors = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(
                pessoa,
                validation,
                errors,
                validateAllProperties: true
            );

           
            if( ! isValid)
            {
                foreach (var item in errors)
                {
                    Console.WriteLine("\tERRO: " + item.ErrorMessage);
                }
            }

            var pessoaRepository = new PessoaRepository();
            if (pessoaRepository.VerificarCpf(pessoa.Cpf, pessoa.Id))
            {
                Console.WriteLine("\nESTE CPF JÁ ESTÁ CADASTRADO PARA OUTRA PESSOA");
                Console.WriteLine("\tCPF: " + pessoa.Cpf);
                Console.WriteLine("NÃO É POSSÍVEL CONCLUIR ESSA OPERAÇÃO");

                isValid = false;
            }

            return isValid;

            #endregion
        }
    }
}
