using Newtonsoft.Json;
using Redactor.Application.Interfaces;
using Redactor.Extensions;

namespace Redactor.Application.Validators
{
    public class DateRangeValidator : IRequestBodyValidable
    {
        private static string _field = "dateRange";

        public static string Predicate
        {
            get { return _field; }
            private set { }
        }

        public (bool, IDictionary<string, string[]>) Validate(IDictionary<string, object> args)
        {
            var validateProblems = new Dictionary<string, List<string>>();
            Func<(bool, IDictionary<string, string[]>)> result = () => (validateProblems.Count == 0, validateProblems.ToDictionary(k => k.Key, v => v.Value.ToArray()));

            if (!args.TryGetValue(_field, out var value))
            {
                //validateProblems.GetOrAdd(_field, []).Add($"Field \"{_field}\" is required");
                return result();
            }

            DateRange? dateRange;
            try { dateRange = JsonConvert.DeserializeObject<DateRange>(value.ToString()); }
            catch (Exception ex)
            {
                validateProblems.GetOrAdd(_field, []).Add(ex.Message);
                return result();
            }

            if (dateRange?.Begin > dateRange?.End)
                validateProblems.GetOrAdd(_field, []).Add($"Begin datetime \"{dateRange.Begin}\" cannot be less than end datetime \"{dateRange.End}\"");

            return result();
        }
    }

    [Serializable]
    internal class DateRange
    {
        [JsonProperty("begin")]
        public DateTime? Begin { get; set; }

        [JsonProperty("end")]
        public DateTime? End { get; set; }
    }
}
