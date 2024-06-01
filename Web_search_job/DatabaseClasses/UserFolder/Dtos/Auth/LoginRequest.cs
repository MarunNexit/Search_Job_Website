﻿using System.ComponentModel.DataAnnotations;

namespace Web_search_job.DatabaseClasses.UserFolder.Dtos.Auth
{
    public class LoginRequest
    {
        [MinLength(Consts.UsernameMinLength, ErrorMessage = Consts.UsernameLengthValidationError)]
        public string? Username { get; set; }

        [RegularExpression(Consts.PasswordRegex, ErrorMessage = Consts.PasswordValidationError)]
        public string? Password { get; set; }
    }
}
