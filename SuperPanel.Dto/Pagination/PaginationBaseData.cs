namespace SuperPanel.Dto.Pagination
{
    public class PaginationBaseData
    {
        public int PageNumber { get; set; }
        public int RecordSize { get; set; }

        public PaginationBaseData(int currentPage, int recordLimit)
        {
            PageNumber = currentPage;
            RecordSize = recordLimit;
        }
    }
}
