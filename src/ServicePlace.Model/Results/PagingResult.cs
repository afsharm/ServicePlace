using ServicePlace.Model.Queries;

namespace ServicePlace.Model.Results;

public class PagingResult<T>
{
    public int TotalCount { get; set; }
    public required IEnumerable<T> Items { get; set; }
}