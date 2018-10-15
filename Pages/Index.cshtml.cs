using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using CzechsInNHL.Models;

namespace CzechsInNHL.Pages
{
    public class IndexModel : PageModel
    {
        public async Task OnGetAsync()
        {
            var players = new List<Player>();

            foreach (var id in PlayerIDs._dict)
            {
                var playerId = id.Value;
                using (var client = new HttpClient())
                {
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.BaseAddress = new Uri("https://statsapi.web.nhl.com");

                var response = await client.GetAsync($"/api/v1/people/{playerId}?hydrate=stats(splits=statsSingleSeason)");
                response.EnsureSuccessStatusCode();

                var stringResult = await response.Content.ReadAsStringAsync();
                var responseToObjects = JsonConvert.DeserializeObject<RootObject>(stringResult);

                for (int i = 0; i < responseToObjects.people.Count-1; i++)
                {
                    var player = new Player();
                    player.fullName = responseToObjects.people[i].fullName;
                    player.primaryNumber = responseToObjects.people[i].primaryNumber;

                    players.Add(player);
                }                           

                }
            }

            
        }
    }
}
