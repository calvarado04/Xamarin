using System;
using Xamarin.Forms;

//Carlos Alvarado Martínez
//Número de Cuenta 304045046
//Actividad 3 Programación de Dispositivos Móviles

//El namespace de la aplicación
namespace Actividad3
{
	//La clase pública App
	public class App
	{
		//Se declara la Page GetMainPage
		public static Page GetMainPage ()
		{	

			//Se crea el botón button de tipo Button
			Button button = new Button {
				Text = "Dale Clic",
				TextColor = Color.White,
				BackgroundColor = Color.Blue,
			};

			//Label llamado header que muestra texto de entrada
			Label header = new Label
			{
				Text = "Aplicación que muestra un botón",
				Font = Font.BoldSystemFontOfSize(18),
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.StartAndExpand

			};

			// Label llamado label que muestra texto al final
			Label label = new Label
			{
				Text = "Actividad 3",
				Font = Font.SystemFontOfSize(NamedSize.Large),
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.EndAndExpand
			};

			//Se declarara el nuevo contentPage de tipo ContentPage
			ContentPage contentPage = new ContentPage();

			//El contenido de contentPage es un médotodo Content que tiene un StackLayout
			contentPage.Content = new StackLayout {
				Padding = 10,
				VerticalOptions = LayoutOptions.Center,
				Children = {
					header,
					button,
					label
				}
			};
					
		    //Acción al dar clic, muestra mi nombre
			button.Clicked += delegate {
				button.Text = string.Format ("Carlos Alvarado Martínez");
			};

			//GetMainPage retnorna el contenPage definido atrás, se usa en MainActivity.cs
			return contentPage;

		}
	}
}