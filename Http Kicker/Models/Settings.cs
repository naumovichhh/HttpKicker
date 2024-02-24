using System.ComponentModel.DataAnnotations;
using Http_Kicker.Validation;

namespace Http_Kicker.Models
{
    public class Settings
    {
        [IsURL]
        public string? URL { get; set; }
        
        public TimeSpan Interval { get; set; }  
    }
}
