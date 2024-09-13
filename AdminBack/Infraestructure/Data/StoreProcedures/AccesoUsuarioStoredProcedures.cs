using System.Data;
using AdminBack.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AdminBack.Infraestructure.Data.StoreProcedures
{
    public class AccesoUsuarioStoredProcedures
    {
        private readonly ADMINContext _context;

        public AccesoUsuarioStoredProcedures(ADMINContext context)
        {
            _context = context;
        }

        public async Task<bool> LoginUserAsync(string username, string password)
        {
            var resultParam = new SqlParameter
            {
                ParameterName = "@Result",
                SqlDbType = SqlDbType.Bit,
                Direction = ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC LoginUser @Usuario, @Password, @Result OUTPUT",
                new SqlParameter("@Usuario", username),
                new SqlParameter("@Password", password),
                resultParam
            );

            return (bool)resultParam.Value;
        }
    }
}
