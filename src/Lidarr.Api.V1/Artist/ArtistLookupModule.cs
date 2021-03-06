using System.Collections.Generic;
using System.Linq;
using Lidarr.Http;
using Nancy;
using NzbDrone.Core.MediaCover;
using NzbDrone.Core.MetadataSource;

namespace Lidarr.Api.V1.Artist
{
    public class ArtistLookupModule : LidarrRestModule<ArtistResource>
    {
        private readonly ISearchForNewArtist _searchProxy;

        public ArtistLookupModule(ISearchForNewArtist searchProxy)
            : base("/artist/lookup")
        {
            _searchProxy = searchProxy;
            Get("/", x => Search());
        }

        private object Search()
        {
            var searchResults = _searchProxy.SearchForNewArtist((string)Request.Query.term);
            return MapToResource(searchResults).ToList();
        }

        private static IEnumerable<ArtistResource> MapToResource(IEnumerable<NzbDrone.Core.Music.Artist> artist)
        {
            foreach (var currentArtist in artist)
            {
                var resource = currentArtist.ToResource();
                var poster = currentArtist.Metadata.Value.Images.FirstOrDefault(c => c.CoverType == MediaCoverTypes.Poster);
                if (poster != null)
                {
                    resource.RemotePoster = poster.Url;
                }

                yield return resource;
            }
        }
    }
}
