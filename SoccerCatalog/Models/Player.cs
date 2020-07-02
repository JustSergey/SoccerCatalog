using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerCatalog.Models
{
    public class Player
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Имя не должно быть пустым")]
        [RegularExpression("^([А-Я]{1}[а-яё]{1,23}|[A-Z]{1}[a-z]{1,23})$", ErrorMessage = "Имя указано не верно")]
        [DisplayName("Имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Фамилия не должна быть пустой")]
        [RegularExpression("^([А-Я]{1}[а-яё]{1,23}|[A-Z]{1}[a-z]{1,23})$", ErrorMessage = "Фамилия указана не верно")]
        [DisplayName("Фамилия")]
        public string LastName { get; set; }

        [DisplayName("Пол")]
        public Gender Gender { get; set; }
        public int GenderId { get; set; }

        [Required(ErrorMessage = "Дата рождения не должна быть пустой")]
        [DisplayName("Дата рождения")]
        [ValidateDate(ErrorMessage = "Дата рождения указана не верно")]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        [DisplayName("Команда")]
        public Team Team { get; set; }
        public int TeamId { get; set; }

        [DisplayName("Страна")]
        public Country Country { get; set; }
        public int CountryId { get; set; }
    }
}
