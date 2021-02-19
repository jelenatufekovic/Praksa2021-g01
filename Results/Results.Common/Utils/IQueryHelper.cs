namespace Results.Common.Utils
{
    public interface IQueryHelper<T, K>
    {
        ISortHelper<T> Sort { get; }
        IFilterHelper<T, K> Filter { get; }
        IPagingHelper Paging { get; }
    }
}
