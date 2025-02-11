using ServicePlace.Core.Queries;

namespace ServicePlace.Core.Results;

public class PagingResult<T>
{
    public int TotalCount { get; set; }
    public required IEnumerable<T> Items { get; set; }
}