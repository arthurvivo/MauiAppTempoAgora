using MauiAppTempoAgora.Models;
using MauiAppTempoAgora.Services;

namespace MauiAppTempoAgora
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        

        internal async void Button_Clicked_1(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txt_cidade.Text))
                {
                    Tempo? t = await DataService.GetPrevisao(txt_cidade.Text);

                    if (t != null)
                    {
                        lbl_res.Text = $"Cidade: {txt_cidade.Text}\n" +
                                       $"Temperatura mínima: {t.temp_min}°C\n" +
                                       $"Temperatura máxima: {t.temp_max}°C\n" +
                                       $"Visibilidade: {t.visibility} metros\n" +
                                       $"Velocidade do vento: {t.speed} m/s\n" +
                                       $"Latitude: {t.lat}°\n" +
                                       $"Longitude: {t.lon}°\n" +
                                       $"Condição do tempo: {t.main}\n" +
                                       $"Descrição: {t.description}\n" +
                                       $"Nascer do sol: {DateTimeOffset.FromUnixTimeSeconds(t.sunrise ?? 0).ToLocalTime()}\n" +
                                       $"Pôr do sol: {DateTimeOffset.FromUnixTimeSeconds(t.sunset ?? 0).ToLocalTime()}";
                    }
                    else
                    {
                        lbl_res.Text = "Não foi possível obter a previsão do tempo.";
                    }
                }
                else
                {
                    lbl_res.Text = "Preencha a cidade.";
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }
        }
    }
}
