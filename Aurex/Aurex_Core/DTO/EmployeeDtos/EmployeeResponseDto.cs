using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurex_Core.DTO.EmployeeDtos
{
    public record EmployeeResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public DateTime HireDate { get; set; }
        public string ImageUrl { get; set; } = string.Empty;

        public int Score { get; set; } 

        public string DepartmentName { get; set; } = string.Empty;
    }
}
