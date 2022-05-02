using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using MusicWebApp.Models;
using Newtonsoft.Json;
using System.Diagnostics;

namespace MusicWebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        HttpClient httpClient;

        //static string BASE_URL = "https://shazam.p.rapidapi.com";
        //static string API_KEY = "0ed9d332eamsh35ac4c11651d85ep135980jsnf6c7e7e0e226"; //API Key goes here ""

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var userSession = HttpContext.Session.GetString("userLogin");

            var user = userSession != null ? JsonConvert.DeserializeObject<IdentityUser>(userSession) : null;

            return View(new AccountViewModel(user));
        }

        //public IActionResult Index()
        //{


        //    return View();
        //}

        public async Task<IActionResult> Songs()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> ArtistResults(UserInput uObj)
        {

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://spotify23.p.rapidapi.com/playlist_tracks/?id=5ABHKGoOzxkaa28ttQV9sE&offset=0&limit=99"),
                Headers =
                            {
                                { "X-RapidAPI-Host", "spotify23.p.rapidapi.com" },
                                { "X-RapidAPI-Key", "a2451f64c6msh61d5333f0b3fcf5p1f8adejsn9a93d8d553b6" },
                            },
            };

            string musicData = "";
            Tracks tracks = null;

            try
            {

                using (var response = await client.SendAsync(request))
                {

                    musicData = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                }

                if (!musicData.Equals(""))
                {
                    tracks = JsonConvert.DeserializeObject<Tracks>(musicData);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            if (uObj.Artist.ToLower() == "all")
            {
                ViewBag.Artist_Name = "ALL";
            }
            else
            {
                ViewBag.Artist_Name = uObj.Artist;

            }
            return View(tracks);
        }

        [HttpPost]
        public async Task<IActionResult> SongResults(UserInput uObj)
        {

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://spotify23.p.rapidapi.com/playlist_tracks/?id=5ABHKGoOzxkaa28ttQV9sE&offset=0&limit=50"),
                Headers =
                            {
                                { "X-RapidAPI-Host", "spotify23.p.rapidapi.com" },
                                { "X-RapidAPI-Key", "a2451f64c6msh61d5333f0b3fcf5p1f8adejsn9a93d8d553b6" },
                            },
            };

            string musicData = "";
            Tracks tracks = null;

            try
            {

                using (var response = await client.SendAsync(request))
                {

                    musicData = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                }

                if (!musicData.Equals(""))
                {
                    tracks = JsonConvert.DeserializeObject<Tracks>(musicData);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            if (uObj.Song.ToLower()=="all")
            {
                ViewBag.Song_Name = "ALL";
            }
            else {
                ViewBag.Song_Name = uObj.Song;

            }
            return View(tracks);
        }


        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact_Us()
        {

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}



