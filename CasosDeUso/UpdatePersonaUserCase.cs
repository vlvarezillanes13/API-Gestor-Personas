using ApiGestionPersonas.Repositories;

namespace ApiGestionPersonas.CasosDeUso
{
    public interface IUpdatePersonaUserCase
    {
        Task<Dtos.PersonaDto?> Execute(Dtos.PersonaDto persona);
    }
    public class UpdatePersonaUserCase: IUpdatePersonaUserCase
    {
        private readonly PersonaDataBaseContext _personaDataBaseContext;

        public UpdatePersonaUserCase(PersonaDataBaseContext personaDataBaseContext)
        {
            _personaDataBaseContext = personaDataBaseContext;
        }

        public async Task<Dtos.PersonaDto?> Execute(Dtos.PersonaDto persona)
        {
            PersonaEntity? entity = await _personaDataBaseContext.Get(persona.Id);

            if (entity == null)
                return null;

            entity.Rut = persona.Rut;
            entity.Dv = persona.Dv;
            entity.Nombres = persona.Nombres;
            entity.ApellidoPaterno = persona.ApellidoPaterno;
            entity.ApellidoMaterno = persona.ApellidoMaterno;
            entity.Edad = persona.Edad;
            entity.Correo = persona.Correo;

            PersonaEntity personNew = await _personaDataBaseContext.Update(entity);
            return personNew.ToDto();

        }
    }
}
