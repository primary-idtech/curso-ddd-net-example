using CleanArchitecture.Domain.Models;

namespace CleanArchitecture.Presentation.ViewModels
{
    /// <summary>
    /// Portfolio Response.
    /// </summary>
    /// <param name="entity"></param>
    public class PortfolioResponse(Portfolio entity)
    {

        /// <summary>
        /// Identifier of the Portfolio
        /// </summary>
        /// <example>1</example>
        public long Id { get; set; } = entity.Id;
        /// <summary>
        /// Name of the Portfolio
        /// </summary>
        /// <example>Cartera1</example>
        public string Name { get; set; } = entity.Name.Value;
        /// <summary>
        /// Enabled Portfolio
        /// </summary>
        /// <example>true</example>
        public bool Enabled { get; set; } = entity.Enabled;
    }
}
