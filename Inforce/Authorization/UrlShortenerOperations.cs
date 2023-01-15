using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Inforce.Authorization;

public class Constants
{
    public static readonly string CreateOperationName = "Create";
    public static readonly string ReadOperationName = "Read";
    public static readonly string UpdateOperationName = "Update";
    public static readonly string DeleteOperationName = "Delete";

    public static readonly string ShortenerAdministratorRole = "ShortenerAdmin";
}

