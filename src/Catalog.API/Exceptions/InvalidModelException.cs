namespace Catalog.API.Exceptions
{
    public class InvalidModelException : Exception
    {
        public IDictionary<string, string[]> Errors { get; }

        public InvalidModelException(IDictionary<string, string[]> errors) : base("One or more validation errors occurred.")
        {
            Errors = errors;
        }
    }
}
