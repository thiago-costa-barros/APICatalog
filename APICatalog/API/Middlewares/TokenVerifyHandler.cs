using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace APICatalog.API.Middlewares
{
    public class TokenVerifyHandler
    {
        public static JwtBearerEvents GetEvents()
        {
            return new JwtBearerEvents
            {
                OnChallenge = async context =>
                {
                    context.HandleResponse(); // impede resposta padrão 401

                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.ContentType = "application/json";

                    var response = new
                    {
                        success = false,
                        statusCode = 401,
                        requestTime = DateTime.UtcNow,
                        errorType = "Unauthorized",
                        message = "Invalid Token or Not Found."
                    };

                    var json = JsonSerializer.Serialize(response);
                    await context.Response.WriteAsync(json);
                }
            };
        }
    }
}
