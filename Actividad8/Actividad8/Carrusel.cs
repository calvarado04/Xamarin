using System;
using Xamarin.Forms;
using System.Collections.Generic;

namespace Actividad8
{
	
	public class Carrusel : ContentPage
	{
		
		public static Page GetMainPage ()
		{	

			List<ContentPage> pages = new List<ContentPage> (){

			};

			//Lista de fotos remotas para el carrusel
			var uri1 = new Uri("http://wallpapershome.com/images/pages/pic_h/4827.jpg");
			var foto1 = new Image { Aspect = Aspect.AspectFit };
			foto1.Source = ImageSource.FromUri(uri1);

			var uri2 = new Uri("http://wallpapershome.com/images/pages/pic_h/9248.jpg");
			var foto2 = new Image { Aspect = Aspect.AspectFit };
			foto2.Source = ImageSource.FromUri(uri2);

			var uri3 = new Uri("http://wallpapershome.com/images/pages/pic_h/3956.jpg");
			var foto3 = new Image { Aspect = Aspect.AspectFit };
			foto3.Source = ImageSource.FromUri(uri3);

			var uri4 = new Uri("http://wallpapershome.com/images/pages/pic_h/5717.jpg");
			var foto4 = new Image { Aspect = Aspect.AspectFit };
			foto4.Source = ImageSource.FromUri(uri4);

			var uri5 = new Uri("http://wallpapershome.com/images/pages/pic_h/3312.jpg");
			var foto5 = new Image { Aspect = Aspect.AspectFit };
			foto5.Source = ImageSource.FromUri(uri5);

			var uri6 = new Uri("http://wallpapershome.com/images/pages/pic_h/4800.jpg");
			var foto6 = new Image { Aspect = Aspect.AspectFit };
			foto6.Source = ImageSource.FromUri(uri6);

			//Arreglo que contiene las fotos remotas de tipo Image
			Image[] fotos = { foto1, foto2, foto3, foto4, foto5, foto6 };

			//Arreglo que contiene los colores que se usarán como fondo de cada página del carrusel
			Color[] colors = { Color.Red, Color.Green, Color.Blue, Color.Purple, Color.Silver, Color.Teal };

			//Ciclo for que genera cada página del carrusel usando los dos arreglos anteriores
			for (int i = 0; i < 6; i++) {
							
				pages.Add (new ContentPage { 

					Padding = new Thickness (5, Device.OnPlatform (20, 5, 5), 5, 5),

					Content = new StackLayout {

						Orientation = StackOrientation.Vertical,
						HorizontalOptions = LayoutOptions.CenterAndExpand,
						VerticalOptions = LayoutOptions.CenterAndExpand,
						BackgroundColor = colors[i],

						Children = {
							new Label {
								Text = "Actividad 8 - Carrusel de fotos remotas",
								TextColor = Color.White, 
								VerticalOptions = LayoutOptions.StartAndExpand,
								FontSize = 18
							},
							fotos[i],
							new Label {
								Text = "Alumno: Carlos Alvarado Martínez",
								TextColor = Color.White, 
								VerticalOptions = LayoutOptions.EndAndExpand,
								FontSize = 18
							}
						}
					}
				});
			};

			//Retorno del carrusel creado
			return new CarouselPage {
				Children = { pages [0],
					pages [1],
					pages [2],
					pages [3],
					pages [4],
					pages [5]

				}

			};

		}
	}
}