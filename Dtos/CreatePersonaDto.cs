using System.ComponentModel.DataAnnotations;

namespace ApiGestionPersonas.Dtos
{
    public class CreatePersonaDto
    {
        [Required(ErrorMessage = "El Rut es requerido")]
        public string Rut { get; set; }
        [Required(ErrorMessage = "El Dv es requerido")]
        public string Dv { get; set; }
        public string? Nombres { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public int? Edad  { get; set; }
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "el correo no es correcto")]
        public string? Correo { get; set; }

    }
}
