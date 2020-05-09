namespace core.seedwork
{
    public interface ISpecificationPaginate<T> : ISpecification<T>
    {
        bool Descedescending { get; }
        string Order { get; }
        int Page { get; set; }
        int PageSize { get; set; }
    }
}
