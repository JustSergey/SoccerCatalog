using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerCatalog.Models
{
    public class Team
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Название не должно быть пустым")]
        [DisplayName("Название")]
        public string Title { get; set; }
    }
}
