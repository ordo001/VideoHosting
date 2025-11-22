using VideoHostingApi.Common.Entities;

namespace VideoHostingApi.FileService.Service.Exceptions;

public class FileEntityNotFoundException(string message) : ExceptionBase(message);