using GridHub.Database.Models;
using GridHub.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GridHub.API.Controllers
{
    /// <summary>
    /// Controlador para gerenciar as operações CRUD dos usuários.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IRepository<Usuario> _usuarioRepository;

        public UsuarioController(IRepository<Usuario> usuarioRepository)
        {
            _usuarioRepository = usuarioRepository ?? throw new ArgumentNullException(nameof(usuarioRepository));
        }

        /// <summary>
        /// Obtém um usuário específico pelo ID.
        /// </summary>
        /// <param name="id">ID do usuário.</param>
        /// <returns>Usuário solicitado.</returns>
        /// <response code="200">Retorna o usuário solicitado.</response>
        /// <response code="404">Usuário não encontrado.</response>
        [HttpGet("{id}")]
        public ActionResult<Usuario> Get(int id)
        {
            var usuario = _usuarioRepository.GetById(id);

            if (usuario == null)
            {
                return NotFound(); // Retorna 404 se o usuário não for encontrado
            }

            return Ok(usuario); // Retorna 200 e o usuário
        }

        /// <summary>
        /// Adiciona um novo usuário ao sistema.
        /// </summary>
        /// <param name="usuario">Objeto contendo os dados do usuário a ser criado.</param>
        /// <returns>
        /// Retorna um objeto de resposta contendo o status da operação.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Lançada se o parâmetro 'usuario' for nulo.
        /// </exception>
        /// <response code="201">Retorna o usuário criado.</response>
        /// <response code="400">Dados inválidos.</response>
        [HttpPost]
        public ActionResult<Usuario> Post([FromBody] Usuario usuario)
        {
            if (usuario == null)
            {
                return BadRequest("Dados inválidos.");
            }

            // Chamar a lógica de hash para senha antes de adicionar
            usuario.DefinirSenha(usuario.Senha);

            _usuarioRepository.Add(usuario);

            return CreatedAtAction(nameof(Get), new { id = usuario.UsuarioId }, usuario);
        }

        /// <summary>
        /// Atualiza um usuário existente.
        /// </summary>
        /// <param name="id">ID do usuário a ser atualizado.</param>
        /// <param name="usuario">Objeto usuário com os novos dados.</param>
        /// <returns>Usuário atualizado.</returns>
        /// <response code="200">Retorna o usuário atualizado.</response>
        /// <response code="400">Dados inválidos ou ID não corresponde ao usuário.</response>
        /// <response code="404">Usuário não encontrado.</response>
        [HttpPut("{id}")]
        public ActionResult<Usuario> Put(int id, [FromBody] Usuario usuario)
        {
            if (usuario == null || id != usuario.UsuarioId)
            {
                return BadRequest("Dados inválidos.");
            }

            var usuarioExistente = _usuarioRepository.GetById(id);
            if (usuarioExistente == null)
            {
                return NotFound(); // Retorna 404 se o usuário não for encontrado
            }

            // Atualizar o usuário
            usuarioExistente.Nome = usuario.Nome;
            usuarioExistente.Email = usuario.Email;
            usuarioExistente.Telefone = usuario.Telefone;
            usuarioExistente.FotoPerfil = usuario.FotoPerfil;

            if (!string.IsNullOrEmpty(usuario.Senha))
            {
                usuarioExistente.DefinirSenha(usuario.Senha); // Atualiza a senha, se fornecida
            }

            _usuarioRepository.Update(usuarioExistente);

            return Ok(usuarioExistente);
        }

        /// <summary>
        /// Exclui um usuário específico.
        /// </summary>
        /// <param name="id">ID do usuário a ser excluído.</param>
        /// <returns>Resposta de sucesso ou erro.</returns>
        /// <response code="204">Usuário excluído com sucesso.</response>
        /// <response code="404">Usuário não encontrado.</response>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var usuario = _usuarioRepository.GetById(id);
            if (usuario == null)
            {
                return NotFound(); // Retorna 404 se o usuário não for encontrado
            }

            _usuarioRepository.Delete(usuario);

            return NoContent(); // Retorna 204 (sem conteúdo) após a exclusão
        }
    }
}
