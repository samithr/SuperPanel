using System.Collections.Generic;

namespace SuperPanel.Dto.Pagination
{
    public class PaginationResponse<TEntity>
    {
        public int CurrentPage { get; set; }
        public bool IsLastPage { get; set; }
        public int TotalPage { get; set; }
        public string ErrorMessage { get; set; }
        public IEnumerable<TEntity> Data { get; set; }
    }
}
