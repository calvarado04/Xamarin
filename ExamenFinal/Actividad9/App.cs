	using System;
	using Xamarin.Forms;
	using System.Net.Http;
	using System.Collections.Generic;
	using Newtonsoft.Json;

	// Examen Final Programación de Dispositivos Móviles
	// Alumno: Carlos Alvarado Martínez
	// Número de Cuenta: 304045046

	namespace ExamenFinal
	{

		//Se genera el modelo de clases para el Json
		public class Tenencias
		{
			[JsonProperty("placa")]
			public string placa { get; set; }
			[JsonProperty("tieneadeudos")]
			public string tieneadeudos { get; set; }
		}

		public class Infracciones
		{
			[JsonProperty("folio")]
			public string folio { get; set; }
			[JsonProperty("fecha")]
			public string fecha { get; set; }
			[JsonProperty("situacion")]
			public string situacion { get; set; }
			[JsonProperty("motivo")]
			public string motivo { get; set; }
			[JsonProperty("fundamento")]
			public string fundamento { get; set; }
			[JsonProperty("sancion")]
			public string sancion { get; set; }
		}

		public class Verificaciones
		{
			[JsonProperty("placa")]
			public string placa { get; set; }
			[JsonProperty("vin")]
			public string vin { get; set; }
			[JsonProperty("marca")]
			public string marca { get; set; }
			[JsonProperty("submarca")]
			public string submarca { get; set; }
			[JsonProperty("modelo")]
			public int modelo { get; set; }
			[JsonProperty("combustible")]
			public string combustible { get; set; }
			[JsonProperty("certificado")]
			public int certificado { get; set; }
			[JsonProperty("cancelado")]
			public string cancelado { get; set; }
			[JsonProperty("vigencia")]
			public string vigencia { get; set; }
			[JsonProperty("verificentro")]
			public int verificentro { get; set; }
			[JsonProperty("linea")]
			public int linea { get; set; }
			[JsonProperty("fecha_verificacion")]
			public string fecha_verificacion { get; set; }
			[JsonProperty("hora_verificacion")]
			public string hora_verificacion { get; set; }
			[JsonProperty("resultado")]
			public string resultado { get; set; }
			[JsonProperty("causa_rechazo")]
			public string causa_rechazo { get; set; }
			[JsonProperty("equipo_gdf")]
			public string equipo_gdf { get; set; }
		}

		public class Consulta
		{
			[JsonProperty("placa")]
			public string placa { get; set; }
			[JsonProperty("tenencias")]
			public Tenencias tenencias { get; set; }
			[JsonProperty("infracciones")]
			public IList<Infracciones> infracciones { get; set; }
			[JsonProperty("verificaciones")]
			public IList<Verificaciones> verificaciones { get; set; }
		}

		public class RootObject
		{
			[JsonProperty("consulta")]
			public Consulta consulta { get; set; }
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
					Text = "Consulta Placa CDMX",
					TextColor = Color.White,
					BackgroundColor = Color.FromHex ("77D065")
				};


				var placaE = new Entry { Placeholder = "Placa" };


				ContentPage contentPage = new ContentPage();
				//Stacklayout permite agregar controles verticalmente
				//Por el momento solo tenemos un control que es el boton
				contentPage.Content = new StackLayout {
					//Espacio alrededor de los controles
					Padding = 10,

					Children = {
						new Label
						{
							Text = "Verificar placa automóvil CDMX",
							TextColor = Color.Silver,
							HorizontalOptions = LayoutOptions.Center,
							VerticalOptions = LayoutOptions.StartAndExpand,
							FontSize = 20
						},
						placaE,
						btnLogin,
						new Label
						{
							Text = "Examen Final Carlos Alvarado Martínez",
							TextColor = Color.Silver,
							HorizontalOptions = LayoutOptions.Center,
							VerticalOptions = LayoutOptions.EndAndExpand,
							FontSize = 20
						}
					}
				};
						
				btnLogin.Clicked += async (object sender, EventArgs e) => {

					//URL de la página del gobierno de la Ciudad de México para verificar las placas
					string url = string.Format(@"http://datos.labplc.mx/movilidad/vehiculos/{0}.json", placaE.Text);

					//Se usa el HttpClient
				using (var client = new HttpClient ()) try {
					
						//Dado que el servidor del gobierno de la Ciudad de México a veces no responde el JSON, hay que manejar un Timeout (15 segundos)
						client.Timeout = TimeSpan.FromMilliseconds(15000);

						string resultado = await client.GetStringAsync(url);
				
						RootObject rootArray;

						//Se deserializa el Json en la lista rootArray
						rootArray = JsonConvert.DeserializeObject<RootObject>(resultado);

						//Se pasa la consulta al rootObject de tipo Consulta para poder checar las infracciones
						Consulta rootObject = rootArray.consulta;

						//Cuenta la cantidad de infracciones que aparezcan en la lista
						int cantidad = rootObject.infracciones.Count;

						if (cantidad == 0) {

							await contentPage.DisplayAlert (string.Format("Verifica placa CDMX: {0}", placaE.Text),string.Format("El vehículo con placa: {0} no ha tenido infracciones en la CDMX", placaE.Text), "Cerrar");

						} else {

							await contentPage.DisplayAlert (string.Format("Verifica placa CDMX: {0}", placaE.Text),string.Format("El vehículo con placa: {0} ha tenido {1} infracciones en la CDMX", placaE.Text, cantidad), "Cerrar");

						}

					}

					catch  (HttpRequestException) {

						Console.WriteLine("HttpRequestException");

					}
					
					//Envía mensaje si hubo Timeout
					catch (System.Threading.Tasks.TaskCanceledException) {

						await contentPage.DisplayAlert (string.Format("Verifica placa CDMX: {0}", placaE.Text),"El servidor no responde, favor de intentar más tarde", "Cerrar");

					}
					
				};

				//Return del NavigationPage
				return new NavigationPage(contentPage);
			}
		}
	}