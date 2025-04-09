using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;


public class CardInfo
{
    public string Name { get; set; }
    public string Type { get; set; }
    public string Desc { get; set; }
}

class Program
{
    static async Task Main(string[] args)
    {
        try
        {
            
            using (HttpClient client = new HttpClient())
            {
                string apiUrl = "https://db.ygoprodeck.com/api/v7/cardinfo.php?fname=Exodia";


                
                HttpResponseMessage response = await client.GetAsync(apiUrl);

               
                response.EnsureSuccessStatusCode();

                
                string responseBody = await response.Content.ReadAsStringAsync();

                // Deserialize JSON into a CardInfo object
                var card = JsonConvert.DeserializeObject<CardInfo>(responseBody);

                // Output card information
                Console.WriteLine($"Card Name: {card.Name}");
                Console.WriteLine($"Description: {card.Desc}");
            }
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Request error: {e.Message}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Unexpected error: {e.Message}");
        }
    }
}
