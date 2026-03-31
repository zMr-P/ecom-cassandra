namespace ecom_cassandra.CrossCutting.Constants;

public class ErrorMessage
{
    public const string UserAlreadyExists = "User with the provided email already exists.";
    public const string InvalidEmail = "Provided email is not valid.";
    public const string InvalidPassword = "The password provided does not match the user";
    public const string UserNotFound = "User not found.";
    public const string Unauthorized = "Unauthorized access.";
}