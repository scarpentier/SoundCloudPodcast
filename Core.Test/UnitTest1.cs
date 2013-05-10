using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SoundCloudPodcast.Core.Test
{
    [TestClass]
    public class UnitTest1
    {
        private readonly SoundCloudApi _api = new SoundCloudApi("b259bac51a32ebe4d1c4a3a380501d27");

        [TestMethod]
        public void GetTracks()
        {
            var tracks = _api.GetTracks("mikewardca");
            Assert.IsTrue(tracks.Count > 0);
        }
    }
}
