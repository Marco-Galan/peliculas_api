using Microsoft.EntityFrameworkCore;

namespace peliculas_api.Utilidades
{
    public static class HttpContextExtensions
    {
        public async static Task ParametrosPaginacionCabecera<T>(this HttpContext httpContext, IQueryable<T> queryable
            )
        {
            if (httpContext is null)
            {
                throw new ArgumentNullException(nameof(httpContext));

            }

            double cantidad = await queryable.CountAsync();
            httpContext.Response.Headers.Append("totalRegistros", cantidad.ToString());
        }
    }
}
