using System;
using System.ComponentModel.DataAnnotations;

namespace CadastroUsuarios.DTO
{
    public class UsuarioDTO
    {
        public int Id { get; set; }

        public bool Ativo { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Nome do usuário")]
        public string Nome { get; set; }

        [Required]
        [StringLength(100)]
        public string Sobrenome { get; set; }

        [StringLength(100)]
        [Display(Name = "Nome Social")]
        public string NomeSocial { get; set; }

        [Display(Name = "Data de nascimento")]
        public DateTime DataNascimento { get; set; }

        [Required]
        [Display(Name = "CPF")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "CPF deve conter 11 dígitos")]
        public string Cpf { get; set; }

        [Required]
        [StringLength(250)]
        public string Senha { get; set; }
    }
}