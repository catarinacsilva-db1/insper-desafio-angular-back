
using CadastroUsuarios.DTO;
using CadastroUsuarios.Models;


namespace CadastroUsuarios.Controllers.Utils
{
    public static class UsuarioMapper
    {
        public static UsuarioDTO ToDto(UsuarioModel model)
        {
            return new UsuarioDTO
            {
                Id = model.Id,
                Ativo = model.Ativo,
                Nome = model.Nome,
                Sobrenome = model.Sobrenome,
                NomeSocial = model.NomeSocial,
                DataNascimento = model.DataNascimento,
                Cpf = model.Cpf,
                Senha = model.Senha
            };
        }

        public static UsuarioModel ToModel(UsuarioDTO dto)
        {
            return new UsuarioModel
            {
                Id = dto.Id,
                Ativo = dto.Ativo,
                Nome = dto.Nome,
                Sobrenome = dto.Sobrenome,
                NomeSocial = dto.NomeSocial,
                DataNascimento = dto.DataNascimento,
                Cpf = dto.Cpf,
                Senha = dto.Senha
            };
        }
    }
}
