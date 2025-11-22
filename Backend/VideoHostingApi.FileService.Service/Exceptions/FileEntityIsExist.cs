using VideoHostingApi.Common.Entities;

namespace VideoHostingApi.FileService.Service.Exceptions;

public class FileEntityIsExist(string message) : ExceptionBase(message);