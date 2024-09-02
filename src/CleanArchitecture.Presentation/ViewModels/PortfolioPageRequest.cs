using CleanArchitecture.Presentation.ViewModels.Share;

namespace CleanArchitecture.Presentation.ViewModels
{
    /// <summary>
    /// Portfolio Page Request Model
    /// </summary>
    public class PortfolioPageRequest : PageRequest
    {
        /// <summary>
        /// Enabled Portfolios. Default is true.
        /// </summary>
        /// <example>true</example>
        public bool? Enabled { get; set; }
    }
}
