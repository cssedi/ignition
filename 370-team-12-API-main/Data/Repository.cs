using BMWIgnition_API.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using BMWIgnition_API.Data;

namespace BMWIgnition_API.Model
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _appDbContext;

        public Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void AddDepartment(Department department)
        {
            _appDbContext.Departments.Add(department);
        }

        public void DeleteDepartment(Department department)
        {
            _appDbContext.Departments.Remove(department);
            _appDbContext.SaveChanges();
        }

        public async Task<Department[]> GetAllDepartmentsAsync()
        {
            IQueryable<Department> query = _appDbContext.Departments.Include(x => x.Function).ThenInclude(x => x.Departments).OrderBy(x=>x.DepartmentCode);

            return await query.ToArrayAsync();
        }

        public Department GetDepartmentById(int id)
        {
            return _appDbContext.Departments.Include(f => f.Function).Include(aa=> aa.AwardsArchitect).Where(x => x.DepartmentId == id).FirstOrDefault();
        }


        public bool SaveAll()
        {
            return _appDbContext.SaveChanges() > 0;
        }

        public Department UpdateDepartment(int id, DepartmentViewModel departmentViewModel)
        {
            var department = _appDbContext.Departments.Find(id);
            department.DepartmentCode = departmentViewModel.DepartmentCode;
            department.Id = departmentViewModel.awardsArchitectId;
            department.Name = departmentViewModel.Name;
            department.FunctionId = departmentViewModel.functionId;

            _appDbContext.SaveChanges();

            return department;
        }
        public async Task<Function[]> SearchFunctions(string searchTerm)
        {
            IQueryable<Function> query = _appDbContext.Functions.Where(f => f.FunctionCode.Contains(searchTerm) || f.Name.Contains(searchTerm));

            return await query.ToArrayAsync();
        }

        public void AddFunction(Function function)
        {
            _appDbContext.Functions.Add(function);
        }

        public void DeleteFunction(Function function)
        {
            _appDbContext.Functions.Remove(function);
            _appDbContext.SaveChanges();
        }

        public async Task<Function[]> GetAllFunctionsAsync()
        {
            IQueryable<Function> query = _appDbContext.Functions.Include(x => x.Departments);

            return await query.ToArrayAsync();
        }



        public Function GetFunctionById(int id)
        {
            return _appDbContext.Functions.Where(x => x.FunctionId == id).FirstOrDefault();
        }

        public Function UpdateFunction(int id, FunctionViewModel functionViewModel)
        {
            var function = _appDbContext.Functions.Find(id);
            function.FunctionCode = functionViewModel.FunctionCode;

            function.Name = functionViewModel.FunctionName;
            function.FunctionCode = functionViewModel.FunctionCode;


            _appDbContext.SaveChanges();

            return function;
        }

        public async Task<Department[]> Getfunctiondepartments(int functionID)
        {
            IQueryable<Department> departments = _appDbContext.Departments.Where(d => d.FunctionId == functionID);

            return await departments.ToArrayAsync();
        }

    }
}
