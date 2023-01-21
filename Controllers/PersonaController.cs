using ApiGestionPersonas.CasosDeUso;
using ApiGestionPersonas.Dtos;
using ApiGestionPersonas.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ApiGestionPersonas.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : Controller
    {
        private readonly PersonaDataBaseContext _personaDataBaseContext;
        private readonly IUpdatePersonaUserCase _UpdatePersonaUserCase;

        public PersonaController(PersonaDataBaseContext personaDataBaseContext, IUpdatePersonaUserCase UpdatePersonaUserCase)
        {
            _personaDataBaseContext = personaDataBaseContext;
            _UpdatePersonaUserCase = UpdatePersonaUserCase;
        }

        [HttpGet("ObtenerPersonas")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PersonaDto>))]
        public async Task<IActionResult> GetPersonas()
        {
            List<PersonaEntity> result = await _personaDataBaseContext.GetAll();
            return new OkObjectResult(result);
        }

        [HttpGet("ObtenerPersona/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PersonaDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPersona(int id)
        {
            PersonaEntity? result = await _personaDataBaseContext.Get(id);
            if(result == null)
                return new NotFoundResult();
            return new OkObjectResult(result.ToDto());
        }

        [HttpPost("AgregarPersona")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PersonaDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(PersonaDto))]
        public async Task<IActionResult> PostPersonas(CreatePersonaDto persona)
        {
            PersonaEntity? result = await _personaDataBaseContext.Add(persona);
            if (result == null)
                throw new Exception("Problemas al agregar persona");
            return new OkObjectResult(result.ToDto());
        }

        [HttpDelete("EliminarPersona/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PersonaDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePersona(int id)
        {
            PersonaEntity? result = await _personaDataBaseContext.Delete(id);
            if (result == null)
                return new NotFoundResult();
            return new OkObjectResult(result.ToDto());
        }

        [HttpPut("ActualizarPersona")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PersonaDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePersona(PersonaDto persona)
        {
            PersonaDto? result = await _UpdatePersonaUserCase.Execute(persona);

            if (result == null)
                return new NotFoundResult();

            return new OkObjectResult(result);
        }
    }
}
