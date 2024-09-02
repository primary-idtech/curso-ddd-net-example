using CleanArchitecture.Application.Shared.DTOs;
using CleanArchitecture.Domain.Models;
using CleanArchitecture.Presentation.ViewModels.Share;
using System.Linq;

namespace CleanArchitecture.Presentation.ViewModels
{
    /// <summary>
    /// Information result for search Portfolios.
    /// </summary>
    public class PortfolioPageResponse : PageResponse<PortfolioResponse>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dto">Page with portfolio items</param>
        public PortfolioPageResponse(PageDto<Portfolio> dto)
        {
            this.Items = dto.Items.Select(x => new PortfolioResponse(x));
            this.Offset = dto.Offset;
            this.Total = dto.Total;
            this.Limit = dto.Limit;
        }
    }
}
