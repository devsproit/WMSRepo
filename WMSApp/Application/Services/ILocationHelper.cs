using Domain.Model;

namespace Application.Services;

public interface ILocationHelper
{
    bool DeleteLocationById(int Id);
    List<LocationDb> GetAllLocation();
    LocationDb GetLocationById(int Id);
    bool UpdateLocationById(LocationDb Location);
    bool CreateNewLocation(LocationDb Location);
}