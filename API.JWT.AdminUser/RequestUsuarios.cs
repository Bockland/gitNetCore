using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.JWT.AdminUser
{
    

    public class UsuariosApi
    {
        public string nombre { get; set; }
        public string apellido { get; set; }
        public String email { get; set; }
        public string profesion { get; set; }
        public string name { get; set; }
        public string lastname { get; set; }
        public string id { get; set; }
        public string springboottestId { get; set; }   
    }

    public class ResponseUsuarioLista
    {
        public string mensaje { get; set; }
        public List<UsuariosApi> response { get; set; }        

        public ResponseUsuarioLista(List<UsuariosApi> res, string men)
        {
            this.response = res;
            this.mensaje = men;
        }
    }

    public class ResponseUsuarioCreate
    {
        public string mensaje { get; set; }
        public UsuariosApi response { get; set; }

        public ResponseUsuarioCreate(string men, UsuariosApi res)
        {
            this.response = res;
            this.mensaje = men;
        }
    }

    public class ResponseUsuarioUpdate
    {
        public string mensaje { get; set; }
        public UsuariosApi response { get; set; }

        public ResponseUsuarioUpdate(string men,UsuariosApi res)
        {
            this.response = res;
            this.mensaje = men;
        }
    }

    public class ResponseUsuarioDelete
    {
        public string response { get; set; }       

        public ResponseUsuarioDelete(string men)
        {            
            this.response = men;
        }
    }

    public class RequestToken
    {
        public string usuario { get; set; }
        public string password { get; set; }

        public RequestToken()
        {
            
        }
    }

    public class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string lastName { get; set; }

        public User()
        {
            
        }
    }

    
}
