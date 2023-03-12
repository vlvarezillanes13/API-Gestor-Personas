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
        public DbSet<PerfilEntity> perfiles { get; set; }


        public async Task<UsuarioEntityLogin?> Get(UsuarioAccesoDto usuarioAcceso)
        {
            UsuarioEntity? user = await usuario.FirstOrDefaultAsync(x => x.Username.Equals(usuarioAcceso.Username));
            PerfilEntity? perfil = await perfiles.FirstOrDefaultAsync(x => x.Id == user.Perfil);
            if (user == null | perfil == null || user?.Password != usuarioAcceso.Password)
                return null;
            UsuarioEntityLogin usuarioEntityLogin = new UsuarioEntityLogin();
            usuarioEntityLogin.Id = user.Id;
            usuarioEntityLogin.Username = user.Username;
            usuarioEntityLogin.Password = user.Password;
            usuarioEntityLogin.Perfil = user.Perfil;
            usuarioEntityLogin.NombrePerfil = perfil.Nombre;

            return usuarioEntityLogin;
        }

        public async Task<PerfilEntity?> Get(int idPerfil)
        {
            PerfilEntity? perfil = await perfiles.FirstOrDefaultAsync(x => x.Id == idPerfil);
            if (perfil == null)
                return null;
            return perfil;
        }


        public class UsuarioEntity
        {
            public int Id { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public int Perfil { get; set; }

        }

        public class UsuarioEntityLogin : UsuarioEntity 
        {
            public UsuarioEntityLogin()
            {
                
            }

            public string NombrePerfil { get; set; }

        }

        public class PerfilEntity
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public bool Activo { get; set; }

        }

        public class UsuarioEntityToken
        {
            public int Id { get; set; }
            public string Username { get; set; }
            public int Perfil { get; set; }

        }

    }
}
