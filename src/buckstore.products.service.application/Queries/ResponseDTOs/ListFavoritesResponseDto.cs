using System.Collections.Generic;
using buckstore.products.service.application.Queries.ViewModels;

namespace buckstore.products.service.application.Queries.ResponseDTOs
{
    public class ListFavoritesResponseDto
    {
        public IEnumerable<ListFavoritesVW> Favorites { get; set; }

        public ListFavoritesResponseDto(IEnumerable<ListFavoritesVW> favorites)
        {
            Favorites = favorites;
        }
    }
}
