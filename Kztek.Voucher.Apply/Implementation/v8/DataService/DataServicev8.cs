using IParkingv8.API.Interfaces;

namespace IParkingv8.API.Implementation.v8.DataService
{
    public class DataServicev8(IAuth auth) : IDataService
    {
        public IAuth Auth { get; set; } = auth;
        public IAccessKey AccessKey { get; set; } = new AccessKeyAPI(auth);
    }
}
