using CMS.Repositories.Interfaces.TMM;
using DataManager;
using DataManager.TMMDbClasses;

namespace CMS.Repositories.Repositories.TMM
{
    public class VideoConsulationRepository : Repository<VideoConsulation>, IVideoConsulationRepository
    {
        public VideoConsulationRepository(TMMDBContext tMMDBContext) : base(tMMDBContext) { }
    }
}
