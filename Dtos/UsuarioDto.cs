using ApiGestionPersonas.Repositories;

namespace ApiGestionPersonas.Dtos
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password   { get; set; }
        public int Perfil   { get; set; }

    }
}
