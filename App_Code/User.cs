using System;

namespace SoundCloudPodcast
{
    public class User
    {
        public long Id { get; set; }
        public string Permalink { get; set; }
        public string UserName { get; set; }
        public string PermalinkUrl { get; set; }
        public string Country { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public string WebsiteUrl { get; set; }
        public string WebsiteTitle { get; set; }
        public string AvatarUrl { get; set; }
    }
}
