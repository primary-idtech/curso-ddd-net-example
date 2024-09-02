using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Presentation.ViewModels
{
    /// <summary>
    /// Create Portfolio Request Model
    /// </summary>
    public class CreatePortfolioRequest
    {
        /// <summary>
        /// Name of the Portfolio
        /// </summary>
        /// <example>Cartera1</example>
        [Required]
        public required string Name { get; set; }
    }
}
