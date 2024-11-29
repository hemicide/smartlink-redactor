using Redactor.Application.Interfaces;

namespace Redactor.Application.Validators
{
    public class DeviceValidator : IRequestBodyValidable
    {

        private static string _field = "device";

        public static string Predicate
        {
            get { return _field; }
            private set { }
        }

        public (bool, IDictionary<string, string[]>) Validate(IDictionary<string, object> args)
        {
            var validateProblems = new Dictionary<string, List<string>>();
            Func<(bool, IDictionary<string, string[]>)> result = () => (validateProblems.Count == 0, validateProblems.ToDictionary(k => k.Key, v => v.Value.ToArray()));

            if (args.TryGetValue(_field, out var value))
            {

            }
            else
            {
                //validateProblems.GetOrAdd(_field, []).Add($"Argument \"{_field}\" is required");
            }
            return result();
        }
    }
}
