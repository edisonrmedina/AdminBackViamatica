namespace AdminBack.Application.DTO
{
    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int LogginAttemps {  get; set; }
    }
}
