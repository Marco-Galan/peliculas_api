using peliculas_api.DTOs;
using System.Runtime.CompilerServices;

namespace peliculas_api.Utilidades
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Paginar<T>(this IQueryable<T> queryable, PaginacionDTO paginacion)
        {
             return queryable
            .Skip((paginacion.Pagina - 1) * paginacion.resultadosPorPagina)
            .Take(paginacion.resultadosPorPagina);
        }
    }
}
