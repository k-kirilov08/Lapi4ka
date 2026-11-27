namespace Lapi4ka.Common.Describers
{
    using Microsoft.AspNetCore.Identity;

    public class BulgarianIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DefaultError()
            => new IdentityError { Description = "Възникна грешка." };

        public override IdentityError DuplicateEmail(string email)
            => new IdentityError { Description = $"Имейлът {email} вече се използва." };

        public override IdentityError DuplicateUserName(string userName)
            => new IdentityError { Description = $"Потребителското име {userName} вече се използва." };

        public override IdentityError InvalidEmail(string email)
            => new IdentityError { Description = "Невалиден имейл адрес." };

        public override IdentityError PasswordTooShort(int length)
            => new IdentityError { Description = $"Паролата трябва да съдържа поне {length} символа." };

        public override IdentityError PasswordRequiresDigit()
            => new IdentityError { Description = "Паролата трябва да съдържа поне една цифра." };

        public override IdentityError PasswordRequiresLower()
            => new IdentityError { Description = "Паролата трябва да съдържа поне една малка буква." };

        public override IdentityError PasswordRequiresUpper()
            => new IdentityError { Description = "Паролата трябва да съдържа поне една главна буква." };

        public override IdentityError PasswordRequiresNonAlphanumeric()
            => new IdentityError { Description = "Паролата трябва да съдържа поне един специален символ." };

        public override IdentityError PasswordMismatch()
            => new IdentityError { Description = "Грешна парола." };

        public override IdentityError UserNotInRole(string role)
            => new IdentityError { Description = $"Потребителят не е в роля {role}." };
    }
}
