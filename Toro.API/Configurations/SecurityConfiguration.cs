using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Toro.API.Configurations;

public static class SecurityConfiguration
{
    public static void AddSecurityConfiguration(this WebApplicationBuilder builder)
    {
        //Configuração do Token
        var key = Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("ApplicationKey")!);
        builder.Services.AddSingleton(new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature));

        builder.Services.AddAuthentication(authOptions =>
        {
            authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateAudience = false,
                ValidateIssuer = false
            };
        });

        // Ativa o uso do token como forma de autorizar o acesso
        // a recursos deste projeto
        builder.Services.AddAuthorization(auth =>
        {
            auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                .RequireAuthenticatedUser().Build());
        });

        //Para todas as requisições serem necessaria o token, para um endpoint não exisgir o token
        //deve colocar o [AllowAnonymous]
        //Caso remova essa linha, para todas as requisições que precisar de token, deve colocar
        //o atributo [Authorize("Bearer")]
        builder.Services.AddMvc(config =>
        {
            var policy = new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                .RequireAuthenticatedUser().Build();

            config.Filters.Add(new AuthorizeFilter(policy));
        });

        builder.Services.AddCors();
    }
}
