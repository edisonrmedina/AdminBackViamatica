namespace AdminBack.Application.DTO
{
    public class UpdateUsuarioDto
    {
        public int IdUsuario { get; set; }
        public string Correo { get; set; }         
        public string Usuario { get; set; }        
        public string Password { get; set; }       
        public bool Activo { get; set; }
        public int IdRol { get; set; }
    }
}
