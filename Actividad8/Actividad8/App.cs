using System;
using Xamarin.Forms;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;

//Carlos Alvarado Martínez
//Actividad 8 Programación de Dispositivos Móviles

namespace Actividad8
{

	//Clase Status que servirá para el json que se recibe de la URL
	public class Status {
		public string status { get; set; } 
	}

	//La clase App que es la base del programa
	public class App : Application
	{

		public App ()
		{
			MainPage = GetMainPage ();
		}

		//Ahora regresamos una pagina de tipo NavigationPage para navegar entre pantallas
		public static NavigationPage GetMainPage ()
		{	
			//Creamos un boton con fondo verde y texto blanco
			Button btnLogin = new Button {
				Text = "Login",
				TextColor = Color.White,
				BackgroundColor = Color.FromHex ("77D065")
			};


			var usuario = new Entry { Placeholder = "Usuario" };

			var password = new Entry { Placeholder = "Password", IsPassword = true };

			ContentPage contentPage = new ContentPage();
			//Stacklayout permite agregar controles verticalmente
			//Por el momento solo tenemos un control que es el boton
			contentPage.Content = new StackLayout {
				//Espacio alrededor de los controles
				Padding = 10,

				Children = {
					new Label
					{
						Text = "Probar conexión a Fedomex.xyz",
						TextColor = Color.Silver,
						HorizontalOptions = LayoutOptions.Center,
						VerticalOptions = LayoutOptions.StartAndExpand,
						FontSize = 20
					},
					usuario,
					password,
					btnLogin,
					new Label
					{
						Text = "Actividad 8 Carlos Alvarado Martínez",
						TextColor = Color.Silver,
						HorizontalOptions = LayoutOptions.Center,
						VerticalOptions = LayoutOptions.EndAndExpand,
						FontSize = 20
					}
				}
			};

			btnLogin.Clicked += async (object sender, EventArgs e) => {

				//URL de la página de login de Fedomex
				string url = "http://fedomex.xyz/Tienda-en-Linea/login.html";
				string result = String.Empty;

				//Se usa el HttpClient
				using (var client = new HttpClient ()) {
					var content = new FormUrlEncodedContent (new[] {
						new KeyValuePair<string, string> ("username", usuario.Text),
						new KeyValuePair<string, string> ("password", password.Text)
					});

					//Se espera la respuesta del servidor y se guarda en un string
					using (var response = await client.PostAsync (url, content)) {
						using (var responseContent = response.Content) {
							result = await responseContent.ReadAsStringAsync ();

							//IF Statement, si en la cadena que se obtuvo aparece Bienvenido de nuevo,
							//Se cambia al nuevo StackLayout y si no, no regresa a la pantalla principal
							if (result.Contains ("Bienvenido de nuevo")) {
								result = "Sí";

								PaginaCarrusel carrusel = new PaginaCarrusel();

								//Generamos una nueva página de navegación de la Pagina del carrusel
								var todoPage = carrusel.Carrusel();
								await contentPage.Navigation.PushAsync(todoPage);

							}

							//La parte else del IF STATEMENT
							else {
								result = "No";
							}
						}
					}

					//Muestra el display Alert requerido, tanto para sí o para no	
					await contentPage.DisplayAlert ("Respuesta del servidor", result, "OK");
				}

			};

			//Return del NavigationPage
			return new NavigationPage(contentPage);
		}
	}
}