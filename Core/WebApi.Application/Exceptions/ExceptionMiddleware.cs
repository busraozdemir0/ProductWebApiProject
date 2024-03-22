using FluentValidation;
using Microsoft.AspNetCore.Http;
using SendGrid.Helpers.Errors.Model;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;


namespace WebApi.Application.Exceptions
{
    public class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
        {
            try
            {
                await next(httpContext);


            }catch(Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            int statusCode = GetStatusCode(exception); // Gelen exception'a gore StatusCode'u bulduk.
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = statusCode;

            if(exception.GetType() == typeof(ValidationException)) // exception'ın tipi ValidationException ise
                return httpContext.Response.WriteAsync(new ExceptionModel
                {
                    Errors = ((ValidationException)exception).Errors.Select(x => x.ErrorMessage),   // exception türü ValidationException yapildi.
                    StatusCode = StatusCodes.Status400BadRequest  // Eger validasyondan gecemediyse BadRequest donmeli
                }.ToString());

            List<string> errors = new()
            {
                $" Hata Mesajı: {exception.Message}",
            };

            return httpContext.Response.WriteAsync(new ExceptionModel
            {
                Errors = errors,
                StatusCode = statusCode
            }.ToString());
        }

        // Status kodları bulup dondurecek olan metod
        private static int GetStatusCode(Exception exception) =>
            // Switch'in farkli bir kullanimi => her defasinda case yazmak yerine boyle de kullanilabilir.
            exception switch
            {
                BadRequestException => StatusCodes.Status400BadRequest,
                NotFoundException => StatusCodes.Status400BadRequest,
                ValidationException=>StatusCodes.Status422UnprocessableEntity,
                _ => StatusCodes.Status500InternalServerError  // Default status kod (eger yukaridakilerden biri degilse bu donecek)
            };
    }
}
