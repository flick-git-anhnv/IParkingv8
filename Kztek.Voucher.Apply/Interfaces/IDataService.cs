using IParkingv8.API.Implementation.v8.DataService;

namespace IParkingv8.API.Interfaces
{
    public interface IDataService
    {
        IAuth Auth { get; set; }
        IAccessKey AccessKey { get; set; }
    }
}
