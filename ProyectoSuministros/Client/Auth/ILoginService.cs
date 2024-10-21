using ProyectoSuministros.Shared.DTOs;
using System;
namespace ProyectoSuministros.Client.Auth
{
	public interface ILoginService
	{
		Task Login(UserTokenDTO token);
		Task Logoute();
		Task ManejarRenovacionToken();
		Task CheckLoginApp();
	}
}

