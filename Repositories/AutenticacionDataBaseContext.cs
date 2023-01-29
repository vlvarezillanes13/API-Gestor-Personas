using ApiGestionPersonas.Dtos;
using Microsoft.EntityFrameworkCore;

namespace ApiGestionPersonas.Repositories
{
    public class AutenticacionDataBaseContext : DbContext
    {
        public AutenticacionDataBaseContext(DbContextOptions<AutenticacionDataBaseContext> options) : base(options)
        {
        }

        public DbSet<UsuarioEntity> usuario { get; set; }


        public async Task<UsuarioEntity?> Get(UsuarioAccesoDto usuarioAcceso)
        {
            UsuarioEntity? user = await usuario.FirstOrDefaultAsync(x => x.Username.Equals(usuarioAcceso.Username));
            if (user == null | user?.Password != usuarioAcceso.Password)
                return null;
            return user;
        }
    }


    public class UsuarioEntity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Perfil { get; set; }

    }

    public class UsuarioEntityToken
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Perfil { get; set; }

    }

}
