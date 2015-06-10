using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            var jsonData = new WebClient().DownloadString(url);
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

        public List<Track> GetTracksRepostFromUserId(string userId)
        {
            // Undocumented, nice but could break at any moment
            const string serviceUrl = "https://api-v2.soundcloud.com/profile/soundcloud:users:{0}?limit=50&offset=0";

            var url = string.Format(serviceUrl, userId);
            var jsonData = new WebClient().DownloadString(url);

            return (from t in JToken.Parse(jsonData)["collection"] 
                    where t["type"].ToString().Contains("track")
                    select new Track()
                               {
                                   ArtworkUrl = (string)t["track"]["artwork_url"], 
                                   CreatedAt = (DateTime)t["track"]["created_at"], 
                                   Description = (string)t["track"]["description"], 
                                   Downloadable = (bool)t["track"]["downloadable"], 
                                   DownloadUrl = string.Format("{0}?consumer_key={1}", t["track"]["download_url"], this.Key), 
                                   Duration = (long)t["track"]["duration"], Id = (long)t["track"]["id"], 
                                   Permalink = (string)t["track"]["permalink"], 
                                   PermalinkUrl = (string)t["track"]["permalink_url"], 
                                   StreamUrl = string.Format("{0}?consumer_key={1}", t["track"]["stream_url"], this.Key), 
                                   Streamable = (bool)t["track"]["streamable"], Title = (string)t["track"]["title"], 
                                   TrackType = (string)t["track"]["track_type"], 
                                   WaveformUrl = (string)t["track"]["waveform_url"]
                               }).ToList();
        }

        public List<Track> GetTracksFromPlaylist(string playlistId, string secretToken = "")
        {
            const string serviceUrl = "/playlists/{1}.json?consumer_key={0}&secret_token={2}";
            var url = string.Format(ApiBaseUrl + serviceUrl, Key, playlistId, secretToken);
            var jsonData = new WebClient().DownloadString(url);

            return (from t in JObject.Parse(jsonData)["tracks"]
                    select new Track
                    {
                        ArtworkUrl = (string)t["artwork_url"],
                        CreatedAt = (DateTime)t["created_at"],
                        Description = (string)t["description"],
                        Downloadable = (bool)t["downloadable"],
                        DownloadUrl = string.Format("{0}?consumer_key={1}", t["download_url"], Key),
                        Duration = (long)t["duration"],
                        Id = (long)t["id"],
                        OriginalFormat = (string)t["original_format"],
                        Permalink = (string)t["permalink"],
                        PermalinkUrl = (string)t["permalink_url"],
                        StreamUrl = string.Format("{0}?consumer_key={1}", t["stream_url"], Key),
                        Streamable = (bool)t["streamable"],
                        Title = (string)t["title"],
                        TrackType = (string)t["track_type"],
                        WaveformUrl = (string)t["waveform_url"]
                    }).ToList();
        }

        public List<Track> GetTracksFromUser(string user)
        {
            const string serviceUrl = "/users/{1}/tracks.json?consumer_key={0}";

            var url = string.Format(ApiBaseUrl + serviceUrl, Key, user);
            var jsonData = new WebClient().DownloadString(url);

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
