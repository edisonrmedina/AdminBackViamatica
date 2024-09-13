namespace AdminBack.Application.DTO
{
    public class CreateUsuarioDto
    {
        public string Correo { get; set; }         
        public string Usuario { get; set; }        
        public string Password { get; set; }       
        public bool Activo { get; set; }
        public int IdRol { get; set; }
    }
}
