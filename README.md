# Cours Xamarin.Forms pour les étudiants IMERIR (MAD)

## Librairies
- Xamarin.Essential: https://github.com/xamarin/Essentials
- MvvmLight: https://github.com/lbugnion/mvvmlight
- NewtonSoft: https://github.com/JamesNK/Newtonsoft.Json
- ImageCirclePlugin: https://github.com/jamesmontemagno/ImageCirclePlugin

## Getting started
### C#

### Xamarin
#### Localization
- Add a new .NET Standard Library project
- Choose .NET Standard 2.1 and named it
- Add a new Resource File (Localization.resx)
- In properties, change Custom Tool to PublicResXFileCodeGenerator
- In your main PCL, add a reference to you created project
- In Localization.resx add values as below
```
<data name="TestText">
	<value>Hello world.</value>
</data>
````
##### XAML
```
xmlns:resources="clr-namespace:tvshows.Strings;assembly=tvshows.Strings"

Text="{x:Static resources:Localization.TestText}"
```
##### Code
```
string text = Localization.TestText
```

#### Design data
- Add the namespace in your XAML file
```
xmlns:d="http://xamarin.com/schemas/2014/forms/design"

d:Text="Here is my design text"
```
## Architecture
- MVVM
- IoC
- Dependency Injection
- PCL
### Data Binding
https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/data-binding/

## Troubleshooting
- Android clear htttp -> true (Research)
https://medium.com/@son.rommer/fix-cleartext-traffic-error-in-android-9-pie-2f4e9e2235e6

- iOS: The resource could not be loaded because the App Transport Security policy requires the use of a secure connection.
https://forums.xamarin.com/discussion/99876/xamarin-ios-on-visual-studio-2017-requires-the-use-of-a-secure-connection

- Change Android status bar color
`Window.SetStatusBarColor(Color.ParseColor("#920000"));`

## Useful links
### Xamarin Forms documentation
https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/
### TVMaze API: 
http://www.tvmaze.com/api
#### Search a list of shows
http://api.tvmaze.com/search/shows?q=girls
#### Search a specific show
http://api.tvmaze.com/shows/1


- Création d'un projet Multiplatform -> Appp -> Blank Forms App -> App name -> Android/iOS / -> Use.NET Standard (not Shared Library)
- Mettre à jours les paquets
- Design de la vue 
- Binding context -> this


- Install MVVMLightLibs in all projects and inherits from ViewModelBase
Define 2 properties for the Text and one for the list
- Set binding context from MainPage

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


Large title


OnAppearing

IoC -> DependencyService -> FavoriteService

CollectionView aucune selection si SelectionMode not set to Single

