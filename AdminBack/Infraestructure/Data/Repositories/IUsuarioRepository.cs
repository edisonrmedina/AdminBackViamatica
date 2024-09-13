using AdminBack.Models;

namespace AdminBack.Infraestructure.Data.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario> GetUsuarioByIdAsync(int id);
        Task<List<Usuario>> GetAllUsuariosAsync();
        Task AddUsuarioAsync(Usuario usuario);
        Task UpdateUsuarioAsync(Usuario usuario);
        Task DeleteUsuarioAsync(int id);
    }
}
