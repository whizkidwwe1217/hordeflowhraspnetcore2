using HordeFlow.HR.Infrastructure.Models;
using HordeFlow.HR.Repositories.Interfaces;

namespace HordeFlow.HR.Repositories
{
    public interface ICompanyRepository: IRepository<Company> {}
    public interface IEmployeeRepository : IRepository<Employee> {}
    public interface IUserRepository : IRepository<User> {}
    public interface ITeamRepository: IRepository<Team> {}
    public interface IDepartmentRepository: IRepository<Department> {}
    public interface IDesignationRepository: IRepository<Designation> {}
    public interface IAddressRepository: IRepository<Address> {}
    public interface IStateRepository: IRepository<State> {}
    public interface ICountryRepository: IRepository<Country> {}
    public interface IEmployeeAddressRepository: IRepository<EmployeeAddress> {}
    public interface ICompanyAddressRepository: IRepository<CompanyAddress> {}
}