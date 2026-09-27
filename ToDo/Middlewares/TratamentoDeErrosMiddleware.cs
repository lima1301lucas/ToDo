using System.Net;
using System.Text.Json;
using ToDo.Exceptions;

namespace ToDo.Middlewares
{
    public class TratamentoDeErrosMiddleware
    {
        private readonly RequestDelegate _next;

        public TratamentoDeErrosMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (RecursoNaoEncontradoException ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                var resposta = JsonSerializer.Serialize(new { erro = ex.Message });
                await context.Response.WriteAsync(resposta);
            }
            catch (ConflitoException ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                var resposta = JsonSerializer.Serialize(new { erro = ex.Message });
                await context.Response.WriteAsync(resposta);
            }
            catch (NaoAutorizadoException ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                var resposta = JsonSerializer.Serialize(new { erro = ex.Message });
                await context.Response.WriteAsync(resposta);
            }
            catch (Exception)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var resposta = JsonSerializer.Serialize(new { erro = "Ocorreu um erro interno no servidor." });
                await context.Response.WriteAsync(resposta);
            }
        }
    }
}