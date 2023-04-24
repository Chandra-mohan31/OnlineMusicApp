using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using OnlineMusicApp.Areas.Identity.Data;
using OnlineMusicApp.Data;
using OnlineMusicApp.Migrations;
using OnlineMusicApp.Models;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;

namespace OnlineMusicApp.Controllers
{

    //declaring a class for data from API
    public class Track
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string ImageUrl { get; set; }
        public string PreviewUrl { get; set; }
    }


    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<OnlineMusicAppUser> userManager;
        private readonly OnlineMusicAppContext _context;



        //get tracks from API
        public async Task<List<Track>> SearchTracksOnSpotifyAsync(string keyword)
        {
            var client = new HttpClient();

            var clientId = "";
            var clientSecret = "";
            var encodedCredentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(clientId + ":" + clientSecret));

            var parameters = new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" }
            };

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encodedCredentials);

            var response1 = await client.PostAsync("https://accounts.spotify.com/api/token", new FormUrlEncodedContent(parameters));
            var tokenResponse = await response1.Content.ReadAsStringAsync();
            var accessToken = JObject.Parse(tokenResponse)["access_token"].ToString();

            string apiUrl = "https://api.spotify.com/v1/search?q=" + keyword + "&type=track";
            //string apiUrl = "https://api.spotify.com/v1/search?q=ranjithame&type=track&language=tamil";
            List<Track> tracks = new List<Track>();
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = await response.Content.ReadAsStringAsync();
                        JObject responseData = JObject.Parse(responseString);
                        JArray tracksData = (JArray)responseData["tracks"]["items"];
                        foreach (JObject trackData in tracksData)
                        {
                            Track track = new Track();
                            
                            track.Id = (string)trackData["id"];
                            track.Name = (string)trackData["name"];
                            track.Artist = (string)trackData["artists"][0]["name"];
                            track.Album = (string)trackData["album"]["name"];
                            track.ImageUrl = (string)trackData["album"]["images"][0]["url"];
                            track.PreviewUrl = (string)trackData["preview_url"];
                            Console.WriteLine(track.Id);
                            tracks.Add(track);
                        }
                    }
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine("Error searching for tracks on Spotify: " + ex.Message);
                }
            }
            return tracks;
        }

        public HomeController(ILogger<HomeController> logger,UserManager<OnlineMusicAppUser> userManager, OnlineMusicAppContext context)
        {
            _context = context;
            _logger = logger;
            this.userManager = userManager;
        }
       
        public IActionResult Index()
        {
            
           //get the tracks - default to tracks from anirudh
            Task<List<Track>> musicTracks = SearchTracksOnSpotifyAsync("anirudh");
            List<Track> trackList = musicTracks.GetAwaiter().GetResult();
            foreach (var item in trackList)
            {
                Console.WriteLine(item.Name);
                
            }
            ViewData["MusicTracks"] = trackList;
            var userId = userManager.GetUserId(this.User);
            Console.WriteLine("userid: " + userId);
            var user = userManager.FindByIdAsync(userId);

            ViewData["user"] = user;
            return View();
        }
        //public IActionResult Index(string trackId)
        //{
        //    ViewData["MSG"] = trackId;
        //    //get the tracks - default to tracks from anirudh
        //    Task<List<Track>> musicTracks = SearchTracksOnSpotifyAsync("anirudh");
        //    List<Track> trackList = musicTracks.GetAwaiter().GetResult();
        //    foreach (var item in trackList)
        //    {
        //        Console.WriteLine(item.Name);

        //    }
        //    ViewData["MusicTracks"] = trackList;
        //    return View();
        //}
        [HttpPost]
        public IActionResult Index(string query,string action)
        {
            Console.WriteLine(action);
            Console.WriteLine("Query : " + query);
            string Q = "";
            if (query == null || query.Length == 0)
            {
                Q = "happy";
            }
            else
            {
                Q = query;
            }
            Task<List<Track>> musicTracks = SearchTracksOnSpotifyAsync(Q);
            List<Track> trackList = musicTracks.GetAwaiter().GetResult();
            foreach (var item in trackList)
            {
                Console.WriteLine(item.Name);
            }
            ViewData["MusicTracks"] = trackList;
            var userId = userManager.GetUserId(this.User);
            Console.WriteLine("userid: " + userId);
            var user = userManager.FindByIdAsync(userId);

            ViewData["user"] = user;
            return View();
        }

        [HttpPost("AddFav")]
        public async Task<IActionResult> AddFav(string track_id,string track_name,string track_artist,string track_album,string track_image_url,string track_preview_url)
        {
            Console.WriteLine("post method");
            Console.WriteLine(track_id);
            Console.WriteLine(track_name);
            Console.WriteLine(track_album);
            Console.WriteLine(track_artist);
            Console.WriteLine(track_image_url);
            Console.WriteLine(track_preview_url);
            var userId = userManager.GetUserId(this.User);
            Console.WriteLine("userid: " + userId);
            var user = await userManager.FindByIdAsync(userId);
            
            UserAlbum userAlbum = new UserAlbum();
            userAlbum.MusicAppUser = user;
            userAlbum.TrackId = track_id;
            userAlbum.Name = track_name;
            userAlbum.Artist = track_artist;
            userAlbum.Album = track_album;
            userAlbum.ImageUrl = track_image_url;
            userAlbum.PreviewUrl = track_preview_url;
            _context.Add(userAlbum);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index","UserAlbums");
            
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