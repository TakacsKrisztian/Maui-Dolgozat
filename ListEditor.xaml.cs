using System.Collections.ObjectModel;

namespace Dolgozat;

public partial class ListEditor : ContentPage
{
    ObservableCollection<string> list = new ObservableCollection<string>();
    private readonly string WelcomeUser = MainPage.CurrentUser;

    public ListEditor()
	{
		InitializeComponent();
        WelcomeLabel.Text = $"Üdvözöljük {WelcomeUser}!";
        Adat.Text = "";
	}

    private void BtnFelvitel_Clicked(object sender, EventArgs e)
    {
        if (Adat.Text == "")
        {
            DisplayAlert("Hiba a felvitelben!", "Üres a felviteli mezõ!", "OK");
        }
        else
        {
            list.Add(Adat.Text);
            ListaElemek.ItemsSource = list;
        }
    }

    private void BtnMent_Clicked(object sender, EventArgs e)
    {
        try
        {
            var docsDirectory = Android.App.Application.Context.GetExternalFilesDir(Android.OS.Environment.DirectoryDocuments);
            File.WriteAllLines($"{docsDirectory.AbsoluteFile.Path}/lista.txt", list);
            DisplayAlert("Sikeres mentés!", "", "OK");
        }
        catch (Exception ex)
        {
            DisplayAlert("Hiba!", ex.Message, "Cancel");
        }
    }

    private void BtnBetolt_Clicked(object sender, EventArgs e)
    {
        var docsDirectory = Android.App.Application.Context.GetExternalFilesDir(Android.OS.Environment.DirectoryDocuments);
        var filePath = System.IO.Path.Combine(docsDirectory.AbsolutePath, "lista.txt");
        if (System.IO.File.Exists(filePath))
        {
            string[] lines = System.IO.File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                list.Add((string)line);
            }
            ListaElemek.ItemsSource = list;
        }
        else
        {
            DisplayAlert("Hiba!", "A fájl nem létezik!", "OK");
        }
    }

    private void BtnKijTorl_Clicked(object sender, EventArgs e)
    {
#pragma warning disable CS0168
        try
        {
            list.RemoveAt(list.IndexOf(ListaElemek.SelectedItem.ToString()));
        }
        catch (Exception ObjectNullReferenceException)
        {
            DisplayAlert("Hiba!", "Nincs honnan elemet kitörölni!", "OK");
        }
#pragma warning restore CS0168
    }

    private void BtnFullTorl_Clicked(object sender, EventArgs e)
    {
        list.Clear();
    }

    private void BtnBeszur_Clicked(object sender, EventArgs e)
    {
#pragma warning disable CS0168
        try
        {
            list.Insert(list.IndexOf(ListaElemek.SelectedItem.ToString()), Adat.Text);
            ListaElemek.ItemsSource = list;
        }
        catch (Exception ArgumentOutOfRangeException)
        {
            DisplayAlert("Hiba!", "Nincs hova elemet beszúrni!", "OK");
        }
#pragma warning restore CS0168
    }
}