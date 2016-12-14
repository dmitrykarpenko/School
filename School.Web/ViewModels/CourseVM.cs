using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace School.Web.ViewModels
{
    public class CourseVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        //ICollection<int>
        //public IEnumerable<SelectListItem> IdsOfSelectedGroups { get; set; } = new List<SelectListItem>();
        //public MultiSelectList AvailableGroups { get; set; }
        public IEnumerable<SelectableGroupVM> SelectableGroups { get; set; }
    }
}
