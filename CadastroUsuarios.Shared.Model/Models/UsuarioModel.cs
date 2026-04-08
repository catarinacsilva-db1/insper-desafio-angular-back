using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CadastroUsuarios.Models
{
    [Table("Usuarios")]
    public class UsuarioModel
    {

        public int Id { get; set; }
        public bool Ativo { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [StringLength(100)]
        public string Sobrenome { get; set; }

        [StringLength(100)]
        public string NomeSocial { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime DataNascimento { get; set; }

        [Required]
        public string Cpf { get; set; }

        [Required]
        [StringLength(250)]
        public string Senha { get; set; }
    }
}