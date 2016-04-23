using System;
using Xamarin.Forms;
using System.Collections.Generic;


namespace Actividad7
{
	public class PaginaColores: ContentPage
	{

		public static Page GetMainPage() 
		{	
			//Creamos una lista de paginas de tipo ContentPage. 
			//Hasta ahora nuestra lista no tiene ningun elemento
			List<ContentPage> pages = new List<ContentPage> (){

			};

			//Creamos un arreglo de colores con 6 elementos, Red, Green, Blue, Yellow, Silver y White
			Color[] colors = { Color.Red, Color.Green, Color.Blue, Color.Yellow, Color.Silver, Color.Teal };

			//Por cada color agregamos un nuevo objeto de tipo ContentPage a la lista
			foreach (Color c in colors) {
				pages.Add (new ContentPage { 


					Padding = new Thickness (0, Device.OnPlatform (40, 40, 0), 0, 0),
					Content = new StackLayout {




						//Cada pagina tiene como elementos un BoxView (un rectángulo) con 
						//uno de los colores definidos en el arreglo
						Children = {
							new BoxView {
								Color = c,
								VerticalOptions = LayoutOptions.FillAndExpand 
							}
						}
					}

				});

			}

			//Agregamos las páginas al carouselPage
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