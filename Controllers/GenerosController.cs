using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using peliculas_api.DTOs;
using peliculas_api.entidades;
using peliculas_api.Utilidades;

namespace peliculas_api.Controllers
{
    [Route("api/generos")]
    [ApiController]
    public class GenerosController : ControllerBase
    // :ControllerBase -> Acceso a controlres auxiliares
    {   //Acopalmiento fuerte / Configuracion de dependencia
        private readonly IOutputCacheStore outputCacheStore;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private const string cacheTag = "generos";  // Constante para el tag de cache

        public GenerosController(
            // Servicio de cache
            IOutputCacheStore outputCacheStore,
            ApplicationDbContext context,
            IMapper mapper
            )
        {
            this.outputCacheStore = outputCacheStore;
            this.context = context;
            this.mapper = mapper;
        }


        [HttpGet] //api/generos
        [OutputCache(Tags = [cacheTag])] // Limpia cache
        public async Task<List<GeneroDTO>> Get([FromQuery] PaginacionDTO paginacion)
        {
            var queryable = context.Generos;
            await HttpContext.ParametrosPaginacionCabecera(queryable);
            return await queryable
                .OrderBy(g => g.Nombre)
                .Paginar(paginacion)
                .ProjectTo<GeneroDTO>(mapper.ConfigurationProvider).ToListAsync();

        }

        [HttpGet("{id:int}", Name = "ObtenerGeneroPorId")] // api/generos/500
        [OutputCache(Tags = [cacheTag])] // Limpia cache

        public async Task<ActionResult<GeneroDTO>> Get(int id) // api/generos/500
        {
            var genero = await context.Generos
                .ProjectTo<GeneroDTO>(mapper.ConfigurationProvider)
                // Se busca el id en la base de datos
                .FirstOrDefaultAsync(g => g.Id == id);

            if (genero is null)
            {
                return NotFound();
            }
            return genero;
        }

        //Fucnion ActionResult<Genero> : Clase base que define el resultado que una acción en un controlador puede devolver
        [HttpPost]
        public async Task<ActionResult<Genero>> Post([FromBody] GeneroCreacionDTO generoCreacionDTO)

        {
            //Se guardan los datos asincronamente
            var genero = mapper.Map<Genero>(generoCreacionDTO);
            context.Add(genero);
            await context.SaveChangesAsync();
            await outputCacheStore.EvictByTagAsync(cacheTag, default); // Elimina el cache
            return CreatedAtRoute("ObtenerGeneroPorId", new { id = genero.Id }, genero);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult>Put(int id, [FromBody] GeneroCreacionDTO generoCreacionDTO)
        {
            var generoExistente = await context.Generos.AnyAsync(g => g.Id == id);
            if (!generoExistente)
            {
                return NotFound();
            }
            var genero = mapper.Map<Genero>(generoCreacionDTO);
            genero.Id = id;
            context.Update(genero);
            await context.SaveChangesAsync();
            await outputCacheStore.EvictByTagAsync(cacheTag, default);

            return NoContent();

        }

        [HttpDelete("{id:int}")]
        public void Delete()
        {
            throw new NotImplementedException();
        }
    }
}
