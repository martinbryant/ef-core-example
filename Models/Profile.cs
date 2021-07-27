using Microsoft.EntityFrameworkCore;

namespace ef_core_example.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Profile
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}