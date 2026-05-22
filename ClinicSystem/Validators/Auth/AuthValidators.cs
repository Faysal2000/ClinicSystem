
using ClinicSystem.DTOs.Auth;
using FluentValidation;
using LoginRequest = ClinicSystem.DTOs.Auth.LoginRequest;

namespace ClinicSystem.Validators.Auth
{
    public class RegisterRequestValidator : AbstractValidator<DTOs.Auth.RegisterRequest>
    {

        private static readonly string[] ValidRoles = { "Admin", "Doctor", "Receptionist" };

        public RegisterRequestValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full name is required.")
                .MaximumLength(100).WithMessage("Full name cannot exceed 100 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MaximumLength(50).WithMessage("Email cannot exceed 50 characters.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one digit.");

            RuleFor(x => x.Phone)
                .MaximumLength(20).WithMessage("Phone cannot exceed 20 characters.")
                .When(x => x.Phone != null);

            RuleFor(x => x.Address)
                .MaximumLength(100).WithMessage("Address cannot exceed 100 characters.")
                .When(x => x.Address != null);

            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Role is required.")
                .Must(r => ValidRoles.Contains(r))
                .WithMessage("Role must be Admin, Doctor, or Receptionist.");
        }

        public class LoginRequestValidator : AbstractValidator<LoginRequest>
        {
            public LoginRequestValidator()
            {
                RuleFor(x => x.Email)
                    .NotEmpty().WithMessage("Email is required.")
                    .EmailAddress().WithMessage("Invalid email format.");

                RuleFor(x => x.Password)
                    .NotEmpty().WithMessage("Password is required.");
            }
        }

    }
}

