using AdminBack.Application.DTO;
using AdminBack.Infraestructure.Data.Repositories;
using AdminBack.Models;
using AutoMapper;

namespace AdminBack.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<UsuarioDto> GetUsuarioByIdAsync(int id)
        {
            var usuario = await _usuarioRepository.GetUsuarioByIdAsync(id);
            return _mapper.Map<UsuarioDto>(usuario); // Mapea la entidad a un DTO
        }

        public async Task<List<UsuarioDto>> GetAllUsuariosAsync()
        {
            var usuarios = await _usuarioRepository.GetAllUsuariosAsync();
            return _mapper.Map<List<UsuarioDto>>(usuarios);
        }

        public async Task AddUsuarioAsync(CreateUsuarioDto usuarioDto)
        {
            var usuario = _mapper.Map<Usuario>(usuarioDto); // Convierte el DTO en entidad
            await _usuarioRepository.AddUsuarioAsync(usuario);
        }

        public async Task UpdateUsuarioAsync(UpdateUsuarioDto usuarioDto)
        {
            var usuario = _mapper.Map<Usuario>(usuarioDto); // Convierte el DTO en entidad
            await _usuarioRepository.UpdateUsuarioAsync(usuario);
        }

        public async Task DeleteUsuarioAsync(int id)
        {
            await _usuarioRepository.DeleteUsuarioAsync(id);
        }


        public Task<string> LoginAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task LogoutAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
