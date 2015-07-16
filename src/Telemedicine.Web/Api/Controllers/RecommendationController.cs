using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Telemedicine.Core.Domain.Services;
using Telemedicine.Web.Api.Dto;

namespace Telemedicine.Web.Api.Controllers
{
    [RoutePrefix("api/v1/recommendations")]
    public class RecommendationController : ApiController
    {
        private readonly IRecommendationService _recommendationService;

        public RecommendationController(IRecommendationService recommendationService)
        {
            _recommendationService = recommendationService;
        }

        [Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var recommendation = await _recommendationService.GetByIdAsync(id);
            if (recommendation == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<RecommendationDto>(recommendation));
        }
    }
}
