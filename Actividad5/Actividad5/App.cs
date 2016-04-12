using System;
using Xamarin.Forms;
using System.Net.Http;
using System.Collections.Generic;


namespace Actividad5
{
	public class App
	{
			
			public static Page GetMainPage ()
			{	

				var usuario = new Entry { Placeholder = "Usuario" };
				
				var password = new Entry { Placeholder = "Password", IsPassword = true };

				var login = new Button { Text = "Entrar" };

				ContentPage contentPage = new ContentPage();

				contentPage.Padding = new Thickness (5, Device.OnPlatform (20, 5, 5), 5, 5);

				StackLayout stackLayout = new StackLayout {
					Children = {
						new Label
						{
							Text = "Conectarse a eCampus.fca.unam.mx",
							TextColor = Color.Blue
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

				string url = "http://fedomex.xyz/Tienda-en-Linea/login.html";
				string vstatuscode = String.Empty;

				using (var client = new HttpClient()) {
					var content = new FormUrlEncodedContent(new[] {
						new KeyValuePair<string, string>("username", usuario.Text),
						new KeyValuePair<string, string>("password", password.Text)
					});

					using (var response = await client.PostAsync(url, content)) {
						using (var responseContent = response.Content) {
							vstatuscode =  response.StatusCode.ToString();
							await contentPage.DisplayAlert("Respuesta del servidor",vstatuscode, "Cerrar", null);
						}
					}
				}


			};

				return contentPage;
		
			}
		}
}
