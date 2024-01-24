namespace Application.Mappers;

public interface IMapper<in TSource, out TDestination>
{
    TDestination Map(TSource from);
    //TSource MapReverse(TDestination from);
}