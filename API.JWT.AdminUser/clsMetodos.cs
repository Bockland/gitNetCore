using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace API.JWT.AdminUser
{
    public class clsMetodos
    {
        public static List<UsuariosApi> obtenerListado()
        {
            var url = $"http://arsene.azurewebsites.net/User";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";

            using (WebResponse response = request.GetResponse())
            {
                using (Stream strReader = response.GetResponseStream())
                {
                    if (strReader == null) return null;
                    using (StreamReader objReader = new StreamReader(strReader))
                    {
                        string responseBody = objReader.ReadToEnd();
                        ICollection<object> deserializedObject = JsonConvert.DeserializeObject<ICollection<object>>(responseBody);
                        List<UsuariosApi> Usuarios = new List<UsuariosApi>();

                        foreach (object obj in deserializedObject)
                        {
                            if (obj.GetType().Name.Equals("JArray"))
                            {
                                List<UsuariosApi> usrs = JsonConvert.DeserializeObject<List<UsuariosApi>>(obj.ToString());
                                foreach (UsuariosApi us in usrs) Usuarios.Add(us);
                            }
                            else
                            {
                                UsuariosApi usr = JsonConvert.DeserializeObject<UsuariosApi>(obj.ToString());
                                Usuarios.Add(usr);
                            }
                        }

                        return Usuarios;
                    }
                }
            }

        }

        public static string crearUsuario(UsuariosApi user)
        {
            string response = string.Empty;

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://arsene.azurewebsites.net/User");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(user);
                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                response = result;
            }

            return response;
        }

        public static string actualizarUsuario(UsuariosApi user)
        {
            string response = string.Empty;

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://arsene.azurewebsites.net/User/" + user.id);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "PUT";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(user);
                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                response = result;
            }

            return response;
        }

        public static string eliminarUsuario(string id)
        {
            string response = string.Empty;

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://arsene.azurewebsites.net/User/" + id);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "DELETE";

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                response = result;
            }

            return response;
        }
    }
}
