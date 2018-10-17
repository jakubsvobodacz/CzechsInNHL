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

        public async Task<List<Player>> GetStatsAsync()
        {
            var _players = new List<Player>();

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

                foreach (var person in responseToObjects.people)
                {
                    var player = new Player();
                    player.fullName = person.fullName;
                    player.primaryNumber = person.primaryNumber;

                    _players.Add(player);
                }                           

                }
            }
            return _players;
        }

        public IList<Player> Players { get; set; }

        public async Task OnGetAsync()
        {
            Players = await GetStatsAsync();
            
        }
    }
}
