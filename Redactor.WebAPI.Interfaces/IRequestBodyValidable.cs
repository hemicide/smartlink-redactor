namespace Redactor.WebAPI.Interfaces
{
    public interface IRequestBodyValidable
    {
        public (bool ok, IDictionary<string, string[]> problems) Validate(IDictionary<string, object> args);
    }
}
