namespace Web_search_job.DatabaseClasses.UserFolder
{
    public static class Consts
    {
        public const int UsernameMinLength = 5;

        public const string PasswordRegex = @"^(?=.*[A-Z])(?=.*[\W])(?=.*[0-9])(?=.*[a-z]).{6,128}$";

        public const string UsernameLengthValidationError = "Username повинен містити більше 5 символів.";
        public const string EmailValidationError = "Email повинен бути коректним";

        public const string PasswordValidationError = "Пароль повинен містити більше 6 символів, мін. 1 Великої, мін. 1 малої, мін. 1 спеціальний символ.";

        public class LoginProviders
        {
            public const string Google = "GOOGLE";
            public const string Facebook = "FACEBOOK";
            public const string Password = "PASSWORD";
        }
    }
}