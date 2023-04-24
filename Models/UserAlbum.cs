using OnlineMusicApp.Areas.Identity.Data;

namespace OnlineMusicApp.Models
{
    public class UserAlbum
    {
        public int Id { get; set; }

        public OnlineMusicAppUser MusicAppUser { get; set; }

        public string TrackId { get; set; }

        public string Name { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string ImageUrl { get; set; }
        public string PreviewUrl { get; set; }
    }
}
