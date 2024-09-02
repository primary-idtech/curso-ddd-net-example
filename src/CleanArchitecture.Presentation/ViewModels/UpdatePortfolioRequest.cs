namespace CleanArchitecture.Presentation.ViewModels
{
    /// <summary>
    /// Portfolio Update Request Model
    /// </summary>
    public class UpdatePortfolioRequest
    {
        /// <summary>
        /// Optional new name of the Portfolio
        /// </summary>
        /// <example>New Name</example>
        public string? Name { get; set; }
        /// <summary>
        /// Attribute to change the status of the Portfolio
        /// </summary>
        /// <example>false</example>
        public bool Enabled { get; set; }
    }
}
