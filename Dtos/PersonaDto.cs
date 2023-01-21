namespace ApiGestionPersonas.Dtos
{
    public class PersonaDto
    {
        public int Id { get; set; }
        public string Rut { get; set; }
        public string Dv { get; set; }
        public string? Nombres { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public int? Edad { get; set; }
        public string? Correo { get; set; }

    }
}
