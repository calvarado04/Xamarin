using System;

using Xamarin.Forms;

namespace Actividad6
{
	public class NewPage : ContentPage
	{

		public NewPage ()
		{

			Content = new StackLayout { 

				Children = {
					new Label
					{
						Text = "¡La conexión a Fedomex.xyz fue exitosa!",
						TextColor = Color.Silver,
						HorizontalOptions = LayoutOptions.Center,
						VerticalOptions = LayoutOptions.StartAndExpand,
						FontSize = 20
					},
					new Label
					{
						Text = "Esta es una nueva página de navegación",
						TextColor = Color.Silver,
						HorizontalOptions = LayoutOptions.Center,
						VerticalOptions = LayoutOptions.CenterAndExpand,
						FontSize = 20
					},
					new Label
					{
						Text = "Actividad 6 Carlos Alvarado Martínez",
						TextColor = Color.Silver,
						HorizontalOptions = LayoutOptions.Center,
						VerticalOptions = LayoutOptions.EndAndExpand,
						FontSize = 20
					}
				}
			};
		}
	}
}