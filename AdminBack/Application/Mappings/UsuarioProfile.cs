using AdminBack.Application.DTO;
using AdminBack.Models;
using AutoMapper;

namespace AdminBack.Application.Mappings
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile() 
        {
            CreateMap<Usuario, UsuarioDto>();
            CreateMap<CreateUsuarioDto, Usuario>();
            CreateMap<UpdateUsuarioDto, Usuario>();
        }
        
    }
}
