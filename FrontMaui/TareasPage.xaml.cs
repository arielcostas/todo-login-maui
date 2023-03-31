using FrontMaui.Models;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace FrontMaui;

public partial class TareasPage : ContentPage
{
    public ObservableCollection<Tarea> Tareas { get; set; }

    public TareasPage()
    {
        InitializeComponent();
        Tareas = new();
        BindingContext = this;
        LoadTareasAsync();
    }

    private async Task LoadTareasAsync()
    {
        try
        {
            HttpClient cli = new();
            cli.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Sesion.Token);

            var tareas = await cli.GetFromJsonAsync<Tarea[]>("http://localhost:5072/Tareas");
            Tareas.Clear();
            foreach (var tarea in tareas)
            {
                Tareas.Add(tarea);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert(Title, ex.Message, "Ok");
        }
    }
}

