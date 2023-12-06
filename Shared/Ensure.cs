using Microsoft.Extensions.FileSystemGlobbing;

namespace Polaris.Shared
{
    public class Ensure
    {
        // If the methods in your class are static, you can call them without creating an instance of the class.
        // Static methods belong to the class itself rather than to instances of the class.
        public static void NotNullOrContainWhiteSpace(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException("Tesssss arenott");
            }
        }
    }
}
