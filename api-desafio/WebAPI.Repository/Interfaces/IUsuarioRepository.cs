using System.Collections.Generic;
using WebAPI.Domain.Models;

namespace WebAPI.Repository.Interfaces
{
    public interface IUsuarioRepository
    {
        public List<UsuarioViewModel> GetAllUsuarios();
        public List<UsuarioViewModel> GetUsuarioByFiltro(FiltroUsuarioModel filtro);
        public UsuarioViewModel GetUsuarioById(int id);
        public int CadastrarUsuario(Usuario usuario);
        int RemoverUsuario(int id);
        int AtualizarUsuario(Usuario usuario);
    }
}
