using System.ComponentModel.DataAnnotations;

namespace Http_Kicker.Validation
{
    public class IsURLAttribute : ValidationAttribute
    {
        public IsURLAttribute()
        {
            ErrorMessage ??= "Not a valid URL";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            Uri? uriResult;
            bool result = Uri.TryCreate((string)(value ?? ""), UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (result)
                return ValidationResult.Success;
            else
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }
    }
}
