using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GridHub.Database.Models
{
    public class Usuario
    {
        public Usuario(string email, string senha)
        {
            Email = email;
            DefinirSenha(senha);
        }

        public int UsuarioId { get; set; }

        [DefaultValue("exemplo@email.com")]
        public string Email { get; set; }

        [DefaultValue("")]
        public string Senha { get; private set; }

        [DefaultValue("João")]
        public string Nome { get; set; }

        [DefaultValue("000000000")]
        public string Telefone { get; set; }

        [DefaultValue("foto_padrao.png")]
        public string FotoPerfil { get; set; }

        [DefaultValue(typeof(DateTime), "2024-01-01")]
        public DateTime DataCriacao { get; set; }

        public void DefinirSenha(string senha)
        {
            Senha = BCrypt.Net.BCrypt.HashPassword(senha, 13);
        }

        public bool VerificarSenha(string senha)
        {
            return BCrypt.Net.BCrypt.Verify(senha, Senha);
        }
    }
}
