# Cours Xamarin.Forms pour les étudiants IMERIR (MAD)

## Librairies
- Xamarin.Essential: https://github.com/xamarin/Essentials
- MvvmLight: https://github.com/lbugnion/mvvmlight
- NewtonSoft: https://github.com/JamesNK/Newtonsoft.Json
- Behaviors: https://github.com/davidbritch/behaviors
- ImageCirclePlugin: https://github.com/jamesmontemagno/ImageCirclePlugin

## Getting started
### C#

### Xamarin

## Architecture
- MVVM
- IoC
- Dependency Injection
- PCL
- DataBinding

## Troubleshooting
- Android clear htttp -> true (Research)
https://medium.com/@son.rommer/fix-cleartext-traffic-error-in-android-9-pie-2f4e9e2235e6

- iOS: The resource could not be loaded because the App Transport Security policy requires the use of a secure connection.
https://forums.xamarin.com/discussion/99876/xamarin-ios-on-visual-studio-2017-requires-the-use-of-a-secure-connection

- Change Android status bar color
`Window.SetStatusBarColor(Color.ParseColor("#920000"));`


- Création d'un projet Multiplatform -> Appp -> Blank Forms App -> App name -> Android/iOS / -> Use.NET Standard (not Shared Library)
- Mettre à jours les paquets
- Design de la vue 
- Binding context -> this


- Install MVVMLightLibs in all projects and inherits from ViewModelBase
Define 2 properties for the Text and one for the list
- Set binding context from MainPage
- 
    <ContentPage.BindingContext>
        <viewModels:MainViewModel />
    </ContentPage.BindingContext>
	
- private string text;
        public string Text
        {
            get => text;
            set => Set(ref text, value);
        }

        private ObservableCollection<JsonShow> shows;
        public ObservableCollection<JsonShow> Shows
        {
            get => shows;
            set => Set(ref shows, value);
        }
		
- 

- Classe Show and JsonShow
```
public class Show
{
    public int Id { get; set; }
    public string Url { get; set; }
    public string Name { get; set; }
    public string Language { get; set; }
    public List<string> Genres { get; set; }
}

public class JsonShow
{
    public double Score { get; set; }
    public Show Show { get; set; }
}


```



Command 
https://docs.microsoft.com/fr-fr/xamarin/xamarin-forms/user-interface/button

Présentation des Layouts
Grid (column/rows)
Stack
Namespace

Create a SearchBar
https://docs.microsoft.com/fr-fr/xamarin/xamarin-forms/user-interface/searchbar

ListView
https://docs.microsoft.com/fr-fr/xamarin/xamarin-forms/user-interface/listview/
(Don't let listview to Auto in Grid if you set RowHeight)

CollectionView




Colors

Create a Theme folder in PCL project
Create a New File (ContentPage XAML ou ContentView XAML)
Effacer le xaml.cs
Et copier ce contenu

<?xml version="1.0" encoding="UTF-8"?>
<ResourceDictionary 
    xmlns="http://xamarin.com/schemas/2014/forms"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml">
    <Color x:Key="RovaniemoveColor">#CB3416</Color>
</ResourceDictionary>


Dans le App.xaml, remplacer <Application.Resources/> par 
<Application.Resources>
        <ResourceDictionary Source="Themes/Colors.xaml" />
    </Application.Resources>



Pour avoir un titre sur les tab, ne pas oublier d'ajouter une navigation page

Bonus: Ajouter LArge title sur iOS


Pour selection -> Ajouter lib behaviros a PCL (uniquement)


SearchBar command 
<SearchBar
            x:Name="searchBar"
            Placeholder="Rechercher..."
            SearchCommand="{Binding SearchCommand}"
            SearchCommandParameter="{Binding Text, Source={x:Reference searchBar}}" />
			
			Do not forget name


Behaviors -> Select item
https://docs.microsoft.com/fr-fr/xamarin/xamarin-forms/app-fundamentals/behaviors/reusable/event-to-command-behavior

Runtime is nullabl

android:TabbedPage.ToolbarPlacement="Bottom"

Ajout du loader pour chargement des données
```
<ActivityIndicator
	Grid.Row="1"
	HorizontalOptions="CenterAndExpand"
	IsRunning="{Binding IsBusy}"
	IsVisible="{Binding IsBusy}"
	VerticalOptions="CenterAndExpand"
	Color="{StaticResource ThemeColor}" />
```


La comparaison de Show dans la couche service ne marche pas
public void DeleteItem(Show show)
        {
            var deletedShow = shows.FirstOrDefault(s => s.Id == show.Id);
            int index = shows.IndexOf(deletedShow);
            shows.RemoveAt(index);

            Save();
        } 		
xxxx


xwx

Large title


OnAppearing

IoC -> DependencyService -> FavoriteService

CollectionView aucune selection si SelectionMode not set to Single

