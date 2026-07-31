using eSport.UI.Shared.Infrastructure;

namespace eSport.UI.Shared.Models.Common
{
    public partial record PagingModel : BasePageableModel
    {
        public PagingModel() { }
        public PagingModel(int pageSize,
                           int currentPage,
                           int totalItems,
                           int pages)
        {
            PageSize = pageSize;
            PageNumber = currentPage;
            TotalItems = totalItems;
            TotalPages = pages;

        }

    }

}
