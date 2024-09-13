using AdminBack.Application.DTO;

namespace AdminBack.Application.Services
{
    public interface IUsuarioService
    {
        Task<UsuarioDto> GetUsuarioByIdAsync(int id);
        Task<List<UsuarioDto>> GetAllUsuariosAsync();
        Task AddUsuarioAsync(CreateUsuarioDto usuario);
        Task UpdateUsuarioAsync(UpdateUsuarioDto usuario);
        Task DeleteUsuarioAsync(int id);
        Task<string> LoginAsync(int userId);
        Task LogoutAsync(int userId);
    }
}
