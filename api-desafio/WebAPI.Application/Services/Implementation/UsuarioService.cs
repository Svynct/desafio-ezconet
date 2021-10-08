using System.Collections.Generic;
using WebAPI.Application.Services.Interfaces;
using WebAPI.Domain.Models;
using WebAPI.Repository.Interfaces;

namespace WebAPI.Application.Services.Implementation
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository usuarioRepository;
        public UsuarioService(IUsuarioRepository _usuarioRepository)
        {
            usuarioRepository = _usuarioRepository;
        }

        public List<UsuarioViewModel> RecuperarTodosUsuarios()
        {
            var usuarios = usuarioRepository.GetAllUsuarios();

            return usuarios;
        }

        public UsuarioViewModel RecuperarUsuarioPorId(int id)
        {
            var usuario = usuarioRepository.GetUsuarioById(id);

            return usuario;
        }

        public List<UsuarioViewModel> RecuperarUsuariosPorFiltro(FiltroUsuarioModel filtro)
        {
            var usuarios = usuarioRepository.GetUsuarioByFiltro(filtro);

            return usuarios;
        }

        public int CadastrarUsuario(Usuario usuario)
        {
            var rowsAffected = usuarioRepository.CadastrarUsuario(usuario);

            return rowsAffected;
        }

        public int RemoverUsuario(int id)
        {
            var rowsAffected = usuarioRepository.RemoverUsuario(id);

            return rowsAffected;
        }
        public int EditarUsuario(Usuario usuario)
        {
            var rowsAffected = usuarioRepository.AtualizarUsuario(usuario);

            return rowsAffected;
        }
    }
}
