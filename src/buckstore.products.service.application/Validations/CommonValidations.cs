using System.Text.RegularExpressions;

namespace buckstore.products.service.application.Validations
{
    public static class CommonValidations
    {
        private static readonly Regex IsGuid = new Regex(
            @"^({){0,1}[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}(}){0,1}$",
            RegexOptions.Compiled);
        
        public static bool GuidValidator(string guidCheck)
        {
            return IsGuid.IsMatch(guidCheck);
        }
    }
}