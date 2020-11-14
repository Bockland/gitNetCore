# Desafio Cleverit
Proyecto de API REST con JWT para consumo de API de usuarios (CRUD)

# Controlador Auth
Se utiliza para obtener el token que despues sera utilizado en el header de los request hacia el controlador que ejecuta las acciones de usuarios.
Se consume como POST en la ruta https://localhost:44327/auth/login y recibe como cuerpo un objeto con un atributo "usuario" y otro "password" los cuales se utilizan para obtener los usuarios permitidos para obtener un token.

# Controlador Usuarios
Este controlador se encarga de recibir las peticiones de creación, actualización, eliminación y listar de usuarios hacia la API de usuarios disponibilizada.
Solo puede ser consumido si se envia en el header la etiqueta  Authorization con el valor: Bearer + token
Contiene 4 metodos que se consumen de manera POST:

# listarUsuarios -> /Usuarios/listarUsuarios
Metodo para obtener el listado completo de los usuarios. 
No recibe parametros.

# crearUsuario -> /Usuarios/crearUsuario
Metodo para realizar la creación de un usuario. Devuelve el usuario creado y un mensaje de exito.
Recibe como parametro un objeto de la clase UsuarioApi.

REQUEST:
{
    "nombre": "Javier1",
    "apellido": "Peralta",
    "email": "test@test.cl",
    "profesion": "des",
    "name": "jav123",
    "lastname": "something",
    "id": "JavPQ2"
  }
  
# actualizarUsuario -> /Usuarios/actualizarUsuario
Metodo para realizar la actualizacion de un usuario. Devuelve el usuario creado y un mensaje de exito.
Valida que el objeto enviado contenga el id.
Recibe como parametro un objeto de la clase UsuarioApi.

REQUEST:
{
    "nombre": "Javier2",
    "apellido": "Peralta",
    "email": "test@test.cl",
    "profesion": "des",
    "name": "jav123",
    "lastname": "something",
    "id": "JavPQ2"
  }
  
 # eliminarUsuario -> /Usuarios/eliminarUsuario
Metodo para realizar la elimación de un usuario.
Valida que el objeto enviado contenga el id.
Recibe como parametro un objeto de la clase UsuarioApi.

REQUEST:
{
    "id": "JavPQ2"
}
  
