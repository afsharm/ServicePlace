namespace ServicePlace.Model.Queries;


public abstract class PagingQueryBase
{
    public int PageNumber { set; get; }
    public int PageSize { set; get; }
    public string? SortField { set; get; }
    public SortOrder SortOrder { set; get; }
}

public enum SortOrder
{
    Asc = 0,
    Desc = 1
}