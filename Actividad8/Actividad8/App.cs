using System;
using Xamarin.Forms;

//Carlos Alvarado Martínez
//Actividad 8 Programación de Dispositivos Móviles

namespace Actividad8
{
	
	public class App : Application
	{
		
		public App ()
		{	
			//Abrimos el GetMainPage de la página Carrusel
			MainPage = Carrusel.GetMainPage();

		}
	}
}