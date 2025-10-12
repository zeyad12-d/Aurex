using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurex_Core.DTO.EmployeeDtos
{
    public record UpdateEmployeeDto
    {
        public int Id { get; set; }
        public string Position { get; set; } = string.Empty;
        public DateTime HireDate { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public int Score { get; set; }
    }
}
