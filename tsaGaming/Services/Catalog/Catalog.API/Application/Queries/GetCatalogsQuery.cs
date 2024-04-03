using Services.Common.Dto;

namespace Catalog.API.Application.Queries
{
    public class GetCatalogsQuery : PagingableQuery, IRequest<IList<CatalogDTO>>
    {
        public GetCatalogsQuery() { }
    }
}
