namespace EliteRecruit.Models
{
    public class PipelineStatus
    {
        public PipelineStatus()
        {
            Contact = false;
            Interview = false;
            Offered = false;
            Hired = false;
        }
        public int Id { get; set; }

        public bool Contact { get; set; }

        public bool Interview { get; set; }

        public bool Offered { get; set; }

        public bool Hired { get; set; }
    }
}
