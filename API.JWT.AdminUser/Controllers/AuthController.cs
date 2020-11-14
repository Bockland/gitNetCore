using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;


namespace API.JWT.AdminUser.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("[action]")]
        public string Login(RequestToken Request)
        {
            // Se obtienen los usuarios autorizados en base a lo enviado
            User UsuarioApi = obtenerUsuario(Request.usuario, Request.password);
            if (UsuarioApi == null) return "Datos enviado no validos";

            // Leemos el secret_key desde nuestro appseting
            var secretKey = _configuration.GetValue<string>("SecretKey");
            var key = Encoding.ASCII.GetBytes(secretKey);

            // Creamos los claims (pertenencias, características) del usuario
            var claims = new[]
            {
                new Claim("UserData", JsonConvert.SerializeObject(UsuarioApi))
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                // Nuestro token va a durar un día
                Expires = DateTime.UtcNow.AddDays(1),
                // Credenciales para generar el token usando nuestro secretykey y el algoritmo hash 256
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(createdToken);
        }

        public User obtenerUsuario(string username, string password)
        {
            var url = $"http://arsene.azurewebsites.net/UserData?username=" + username + "&password=" + password;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return null;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd() ;
                            List<User> deserializedObject = JsonConvert.DeserializeObject<List<User>>(responseBody);
                            if (deserializedObject.Count > 0) return deserializedObject[0];
                            else return null;
                        }
                    }
                }
            }
            catch (WebException)
            {
                return null;
            }
        }
    }
}
