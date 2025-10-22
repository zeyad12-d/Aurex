using System.ComponentModel.DataAnnotations;

namespace Aurex_Core.DTO.DepartmentDtos
{
    public record  CreateDepartmentDto
    {
        [Required(ErrorMessage ="Deparment Name Is Required ") , MaxLength(50,ErrorMessage ="Max Length Must be 50")]
        public string Name {  get; set; } = string.Empty;
    }
}
