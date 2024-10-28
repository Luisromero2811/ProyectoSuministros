using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProyectoSuministros.Server.Helpers;
using ProyectoSuministros.Server.Identity;
using ProyectoSuministros.Shared.DTOs;
using Microsoft.EntityFrameworkCore;
using ProyectoSuministros.Shared.Modelos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProyectoSuministros.Server.Controllers.UsuarioController
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<IdentityUsuario> userManager;
        private readonly VerifyUserId verifyUser;

        public UsuarioController(ApplicationDbContext context, UserManager<IdentityUsuario> userManager, VerifyUserId verifyUser)
        {
            this.context = context;
            this.userManager = userManager;
            this.verifyUser = verifyUser;
        }

        [HttpGet("list")]
        public async Task<ActionResult> GetUsers([FromQuery] ParametrosBusquedaUsuarios parametros)
        {
            try
            {
                // Materializamos la lista de usuarios, evitando un AsQueryable en el resultado parcial.
                var usuarios = await context.Usuario.Select(x => new UsuarioInfo
                {
                    UserName = x.Usu!,
                    Password = x.Cve!,
                    Nombre = x.Den!,
                    UserCod = x.Cod,
                    Activo = x.Activo,
                }).ToListAsync();

                // Creamos una nueva lista de usuarios completa con roles
                var usuariosConRoles = new List<UsuarioInfo>();

                // Iteramos sobre los usuarios para agregar roles e ID
                foreach (var item in usuarios)
                {
                    var u = await context.Users.Where(x => x.UserCod == item.UserCod).FirstOrDefaultAsync();
                    if (u != null)
                    {
                        IList<string> roles = await userManager.GetRolesAsync(u);

                        // Asignamos los datos adicionales al objeto temporal
                        item.Roles = roles.ToList();
                        item.Id = u.Id;
                    }

                    usuariosConRoles.Add(item);
                }

                // Calculamos manualmente los valores de paginación
                var totalUsuarios = usuariosConRoles.Count;
                var totalPaginas = (int)Math.Ceiling(totalUsuarios / (double)parametros.tamanopagina);

                // Insertamos los parámetros de paginación en el encabezado HTTP manualmente
                HttpContext.Response.Headers["conteo"] = totalUsuarios.ToString();
                HttpContext.Response.Headers["paginas"] = totalPaginas.ToString();
                HttpContext.Response.Headers["pagina"] = parametros.pagina.ToString();

                // Aplicamos la paginación
                var usuariosPaginados = usuariosConRoles
                    .Skip((parametros.pagina - 1) * parametros.tamanopagina)
                    .Take(parametros.tamanopagina);

                return Ok(usuariosPaginados);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpGet("roles")]
        public async Task<ActionResult<List<RolDTO>>> Get()
        {
            return await context.Roles.Select(x => new RolDTO { ID = x.Id, NombreRol = x.Name! }).ToListAsync();
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Create([FromBody] UsuarioInfo info)
        {
            try
            {
                var userSistema = await context.Usuario.FirstOrDefaultAsync(x => x.Usu == info.UserName);
                if (userSistema != null)
                    return BadRequest("El usuario ya existe");

                var userAsp = await userManager.FindByNameAsync(info.UserName);
                if (userAsp != null)
                    return BadRequest("El usuario ya existe");

                var newUserSistema = new Usuario
                {
                    Den = info.Nombre,
                    Usu = info.UserName,
                    Cve = info.Password,
                };
                context.Add(newUserSistema);

                await context.SaveChangesAsync();

                var newUserAsp = new IdentityUsuario { UserName = newUserSistema.Usu, UserCod = newUserSistema.Cod };
                var result = await userManager.CreateAsync(newUserAsp, newUserSistema.Cve);
                //Si el resultado no fue exitoso
                if (!result.Succeeded)
                {
                    context.Remove(newUserSistema);
                    await context.SaveChangesAsync();
                    return BadRequest(result.Errors);
                }
                result = await userManager.AddToRolesAsync(newUserAsp, info.Roles);
                //Si el resultado no fue exitoso
                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }
                //Si el resultado fue exitoso, retorna el nuevo usuario

                return Ok(newUserSistema);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("editar")]
        public async Task<ActionResult> PutUser([FromBody] UsuarioInfo info)
        {
            try
            {
                //Buscamos al usuario y lo sincronizamos con el usuario de ASP
                var updateUserSistema = await context.Usuario.FirstOrDefaultAsync(x => x.Cod == info.UserCod);

                if (updateUserSistema is null)
                    return NotFound();


                var oldUser = updateUserSistema;

                //Variable para asignacion de la vieja contraseña
                var viejaPass = updateUserSistema.Cve;
                //Nuevos datos a actualizar del usuario, Nombre, nombre de usuario y contraseña
                updateUserSistema.Den = info.Nombre;
                updateUserSistema.Usu = info.UserName;
                updateUserSistema.Cve = info.Password;
                //Actualizacion de registros
                context.Update(updateUserSistema);

                //Actualizar Usuario de Identity AspNet
                var updateUserAsp = await userManager.FindByIdAsync(info.Id);

                if (updateUserAsp != null)
                {
                    //Variable para asignacion de los roles
                    var roles = info.Roles;
                    //Nuevo dato a actualizar del usuario de Asp, solo mandamos el Nombre de usuario 
                    updateUserAsp.UserName = info.UserName;
                    //Nuevo dato para actualizar la contraseña
                    var changepassword = await userManager.ChangePasswordAsync(updateUserAsp, viejaPass, updateUserSistema.Cve);
                    //A través de estas acciones, vamos a obtener, remover y volver a agregar el listado de roles
                    //Method para obtención de los roles
                    var changeGetRoles = await userManager.GetRolesAsync(updateUserAsp);
                    //Method para eliminar los roles 
                    var resultDeleteRoles = await userManager.RemoveFromRolesAsync(updateUserAsp, changeGetRoles.ToList());
                    //Method para mandar el listado de roles
                    var resultAddRoles = await userManager.AddToRolesAsync(updateUserAsp, roles);
                    //Segundo parametros me pide un string de roles no un listado 

                    var resultado = await userManager.UpdateAsync(updateUserAsp);

                    if (!resultado.Succeeded)
                    {
                        context.Update(oldUser);
                        await context.SaveChangesAsync();
                        return BadRequest();
                    }
                }

                //Asignación del rol editado

                //Se retorna al usuario actualizado
                return Ok(updateUserSistema);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("activar")]
        public async Task<ActionResult> PutActive([FromBody] UsuarioInfo info)
        {
            try
            {
                var usuario = await context.Usuario.FirstOrDefaultAsync(x => x.Cod == info.UserCod);
                //Si el usuario no existe
                if (usuario == null)
                {
                    return NotFound();
                }
                usuario.Activo = info.Activo;
                //Función para actualizar el estado activo del usuario
                context.Update(usuario);

                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

    }
}

