using System;
using Xamarin.Forms;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;

//Carlos Alvarado Martínez
//Actividad 5 Programación de Dispositivos Móviles

namespace Actividad5
{
	//Clase Status que servirá para el json que se recibe de la URL
	public class Status {
		public string status { get; set; } 
	}

	//La clase App que es la base del programa
	public class App
	{
			
			public static Page GetMainPage ()
			{	

				var usuario = new Entry { Placeholder = "Usuario" };
				
				var password = new Entry { Placeholder = "Password", IsPassword = true };

				var login = new Button { Text = "Entrar" };

				ContentPage contentPage = new ContentPage();

				contentPage.Padding = new Thickness (5, Device.OnPlatform (20, 5, 5), 5, 5);

				//El StackLayout de la aplicación
				StackLayout stackLayout = new StackLayout {
					Children = {
						new Label
						{
						Text = "Conectarse a 104.42.52.205/mobile/login",
							TextColor = Color.White
						},
						usuario,
						password,
						login,
						new Label
						{
							Text = "Actividad 5 Carlos Alvarado Martínez",
							TextColor = Color.Silver
						}
					}
				};
					
				contentPage.Content = stackLayout;

				//Cuando se le da clic al botón de Login se manda la conexión con el HTTP Client
				login.Clicked += async (object sender, EventArgs e) => {

				//La URL a la que nos conectaremos
				string url = @"http://104.42.52.205/mobile/login";
				string vstatuscode = String.Empty;

				//El HttpClient que se usará 
				using (var client = new HttpClient()) {
					var content = new FormUrlEncodedContent(new[] {
						new KeyValuePair<string, string>("user", usuario.Text),
						new KeyValuePair<string, string>("password", password.Text)
					});
						
					using (var response = await client.PostAsync(url, content)) {
						using (var responseContent = response.Content) {

							//Obtenemos la cadena de respuesta del servidor en formato json
							vstatuscode = await responseContent.ReadAsStringAsync();

							//Declaramos el result que es un objeto de clase Status
							Status result = new Status();

							//Poblamos el objeto result con el json obtenido de la página web
							JsonConvert.PopulateObject(vstatuscode, result);

							//Asignamos el método status del objeto result de tipo Status a la variable resultado
							string resultado = result.status;

							//Mandamos una alerta con el contenido de resultado (el status del json)
							await contentPage.DisplayAlert("Respuesta del servidor", resultado, "Cerrar", null);
						}
					}
				}


			};
				//Se retorna el contenido de la página
				return contentPage;
		
			}
		}
}
