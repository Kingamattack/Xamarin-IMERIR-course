- Création d'un projet Multiplatform -> Appp -> Blank Forms App -> App name -> Android/iOS / -> Use.NET Standard (not Shared Library)
- Mettre à jours les paquets
- Design de la vue 
- Binding context -> this
- Data Bidning 
- Android clear htttp -> true (Research)
https://medium.com/@son.rommer/fix-cleartext-traffic-error-in-android-9-pie-2f4e9e2235e6

- iOS: The resource could not be loaded because the App Transport Security policy requires the use of a secure connection.
https://forums.xamarin.com/discussion/99876/xamarin-ios-on-visual-studio-2017-requires-the-use-of-a-secure-connection

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


DataBinding

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



