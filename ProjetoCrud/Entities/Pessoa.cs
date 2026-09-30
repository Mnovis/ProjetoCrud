using ProjetoCrud.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProjetoCrud.Entities
{
    /// <summary>
    /// Modelo de entidade de domínio para Pessoa.
    /// </summary>
    public class Pessoa
    {
        public int Id { get; set; }

        [MinLength(6, ErrorMessage = "O nome deve ter no mínimo {1} caracteres.")]
        [MaxLength(150, ErrorMessage = "O nome deve ter no máximo {1} caracteres.")]
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Nome { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "O email informado não é válido.")]
        [Required(ErrorMessage = "O email é obrigatório.")]
        public string Email { get; set; } = string.Empty;

        [CpfValidation]
        [RegularExpression("^[0-9]{11}$", ErrorMessage = "O CPF deve ter exatamente 11 números (sem pontos e traços).")]
        [Required(ErrorMessage = "O CPF é obrigatório.")]
        public string Cpf { get; set; } = string.Empty;
        

        public DateTime DataHoraCadastro { get; set; } = DateTime.Now;
    }
}
