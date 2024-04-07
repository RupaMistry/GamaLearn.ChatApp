namespace GamaLearn.ChatApp.Domain
{
    /// <summary>
    /// Represents User creation status
    /// </summary>
    public enum UserCreationEnum
    {
        UserNameExists = 1,
        EmailIDExists = 2,
        InvalidUserError = 3,
        UserCreated = 5
    }
}

