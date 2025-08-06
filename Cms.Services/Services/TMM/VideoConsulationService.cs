using AutoMapper;
using Cms.Services.Extensions;
using Cms.Services.Filters.TMM;
using Cms.Services.Interfaces.TMM;
using Cms.Services.Models.TMMModals;
using CMS.Repositories.Interfaces.TMM;
using DataManager.TMMDbClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Services.Services.TMM
{
    public class VideoConsulationService : IVideoConsulationService
    {
        private readonly IVideoConsulationRepository _videoConsulationRepository;
        private readonly IMapper _mapper;
        public VideoConsulationService(IVideoConsulationRepository videoConsulationRepository, IMapper mapper)
        {
            _videoConsulationRepository = videoConsulationRepository ?? throw new ArgumentNullException(nameof(videoConsulationRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PaginatedList<VideoConsulationModal>> GetAllVideoConsulation(VideoConsulationFilter filter)
        {
            if (filter != null)
            {
                var result =
                _videoConsulationRepository.GetAll(deleted: false)
                .WhereIf(filter.Id > 0, x => x.ID == filter.Id)
                .WhereIf(filter.FromDate != null, x => x.Created_On >= filter.FromDate.Value)
                .WhereIf(filter.ToDate != null, x => x.Created_On <= filter.ToDate.Value)
                .WhereIf(!string.IsNullOrEmpty(filter.Date), x => x.ConsultationDate == DateTime.ParseExact(filter.Date, "MM-dd-yyyy", null, System.Globalization.DateTimeStyles.None))
                .WhereIf(!string.IsNullOrEmpty(filter.Email), x => EF.Functions.Like(x.Email, $"%{filter.Email}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.MobileNo), x => EF.Functions.Like(x.MobileNo, $"%{filter.MobileNo}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.Email, $"%{filter.Query}%"));
                if (filter.SortDescending != null && filter.SortDescending.Value)
                    result = result.OrderByDescending(x => x.ConsultationDate);
                if (filter.SortAscending != null && filter.SortAscending.Value)
                    result = result.OrderBy(x => x.ConsultationDate);
                else 
                    result = result.OrderByDescending(x => x.ID);
                    return await Task.FromResult(_mapper.Map<PaginatedList<VideoConsulation>, PaginatedList<VideoConsulationModal>>(new PaginatedList<VideoConsulation>(result.ToList
                        (), filter.PageSize, filter.PageNumber)));
            }
            else
            {
                var result =
                _videoConsulationRepository.GetAll(deleted: false);
                return await Task.FromResult(_mapper.Map<PaginatedList<VideoConsulation>, PaginatedList<VideoConsulationModal>>(new PaginatedList<VideoConsulation>(result, filter.PageSize, filter.PageNumber)));
            }
        }
        public async Task<PaginatedList<VideoConsulationModal>> GetUsersAllVideoConsulation(VideoConsulationFilter filter)
        {
            if (filter != null)
            {
                var result =
                _videoConsulationRepository.GetAll(deleted: false)
                .WhereIf(filter.Id > 0, x => x.ID == filter.Id)
                .WhereIf(filter.FromDate != null, x => x.ConsultationDate >= filter.FromDate.Value)
                .WhereIf(filter.ToDate != null, x => x.ConsultationDate <= filter.ToDate.Value)
// Pseudocode:
// - When comparing ConsultationDate with DateTime.Today, strip the time part from both sides.
// - Use DateTime.Date property for comparison in the WhereIf conditions.

.WhereIf(filter.Completed != null && filter.Completed.Value, x => x.ConsultationDate.HasValue && x.ConsultationDate.Value.Date < DateTime.Today.Date)
.WhereIf(filter.UpComing != null && filter.UpComing.Value, x => x.ConsultationDate.HasValue && x.ConsultationDate.Value.Date >= DateTime.Today.Date)
                .WhereIf(filter.CreatedBy != null || !string.IsNullOrEmpty(filter.Email),
                x => (filter.CreatedBy != null && x.CreatedBy == filter.CreatedBy) ||
                     (!string.IsNullOrEmpty(filter.Email) && EF.Functions.Like(x.Email, $"%{filter.Email}%")))
                .WhereIf(!string.IsNullOrEmpty(filter.MobileNo), x => EF.Functions.Like(x.MobileNo, $"%{filter.MobileNo}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.Email, $"%{filter.Query}%"));
                if (filter.SortDescending != null && filter.SortDescending.Value)
                    result = result.OrderByDescending(x => x.ConsultationDate);
                if (filter.SortAscending != null && filter.SortAscending.Value)
                    result = result.OrderBy(x => x.ConsultationDate);

                return await Task.FromResult(_mapper.Map<PaginatedList<VideoConsulation>, PaginatedList<VideoConsulationModal>>(new PaginatedList<VideoConsulation>(result.ToList
                    (), filter.PageSize, filter.PageNumber)));
            }
            else
            {
                var result =
                _videoConsulationRepository.GetAll(deleted: false);
                return await Task.FromResult(_mapper.Map<PaginatedList<VideoConsulation>, PaginatedList<VideoConsulationModal>>(new PaginatedList<VideoConsulation>(result, filter.PageSize, filter.PageNumber)));
            }
        }

        public async Task<VideoConsulationModal> GetById(int Id)
        {
            return _mapper.Map<VideoConsulation, VideoConsulationModal>(await _videoConsulationRepository.Entites().FirstOrDefaultAsync(x => x.ID == Id));

        }
    }
}
