using TestTask.Application.Dto.PassportsDto;


namespace TestTask.Application.Dto.EmployeesDto
{
    public class CreateEmployeeDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public int CompanyId { get; set; }
        public int DepartmentId { get; set; }
        public CreatePassportDto Passport { get; set; }
    }
}
