using System;
using MediatR;
using buckstore.products.service.application.Queries.ResponseDTOs;

namespace buckstore.products.service.application.Queries
{
    public class ListFavoritesByUserQuery : IRequest<ListFavoritesResponseDto>
    {
        public Guid UserId { get; set; }
    }
}
