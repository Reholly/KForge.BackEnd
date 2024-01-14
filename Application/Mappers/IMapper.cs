namespace Application.Mappers;

public interface IMapper<in TFromType, out TOType>
{
    TOType Map(TFromType from);
}