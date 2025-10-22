using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurex_Core.DTO.DepartmentDtos
{
    public record  UpdateDepartmentDto
    {
        
        public int Id { get; set; }
        [Required (ErrorMessage ="Name is Required")]
        public string Name { get; set; } = string.Empty;
    }
}
