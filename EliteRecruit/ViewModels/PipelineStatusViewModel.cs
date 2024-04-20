using EliteRecruit.Models;

namespace EliteRecruit.ViewModels
{
    public class PipelineStatusViewModel
    {
        public PipelineStatusViewModel()
        {
            // Empty constructor.
        }

        public PipelineStatusViewModel(PipelineStatus pipelineStatus)
        {
            if (pipelineStatus != null)
            {
                contact = pipelineStatus.Contact;
                interview = pipelineStatus.Interview;
                offered = pipelineStatus.Offered;
                hired = pipelineStatus.Hired;
            }
        }
        public int Id { get; set; }
        public bool contact { get; set; }

        public bool interview { get; set; }

        public bool offered { get; set; }

        public bool hired { get; set; }

        public virtual PipelineStatus PipelineStatus { get; set; }
    }
}
