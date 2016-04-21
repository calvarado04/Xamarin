﻿using System;
using Xamarin.Forms;
using System.Net.Http;

//Carlos Alvarado Martínez
//Actividad 4 Programación de Dispositivos Móviles

namespace Actividad4
{
	public class App
	{
		public static Page GetMainPage ()
		{	
			//Creamos un boton con fondo verde y texto blanco
			Button btnLogin = new Button {
				Text = "Login",
				TextColor = Color.White,
				BackgroundColor = Color.FromHex ("77D065")
			};

			ContentPage contentPage = new ContentPage();
			//Stacklayout permite agregar controles verticalmente
			//Por el momento solo tenemos un control que es el boton
			contentPage.Content = new StackLayout {
				//Espacio alrededor de los controles
				Padding = 10,
				//Centramos el boton al centro
				VerticalOptions = LayoutOptions.Center,
				//Agregamos el boton
				Children = {
					new Label
					{
						Text = "Muestra el contenido de http://104.42.52.205",
						TextColor = Color.Silver
					},
					btnLogin,
					new Label
					{
						Text = "Actividad 4 Carlos Alvarado Martínez",
						TextColor = Color.Silver
					}
				}
			};

			//Cuando se de click al boton, se ejecuta la llamada remota al servidor
			//Async indica que la llamada se hace de manera asincrona. 
			//Si se hiciera de manera sincrona, se bloquearia el hilo de ejecucion actual
			//y se notaria un efecto de pantalla "pasmada".
			btnLogin.Clicked += async (object sender, EventArgs e) => {
				//Creamos un nuevo objeto para hacer la llamada remota
				var client = new HttpClient();

				//Esta es la llamada al servidor. GetStringAsync nos devuelve una cadena con la respuesta del servidor.
				//Como este es un servidor de pruebas, no tiene un dominio, solo una ip.
				var response = await client.GetStringAsync("http://104.42.52.205");

				//Imprimimos en pantalla la respuesta del servidor
				//Los parametros son: titulo, mensaje, y el texto de los botones. En este caso solo tenemos un boton OK.
				//Le faltaba la otra respuesta del servidor, en este caso NULL porque no se requiere otra.
				await contentPage.DisplayAlert("Respuesta del servidor",response, "OK", null);
			};

			return contentPage;
		}
	}
}

