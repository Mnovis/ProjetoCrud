using Dapper;
using Microsoft.Data.SqlClient;
using ProjetoCrud.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoCrud.Repositories
{
    /// <summary>
    /// Repositório para inserir, alterar excluir e consultar os dados da entidade Pessoa no banco de dados.
    /// </summary>
    public class PessoaRepository
    {
        #region Atributos Privados

        private readonly string _connectionString = "";

        #endregion

        #region Métodos

        public void Inserir(Pessoa pessoa)
        {
            // Abrindo conexão com o banco de dados
            using (var connection = new SqlConnection(_connectionString))
            {
                // Executando uma instrução SQl para inserir pessoa na tabela do banco
                connection.Execute("""
                        INSERT INTO PESSOAS(NOME, CPF, EMAIL)
                        VALUES(@Nome, @Cpf, @Email)
                    """, pessoa);
            }
        }

        #endregion

    }
}
