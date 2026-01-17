using iParkingv8.Object.Enums.ParkingEnums;
using iParkingv8.Object.Objects.Bases;
using iParkingv8.Object.Objects.Parkings;
using IParkingv8.API.Interfaces;
using IParkingv8.API.Objects;
using System.Runtime.CompilerServices;

namespace IParkingv8.API.Implementation.v8.DataService
{
    public interface IAccessKey
    {
        public IAuth Auth { get; set; }

        //CREATE
        Task<Tuple<AccessKey?, string>> CreateAsync(AccessKey accessKey, int timeOut = 10000, [CallerMemberName] string callerName = "",
                                                                       [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = "", bool isSaveLog = true);
        Tuple<AccessKey?, string> Create(AccessKey accessKey, int timeOut = 10000, [CallerMemberName] string callerName = "",
                                                                       [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = "", bool isSaveLog = true);

        //GET ALL
        Task<BaseReport<AccessKey>?> GetAllAsync(int timeOut = 10000, [CallerMemberName] string callerName = "", [CallerLineNumber] int lineNumber = 0,
                                                        [CallerFilePath] string filePath = "", bool isSaveLog = true, int pageIndex = 0, int pageSize = Filter.PAGE_SIZE);
        BaseReport<AccessKey>? GetAll(int timeOut = 10000, [CallerMemberName] string callerName = "", [CallerLineNumber] int lineNumber = 0,
                                                       [CallerFilePath] string filePath = "", bool isSaveLog = true, int pageIndex = 0, int pageSize = Filter.PAGE_SIZE);

        //GET BY CONDITION
        Task<BaseReport<AccessKey>?> GetByConditionAsync(string keyword, string collectionId, EmAccessKeyStatus? status, int timeOut = 10000, [CallerMemberName] string callerName = "", [CallerLineNumber] int lineNumber = 0,
                                                                     [CallerFilePath] string filePath = "", bool isSaveLog = true, int pageIndex = 0, int pageSize = Filter.PAGE_SIZE);
        BaseReport<AccessKey>? GetByCondition(string keyword, int timeOut = 10000, [CallerMemberName] string callerName = "", [CallerLineNumber] int lineNumber = 0,
                                                                    [CallerFilePath] string filePath = "", bool isSaveLog = true, int pageIndex = 0, int pageSize = Filter.PAGE_SIZE);


        Task<Tuple<AccessKey?, BaseErrorData?>?> GetByCodeAsync(string code, int timeOut = 10000, EmAccessKeyType accessKeyType = EmAccessKeyType.CARD,
                                                      [CallerMemberName] string callerName = "", [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = "", bool isSaveLog = true);
        Tuple<AccessKey?, BaseErrorData?>? GetByCode(string code, int timeOut = 10000, EmAccessKeyType accessKeyType = EmAccessKeyType.CARD,
                                                      [CallerMemberName] string callerName = "", [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = "", bool isSaveLog = true);

        //GET BY ID
        Task<Tuple<AccessKey?, string>> GetByIdAsync(string id, int timeOut = 10000, [CallerMemberName] string callerName = "", [CallerLineNumber] int lineNumber = 0,
                                                                          [CallerFilePath] string filePath = "", bool isSaveLog = true);
        Task<Tuple<AccessKey?, string>> GetById(string id, int timeOut = 10000, [CallerMemberName] string callerName = "", [CallerLineNumber] int lineNumber = 0,
                                                                          [CallerFilePath] string filePath = "", bool isSaveLog = true);

        //UPDATE
        Task<bool> UpdateCollectionAsync(string accessKeyId, string collectionId, int timeOut = 10000, [CallerMemberName] string callerName = "", [CallerLineNumber] int lineNumber = 0,
                                                [CallerFilePath] string filePath = "", bool isSaveLog = true);
        bool UpdateCollection(string accessKeyId, string collectionId, int timeOut = 10000, [CallerMemberName] string callerName = "", [CallerLineNumber] int lineNumber = 0,
                                              [CallerFilePath] string filePath = "", bool isSaveLog = true);
        //DELETE
        Task<bool> DeleteByIdAsync(string id, int timeOut = 10000, [CallerMemberName] string callerName = "", [CallerLineNumber] int lineNumber = 0,
                                                 [CallerFilePath] string filePath = "", bool isSaveLog = true);
        bool DeleteById(string id, int timeOut = 10000, [CallerMemberName] string callerName = "", [CallerLineNumber] int lineNumber = 0,
                                            [CallerFilePath] string filePath = "", bool isSaveLog = true);
    }
}
