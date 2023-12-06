using Polaris.Shared;

namespace Polaris.Extensions.Guards
{
    public static class AgainstGuardClauseExtensions
    {
        public static void NullOrEmpty(this IAgainstGuardClause againstGuardClause, string? value)
        {
            if(string.IsNullOrEmpty(value))
            {
                throw new Exception("The value cannot be null or empty");
            }
        }

        public static void NullOrContainWhiteSpace(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException("Tesssss arenott");
            }
        }
    }
}
