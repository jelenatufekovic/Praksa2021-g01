namespace Results.Common.Utils.QueryHelpers
{
    public interface IQueryHelper<T, K>
    {
        IFilterHelper<T, K> Filter { get; }
        ISortHelper<T> Sort { get; }
        IPagingHelper Paging { get; }
    }
}
