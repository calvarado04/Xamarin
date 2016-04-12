using System;
using Xamarin.Forms;
using System.Net.Http;
using System.Collections.Generic;

namespace Actividad6
{
	public class App : Application
	{

		public App ()
		{
			MainPage = GetMainPage ();
		}

		//Ahora regresamos una pagina de tipo NavigationPage para navegar entre pantallas
		public static NavigationPage GetMainPage ()
		{	

			var usuario = new Entry { Placeholder = "Usuario" };

			var password = new Entry { Placeholder = "Password", IsPassword = true };
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
						Text = "Probar conexión a Fedomex.xyz",
						TextColor = Color.Silver
					},
					usuario,
					password,
					btnLogin,
					new Label
					{
						Text = "Actividad 6 Carlos Alvarado Martínez",
						TextColor = Color.Silver
					}
				}
			};

			//Cuando se de click al boton, se ejecuta la llamada remota al servidor
			//Async indica que la llamada se hace de manera asyncrona. 
			//Si se hiciera de manera sincrona, se bloquearia el hilo de ejecucion actual
			//y se notaria un efecto de pantalla "pasmada".
			btnLogin.Clicked += async (object sender, EventArgs e) => {
				string url = "http://fedomex.xyz/Tienda-en-Linea/login.html";
				string result = String.Empty;

				using (var client = new HttpClient()) {
					var content = new FormUrlEncodedContent(new[] {
						new KeyValuePair<string, string>("username", usuario.Text),
						new KeyValuePair<string, string>("password", password.Text)
					});

					using (var response = await client.PostAsync(url, content)) {
						using (var responseContent = response.Content) {
							result = await responseContent.ReadAsStringAsync();

							if (result.Contains("Bienvenido de nuevo")) {
								result = "Sí";
							}
							else {
								result = "No";
							}
						}
					}


					await contentPage.DisplayAlert("Respuesta del servidor",result, "OK");
				}

			};
			return new NavigationPage(contentPage);
		}
	}
}

