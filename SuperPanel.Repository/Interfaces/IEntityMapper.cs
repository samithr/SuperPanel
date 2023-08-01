namespace SuperPanel.Repository.Interfaces
{
    public interface IEntityMapper
    {
        TDestination Map<TSource, TDestination>(TSource source);
    }
}
