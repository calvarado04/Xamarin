using System;
using Xamarin.Forms;

//Carlos Alvarado Martínez
//Actividad 7 Programación de Dispositivos Móviles

namespace Actividad7
{
	public class App : Application
	{

		public App ()
		{
			//Se llama el GetMainPage de PaginaColores
			MainPage = PaginaColores.GetMainPage ();
		}

	}
}