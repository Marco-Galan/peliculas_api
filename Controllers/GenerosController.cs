using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using peliculas_api.DTOs;
using peliculas_api.entidades;

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

        // Constante para el tag de cache
        private const string cacheTag = "generos"; 

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
        public List<GeneroDTO> Get()
        {
            return new List<GeneroDTO>() { 
                new GeneroDTO() { 
                    id = 1, Nombre = "Comedia" 
                } ,
                new GeneroDTO() {
                    id = 2, Nombre = "Acción"
                },
                new GeneroDTO() {
                    id = 3, Nombre = "Terror"
                }
            };
        }

        [HttpGet("{id:int}", Name = "ObtenerGeneroPorId")] // api/generos/500
        [OutputCache (Tags = [cacheTag])] // Limpia cache
        public async Task<ActionResult<Genero>> Get(int id)
        {
            throw new NotImplementedException();
        }
        
        //Fucnion ActionResult<Genero> : Clase base que define el resultado que una acción en un controlador puede devolver
        [HttpPost]
        public async Task<ActionResult<Genero>> Post([FromBody] GeneroCreacionDTO generoCreacionDTO)

        {
            //Se guardan los datos asincronamente
            var genero = mapper.Map<Genero>(generoCreacionDTO);
            context.Add(genero);
            await context.SaveChangesAsync();
            return CreatedAtRoute("ObtenerGeneroPorId", new { id = genero.id }, genero);
        }

        [HttpPut]
        public void Put()
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id:int}")]
        public void Delete()
        {
            throw new NotImplementedException();
        }
    }
}
