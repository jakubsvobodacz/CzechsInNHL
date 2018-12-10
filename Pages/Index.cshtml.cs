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
using Microsoft.Extensions.Options;
using CzechsInNHL.Services;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace CzechsInNHL.Pages
{
    public class IndexModel : PageModel
    {
        private readonly string _connectionString;
        private readonly string _tableReference;

        public IndexModel(IOptions<Storage> storage )
        {
            _connectionString = storage.Value.StorageConnectionString;
            _tableReference = storage.Value.TableReference;
        }

        public async Task<List<PlayerSnapshot>> GetLastGameStatsAsync()
        {
            //Retrieve sotrage account
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_connectionString);

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            // Create the CloudTable object that represents the "players" table.
            CloudTable table = tableClient.GetTableReference(_tableReference);
            // Construct the query operation for all customer entities where PartitionKey="Smith".
            TableQuery<PlayerEntity> query = new TableQuery<PlayerEntity>();

            TableContinuationToken token = null;







            var result = table.ExecuteQuerySegmentedAsync(query, token);

            var _players = new List<PlayerSnapshot>();

            foreach (var id in PlayerIDs._dict)
            {
                var playerId = id.Value;
                using (var client = new HttpClient())
                {
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.BaseAddress = new Uri("https://statsapi.web.nhl.com");
                
                //Ask for season stats and profile info
                //var responseSeasonStats = await client.GetAsync($"/api/v1/people/{playerId}?hydrate=stats(splits=statsSingleSeason)");
                //responseSeasonStats.EnsureSuccessStatusCode();
                //var stringResultSeasonStats = await responseSeasonStats.Content.ReadAsStringAsync();
                //var responseSeasonStatsToObjects = JsonConvert.DeserializeObject<RootObject>(stringResultSeasonStats);

                //Ask for game log stats
                var responseGameLog = await client.GetAsync($"/api/v1/people/{playerId}?hydrate=stats(splits=gameLog)");
                responseGameLog.EnsureSuccessStatusCode();
                var stringResultGameLog = await responseGameLog.Content.ReadAsStringAsync();
                var responseToObjectsGameLog = JsonConvert.DeserializeObject<RootObjectGameLog>(stringResultGameLog);
                
                //Process Season Data
                foreach (var person in responseToObjectsGameLog.people)
                {
                    var player = new PlayerSnapshot();

                        //profile
                        player.fullName = person.fullName;
                        player.firstName = person.firstName;
                        player.lastName = person.lastName;
                        player.primaryNumber = person.primaryNumber;
                        player.currentAge = person.currentAge;
                        player.birthCity = person.birthCity;
                        player.height = person.height;
                        player.weight = person.weight * 0.45;
                        player.position = person.primaryPosition.name;
                        if (String.IsNullOrEmpty(person.currentTeam.name))
                        {
                            player.currentTeam = "There is a team missing for some reason";                          
                        }
                        else
                        {
                            player.currentTeam = person.currentTeam.name;
                        }
                       
                        //Season stats                    
                        foreach (var stat in person.stats)
                        {
                            var data = stat.splits.FirstOrDefault();
                            if (data != null)
                            {
                                player.goals = data.stat.goals;
                                player.assists = data.stat.assists;
                                player.plusMinus = data.stat.plusMinus;
                                player.hits = data.stat.hits;
                                player.timeOnIce = data.stat.timeOnIce;
                                player.penaltyMinutes = data.stat.penaltyMinutes;
                                player.lastGameDate = data.date;
                                player.shotPct = data.stat.shotPct;
                                player.lastOpponent = data.opponent.name;
                            }
                            else
                            {
                                player.goals = 0;
                                player.assists = 0;
                                player.plusMinus = 0;
                                player.hits = 0;
                                player.timeOnIce = "data nejsou k dispozici";
                                player.penaltyMinutes = "data nejsou k dispozici";
                                player.lastGameDate = "data nejsou k dispozici";
                                player.shotPct = 0;
                            }
                            

                            

                            //foreach (var split in stat.splits)
                            //{
                            //    player.goals = split.stat.goals;
                            //    player.assists = split.stat.assists;
                            //    player.plusMinus = split.stat.plusMinus;
                            //    player.hits = split.stat.hits;
                            //    player.timeOnIce = split.stat.timeOnIce;
                            //    player.penaltyMinutes = split.stat.penaltyMinutes;
                            //    player.lastGameDate = split.date;
                            //    player.shotPct = split.stat.shotPct;
                            //}
                        }
                        _players.Add(player);
                }                           

                }
            }
            return _players.OrderByDescending(x => x.lastGameDate).ToList();
        }

        public IList<PlayerSnapshot> Players { get; set; }

        public async Task OnGetAsync(string searchText)
        {

            if (!String.IsNullOrEmpty(searchText))
            {
                Players = await GetLastGameStatsAsync();
                Players = Players.Where(x => x.fullName.ToLower().Contains(searchText) || x.currentTeam.ToLower().Contains(searchText)).ToList();
                var a = 1;
            }
            else
            {
                Players = await GetLastGameStatsAsync();
            }
            
            
        }
    }
}
