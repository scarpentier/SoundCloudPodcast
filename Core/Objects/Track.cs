using System;

namespace SoundCloudPodcast.Core.Objects
{
    public class Track
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public long Duration { get; set; }
        public bool Streamable { get; set; }
        public bool Downloadable { get; set; }
        public string Permalink { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string TrackType { get; set; } // TODO: Enum
        public string OriginalFormat { get; set; }
        public string PermalinkUrl { get; set; }
        public string ArtworkUrl { get; set; }
        public string WaveformUrl { get; set; }
        public string StreamUrl { get; set; }
        public string DownloadUrl { get; set; }
    }
}
