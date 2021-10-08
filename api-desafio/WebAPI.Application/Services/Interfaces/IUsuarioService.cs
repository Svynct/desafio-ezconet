using System.Collections.Generic;
using WebAPI.Domain.Models;

namespace WebAPI.Application.Services.Interfaces
{
    public interface IUsuarioService
    {
        public List<UsuarioViewModel> RecuperarTodosUsuarios();
        public List<UsuarioViewModel> RecuperarUsuariosPorFiltro(FiltroUsuarioModel filtro);
        public UsuarioViewModel RecuperarUsuarioPorId(int id);
        public int CadastrarUsuario(Usuario usuario);
        public int RemoverUsuario(int id);
        public int EditarUsuario(Usuario usuario);
    }
}
