using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace SoundCloudPodcast
{
    public class SoundCloudApi
    {
        private string Key { get; set; }
        private const string ApiBaseUrl = "https://api.soundcloud.com";

        public SoundCloudApi(string key)
        {
            Key = key;
        }

        public User GetUser(string user)
        {
            const string serviceUrl = "/users/{1}.json?consumer_key={0}";

            var url = string.Format(ApiBaseUrl + serviceUrl, Key, user);
            var jsonData = new System.Net.WebClient().DownloadString(url);
            var o = JObject.Parse(jsonData);

            return new User
                {
                    Permalink = (string)o["permalink"],
                    UserName = (string)o["username"],
                    PermalinkUrl = (string)o["permalink_url"],
                    AvatarUrl = (string)o["avatar_url"],
                    City = (string)o["city"],
                    Country = (string)o["country"],
                    Description = (string)o["description"],
                    FullName = (string)o["full_name"],
                    Id = (long)o["id"],
                    WebsiteTitle = (string)o["website_title"],
                    WebsiteUrl = (string)o["website"]
                };
        }

        public List<Track> GetTracks(string user)
        {
            const string serviceUrl = "/users/{1}/tracks.json?consumer_key={0}";

            var url = string.Format(ApiBaseUrl + serviceUrl, Key, user);
            var jsonData = new System.Net.WebClient().DownloadString(url);

            return (from t in JArray.Parse(jsonData)
                    select new Track
                        {
                            ArtworkUrl = (string) t["artwork_url"],
                            CreatedAt = (DateTime) t["created_at"],
                            Description = (string) t["description"],
                            Downloadable = (bool) t["downloadable"],
                            DownloadUrl = string.Format("{0}?consumer_key={1}", t["download_url"], Key),
                            Duration = (long) t["duration"],
                            Id = (long) t["id"],
                            OriginalFormat = (string) t["original_format"],
                            Permalink = (string) t["permalink"],
                            PermalinkUrl = (string) t["permalink_url"],
                            StreamUrl = string.Format("{0}?consumer_key={1}", t["stream_url"], Key),
                            Streamable = (bool) t["streamable"],
                            Title = (string) t["title"],
                            TrackType = (string) t["track_type"],
                            WaveformUrl = (string) t["waveform_url"]
                        }).ToList();
        }
    }
}
