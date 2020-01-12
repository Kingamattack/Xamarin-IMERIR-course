- Création d'un projet Multiplatform -> Appp -> Blank Forms App -> App name -> Android/iOS / -> Use.NET Standard (not Shared Library)
- Mettre à jours les paquets
- Design de la vue 
- Binding context -> this
- Data Bidning 
- Android clear htttp -> true (Research)
https://medium.com/@son.rommer/fix-cleartext-traffic-error-in-android-9-pie-2f4e9e2235e6
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


