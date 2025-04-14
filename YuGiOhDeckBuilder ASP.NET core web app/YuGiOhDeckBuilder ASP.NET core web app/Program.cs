using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

// CardInfo class to represent individual card 
public class CardInfo
{
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Desc { get; set; } = string.Empty;
}

// RootResponse class to represent API response 
public class RootResponse
{
    public List<CardInfo> Data { get; set; } = new List<CardInfo>();
}

// BasicsController class for game basics
public class BasicsController
{
    public List<string> GetYuGiOhBasics()
    {
        return new List<string>
        {
            "Life Points (LP): The player’s health in the game.",
            "Card HP and AP: Attributes that define a card’s strength.",
            "Graveyard: A space where used, destroyed, or discarded cards go.",
            "Game Modes: Single-player, multiplayer duels, or tournaments."
        };
    }
}

// Program class containing all methods
class Program
{
    static async Task Main(string[] args)
    {
        try
        {
            // Fetch card information
            await FetchCardInfo();

            // Display Yu-Gi-Oh Basics
            DisplayYuGiOhBasics();

            // Explain the base game and Exodia playstyle
            ExplainBaseGameAndExodia();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error occurred: {ex.Message}");
        }
    }

    // Get card info for starter deck
    static async Task FetchCardInfo()
    {
        try
        {
            using (HttpClient client = new HttpClient())
            {
                string apiUrl = "https://db.ygoprodeck.com/api/v7/cardinfo.php?fname=Exodia";

                // Make API call
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode(); // Throws exception for HTTP errors

                string responseBody = await response.Content.ReadAsStringAsync();

                // Deserialize API response
                var rootResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<RootResponse>(responseBody);

                // Null check and display logic
                if (rootResponse != null && rootResponse.Data != null && rootResponse.Data.Count > 0)
                {
                    Console.WriteLine("Card Information from API:");
                    foreach (var card in rootResponse.Data)
                    {
                        Console.WriteLine($"Card Name: {card.Name}");
                        Console.WriteLine($"Description: {card.Desc}");
                        Console.WriteLine("-----------------------------");
                    }
                }
                else
                {
                    Console.WriteLine("No data found in the API response.");
                }
            }
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Request error: {e.Message}");
        }
        catch (Newtonsoft.Json.JsonException e)
        {
            Console.WriteLine($"Deserialization error: {e.Message}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Unexpected error: {e.Message}");
        }
    }

    static void DisplayYuGiOhBasics()
    {
        BasicsController controller = new BasicsController();
        var basics = controller.GetYuGiOhBasics();

        Console.WriteLine("\nYu-Gi-Oh Basics:");
        foreach (var basic in basics)
        {
            Console.WriteLine(basic);
        }
    }

    // Explain the game
    static void ExplainBaseGameAndExodia()
    {
        Console.WriteLine("\n**Yu-Gi-Oh Base Game**");
        Console.WriteLine("The goal is to reduce your opponent's Life Points (LP) to 0 while protecting your own LP.");
        Console.WriteLine("Players use Monster, Spell, and Trap cards to attack, defend, and gain advantage.");
        Console.WriteLine("\nKey Concepts:");
        Console.WriteLine("- Life Points (LP): Your health. Starts at 8000 for each player.");
        Console.WriteLine("- Card HP and AP: Attack Points (AP) and Defense Points (HP) define a monster's strength.");
        Console.WriteLine("- Graveyard: Discard pile for used, destroyed, or discarded cards.");
        Console.WriteLine("- Game Modes: Single-player, multiplayer duels, or tournaments.");
        Console.WriteLine("\n**Exodia Playstyle: The '5 Cards to Win' Strategy**");
        Console.WriteLine("Exodia the Forbidden One consists of 5 cards:");
        Console.WriteLine("- Right Arm of the Forbidden One");
        Console.WriteLine("- Left Arm of the Forbidden One");
        Console.WriteLine("- Right Leg of the Forbidden One");
        Console.WriteLine("- Left Leg of the Forbidden One");
        Console.WriteLine("- Exodia the Forbidden One (head)");
        Console.WriteLine("Collect all five pieces in your hand to achieve an automatic win!");
        Console.WriteLine("\nTips for Playing Exodia:");
        Console.WriteLine("- Deck Optimization: Use cards to draw more (e.g., Pot of Greed).");
        Console.WriteLine("- Stalling: Use defensive cards like Swords of Revealing Light.");
        Console.WriteLine("- Protection: Shield Exodia pieces with spells and traps.");
    }
}
