namespace NHManager.Blazor.Constants;

public static class Roles
{
    public const string Customer = "Customer";
    public const string Admin = "Admin";
    public const string Employee = "Employee";
    public const string SuperEmployee = "SuperEmployee";
    
    public const string AllEmployees = $"{Admin},{SuperEmployee},{Employee}";
    public const string AdminAndSuper = $"{Admin},{SuperEmployee}";
}
