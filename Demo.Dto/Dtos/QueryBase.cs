namespace Demo.Dto.Dtos
{
    public class QueryBase
    {
        private const int MaxPageSize = 20;
        /// <summary>
        /// 页码
        /// </summary>
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 5;
        /// <summary>
        /// 每页显示数量
        /// </summary>
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        /// <summary>
        /// 排序字段
        /// </summary>
        public string OrderBy { get; set; }
        /// <summary>
        /// 查询关键字
        /// </summary>
        public string SearchTerm { get; set; }

    }
}
