using System;
using System.Text.RegularExpressions;

namespace CleanArchitecture.Domain.Models
{
    public partial class PortfolioName
    {
        public string Value { get; }
        
        private PortfolioName(string value)
        {
            Value = value;
        }

        public static PortfolioName Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new BusinessException("Name is required.");
            if (!NameValidationRegex().IsMatch(name))
                throw new BusinessException("Name contains invalid characters.");
            return new PortfolioName(name);
        }

        [GeneratedRegex(@"^[a-zA-Z0-9 ]*$")]
        private static partial Regex NameValidationRegex();
    }
}
