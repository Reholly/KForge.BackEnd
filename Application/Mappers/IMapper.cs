namespace Application.Mappers;

public interface IMapper<TFromType, TOType>
{
    TOType Map(TFromType from);
    TFromType MapReverse(TFromType dest, TOType src);
}