using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.JWT.AdminUser.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UsuariosController : ControllerBase
    {        
        [HttpPost]
        [Route("listarUsuarios")]
        public ResponseUsuarioLista listarUsuarios()
        {
            try
            {
                List<UsuariosApi> usuarios = new List<UsuariosApi>();                
                usuarios = clsMetodos.obtenerListado();
                return new ResponseUsuarioLista(usuarios, "OK");                
            }
            catch (Exception ex)
            {               
                return new ResponseUsuarioLista(null, "Error: " + ex.Message);
            }            
        }

        [HttpPost]
        [Route("crearUsuario")]
        public ResponseUsuarioCreate crearUsuario(UsuariosApi Request)
        {
            try
            {
                string res = clsMetodos.crearUsuario(Request);
                UsuariosApi response = JsonConvert.DeserializeObject<UsuariosApi>(res);

                return new ResponseUsuarioCreate("OK", response);
            }
            catch (Exception ex)
            {
                return new ResponseUsuarioCreate("Error: " + ex.Message, null);
            }
        }

        [HttpPost]
        [Route("actualizarUsuario")]
        public ResponseUsuarioUpdate actualizarUsuario(UsuariosApi Request)
        {
            try
            {
                if (string.IsNullOrEmpty(Request.id)) return new ResponseUsuarioUpdate("No se envia ID", null);

                string res = clsMetodos.actualizarUsuario(Request);
                UsuariosApi response = JsonConvert.DeserializeObject<UsuariosApi>(res);

                return new ResponseUsuarioUpdate("OK",response);
            }
            catch (Exception ex)
            {
                return new ResponseUsuarioUpdate("Error: " + ex.Message, null);
            }
        }

        [HttpPost]
        [Route("eliminarUsuario")]
        public ResponseUsuarioDelete eliminarUsuario(UsuariosApi Request)
        {
            try
            {
                if (string.IsNullOrEmpty(Request.id)) return new ResponseUsuarioDelete("No se envia ID");

                string response = clsMetodos.eliminarUsuario(Request.id);
                return new ResponseUsuarioDelete(response);
            }
            catch (Exception ex)
            {
                return new ResponseUsuarioDelete("Error: " + ex.Message);
            }
        }
    }
}
