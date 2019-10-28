
using Newtonsoft.Json;

namespace University.Models

{
    public class CampusCareer
    {
        public int CampusID { get; set; }
        [JsonIgnore]
        public Campus Campus { get; set; }
        
        public int CareerID { get; set; }        
        public Career Career { get; set; }
    }
}