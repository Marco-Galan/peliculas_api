using peliculas_api.entidades;

namespace peliculas_api
{
    public interface IRepositorio
    {
        List<Genero> ObtenerGeneros();
        List<Genero> ObtenerTodosLosDatos();
        Task<Genero?> ObtenerPorId(int id);
        
        bool Existe(string nombre);
    }
}
