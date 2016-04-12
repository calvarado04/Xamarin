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

		public static INavigation Navigation { get; private set; }
		public static Page GetMainPage() 
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
				//Centramos el botón al centro
				VerticalOptions = LayoutOptions.Center,
				//Agregamos el botón
				Children = {
					new Label
					{
						Text = "Probar conexión a Fedomex.xyz",
						TextColor = Color.Silver,
						FontSize = 22
					},
					usuario,
					password,
					btnLogin,
					new Label
					{
						Text = "Actividad 6 Carlos Alvarado Martínez",
						TextColor = Color.Silver,
						FontSize = 18
					}
				}
			};

			//Cuando se le da clic al boton de Login, la aplicación se conectará a Fedomex.xyz
			//Pasando como argumento de usuario y contraseña lo que se haya introducido
			//Se obtiene la cadena de respuesta del servidor, que se comparará para determinar
			//si la conexión fue aceptada o fue rechazada por el servidor
			btnLogin.Clicked += async (object sender, EventArgs e) => {
				
				//URL de la página de login de Fedomex
				string url = "http://fedomex.xyz/Tienda-en-Linea/login.html";
				string result = String.Empty;

				//Se usa el HttpClient
				using (var client = new HttpClient()) {
					var content = new FormUrlEncodedContent(new[] {
						new KeyValuePair<string, string>("username", usuario.Text),
						new KeyValuePair<string, string>("password", password.Text)
					});

					//Se espera la respuesta del servidor y se guarda en un string
					using (var response = await client.PostAsync(url, content)) {
						using (var responseContent = response.Content) {
							result = await responseContent.ReadAsStringAsync();

							//IF Statement, si en la cadena que se obtuvo aparece Bienvenido de nuevo,
							//Se cambia al nuevo StackLayout y si no, no regresa a la pantalla principal
							if (result.Contains("Bienvenido de nuevo")) {
								result = "Sí";

								//Botón de regreso en el nuevo StackLayout
								 Button btnBack = new Button {
									Text = "Regresar",
									TextColor = Color.White,
									BackgroundColor = Color.FromHex ("77D065")
								};

								//Contenido del nuevo StackLayout
								contentPage.Content = new StackLayout {
									Padding = 4,
									VerticalOptions = LayoutOptions.Center,
									Children = {
										new Label
										{
											Text = "¡La conexión a Fedomex.xyz fue exitosa!",
											TextColor = Color.Silver, 
											FontSize = 22
										},
										btnBack,
										new Label
										{
											Text = "Actividad 6 Carlos Alvarado Martínez",
											TextColor = Color.Silver,
											FontSize = 18
										}
									}
								};

								//Cuando el Botón de regreso se presiona, se regresa a la pantalla principal
								btnBack.Clicked += (sender2, f) => {
									contentPage.Navigation.PushModalAsync(GetMainPage ());;
								};

									
							
							}

							//La parte else del IF STATEMENT
							else {
								result = "No";
							}
						}
					}

					//Muestra el display Alert requerido, tanto para sí o para no	
					await contentPage.DisplayAlert("Respuesta del servidor",result, "OK");
				}

			};

			//Return del NavigationPage
			return new NavigationPage(contentPage);
		}
	}
}

