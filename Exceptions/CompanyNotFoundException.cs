using Polaris.Exceptions.Base;

namespace Polaris.Exceptions
{
    public class CompanyNotFoundException: NotFoundException
    {
        int httpStatusCode { get; }
        public CompanyNotFoundException(string message) : base(message)
        {
        }
    }
}
