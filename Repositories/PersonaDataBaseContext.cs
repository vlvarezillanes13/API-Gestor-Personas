using ApiGestionPersonas.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ApiGestionPersonas.Repositories
{
    public class PersonaDataBaseContext : DbContext

    {
        public PersonaDataBaseContext(DbContextOptions<PersonaDataBaseContext> options) : base(options)
        {
        }

        public DbSet<PersonaEntity> persona { get; set; }

        public async Task<PersonaEntity?> Get(long id)
        {
            return await persona.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PersonaEntity?> Add(CreatePersonaDto personDto)
        {
            PersonaEntity entity = new PersonaEntity()
            {
                Id = null,
                Rut = personDto.Rut,
                Dv = personDto.Dv,
                Nombres = personDto.Nombres,
                ApellidoPaterno = personDto.ApellidoPaterno,
                ApellidoMaterno = personDto.ApellidoMaterno,
                Edad = personDto.Edad,
                Correo = personDto.Correo,
            };
            EntityEntry<PersonaEntity> response = await persona.AddAsync(entity);
            await SaveChangesAsync();
            return await Get(response.Entity.Id ?? throw new Exception("No se ha podido guardar"));
        }

        public async Task<PersonaEntity> Delete(long id)
        {
            PersonaEntity? entity = await Get(id);
            if (entity == null)
                throw new Exception("No se ha podido eliminar");
            persona.Remove(entity);
            await SaveChangesAsync();
            return entity;
        }

        public async Task<PersonaEntity> Update(PersonaEntity persn)
        {
            persona.Update(persn);
            await SaveChangesAsync();
            return persn;
        }
    }

    public class PersonaEntity
    {
        public int? Id { get; set; }
        public string Rut { get; set; }
        public string Dv { get; set; }
        public string? Nombres { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public int? Edad { get; set; }
        public string? Correo { get; set; }

        public PersonaDto ToDto()
        {
            return new PersonaDto()
            {
                Id = Id ?? throw new Exception("el id no puede ser null"),
                Rut = Rut ?? throw new Exception("rut no puede ser null"),
                Dv = Dv ?? throw new Exception("dv no puede ser null"),
                Nombres = Nombres,
                ApellidoPaterno = ApellidoPaterno,
                ApellidoMaterno = ApellidoMaterno,
                Edad = (int)Edad,
                Correo = Correo
            };
        }

    }
}
