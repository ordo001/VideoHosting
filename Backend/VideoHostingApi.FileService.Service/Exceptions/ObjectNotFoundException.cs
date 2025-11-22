using VideoHostingApi.Common.Entities;

namespace VideoHostingApi.FileService.Service.Exceptions;

public class ObjectNotFoundException(string message) : ExceptionBase(message);