using VideoHostingApi.Common.Entities;

namespace VideoHostingApi.Auth.Services.Contracts.Exceptions;

public class EntityIsExistException(string message) : ExceptionBase(message)
{
    
}