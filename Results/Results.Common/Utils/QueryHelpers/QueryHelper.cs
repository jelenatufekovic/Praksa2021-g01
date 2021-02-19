namespace Results.Common.Utils.QueryHelpers
{
    public class QueryHelper<T, K> : IQueryHelper<T, K>
    {
        private ISortHelper<T> _sortHelper;
        private IFilterHelper<T, K> _filterHelper;
        private IPagingHelper _pagingHelper;

        public ISortHelper<T> Sort
        {
            get
            {
                if (_sortHelper == null)
                {
                    _sortHelper = new SortHelper<T>();
                }
                return _sortHelper;
            }
        }

        public IFilterHelper<T, K> Filter
        {
            get
            {
                if (_filterHelper == null)
                {
                    _filterHelper = new FilterHelper<T, K>();
                }
                return _filterHelper;
            }
        }

        public IPagingHelper Paging
        {
            get
            {
                if(_pagingHelper == null)
                {
                    _pagingHelper = new PagingHelper();
                }
                return _pagingHelper;
            }
        }
    }
}
