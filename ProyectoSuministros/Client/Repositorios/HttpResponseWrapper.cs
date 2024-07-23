using System;
using System.Net;

namespace ProyectoSuministros.Client.Repositorios
{
	public class HttpResponseWrapper<T>
	{
		public HttpResponseWrapper(T response, bool Error, HttpResponseMessage httpResponseMessage)
		{
            Response = response;
            this.Error = Error;
            HttpResponseMessage = httpResponseMessage;
        }

        public T Response { get; }
        public bool Error { get; }
        public HttpResponseMessage HttpResponseMessage { get; }

        //Method donde reutilizamos el HttpResponseWrapper para poder obtener informacion de errores de la webAPI de forma mas especifica
        public async Task<string?> ObtenerMensajeError()
        {
            //Si no existe ningun error no se hace nada
            if (!Error)
            {
                return null;
            }
            //Variable para poder guardar el status del code
            var codigoEstatus = HttpResponseMessage.StatusCode;

            if (codigoEstatus == HttpStatusCode.NotFound)
            {
                return "404, Recurso no encontrado";
            }
            else if (codigoEstatus == HttpStatusCode.BadRequest)
            {
                //Mandamos el cuerpo del mensaje de status
                return await HttpResponseMessage.Content.ReadAsStringAsync();
            }
            else if (codigoEstatus == HttpStatusCode.Unauthorized)
            {
                return "403, Acceso prohibido, verifica tener una sesion activa para realizar esta accion";
            }
            else if (codigoEstatus == HttpStatusCode.Forbidden)
            {
                return "401, No tienes permiso para hacer esta accion";
            }
            else if (codigoEstatus == HttpStatusCode.TooManyRequests)
            {
                return "429, El servidor contiene multiples peticiones, espere un momento";
            }
            else if (codigoEstatus == HttpStatusCode.ServiceUnavailable)
            {
                return "503, Servicio no Disponible. Intente mas tarde";
            }
            else if (codigoEstatus == HttpStatusCode.InternalServerError)
            {
                return "500, Error interno del servidor";
            }
            else
            {
                return "Ha ocurrido algun error inesperado";
            }
        }

    }
}

