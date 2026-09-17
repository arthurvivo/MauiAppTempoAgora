using MauiAppTempoAgora.Models;
using Newtonsoft.Json.Linq;

namespace MauiAppTempoAgora.Services
{
    public class DataService
    {
        public static async Task <Tempo?> GetPrevisao(string cidade)
        {
            Tempo? t = null;
            string chave = "414c697a6ee3830e4014be1c2791ae3a"; 
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={cidade}&units=metric&appid={chave}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage resp = await client.GetAsync(url);

                if (resp.IsSuccessStatusCode)
                {
                    string json = await resp.Content.ReadAsStringAsync();

                    var rascunho = JObject.Parse(json);

                    DateTime time = new();
                    DateTime sunrise = time.AddSeconds(rascunho["sys"]["sunrise"].ToObject<int>()).ToLocalTime();
                    DateTime sunset = time.AddSeconds(rascunho["sys"]["sunset"].ToObject<int>()).ToLocalTime();

                    t = new Tempo()
                    {
                        lat = rascunho["coord"]["lat"].ToObject<double>(),
                        lon = rascunho["coord"]["lon"].ToObject<double>(),
                        description = rascunho["weather"][0]["description"].ToString(),
                        main = rascunho["weather"][0]["main"].ToString(),
                        temp_max = rascunho["main"]["temp_max"].ToObject<double>(),
                        temp_min = rascunho["main"]["temp_min"].ToObject<double>(),
                        speed = rascunho["wind"]["speed"].ToObject<int>(),
                        visibility = rascunho["visibility"].ToObject<int>(),
                        sunrise = rascunho["sys"]["sunrise"].ToObject<int>(),
                        sunset = rascunho["sys"]["sunset"].ToObject<int>()
                    }; // Fecha o objeto de tempo
                } //fecha if se o status do servidor for sucesso
            } // fecha o using 

            return t;
        }
    }
}
