using BMWIgnition_API.ViewModels;

namespace BMWIgnition_API.Model
{
    public interface IRepository
    {
        //Department
        Task<Department[]> GetAllDepartmentsAsync();
        Department GetDepartmentById(int id);
        Department UpdateDepartment(int id, DepartmentViewModel departmentViewModel);
        void DeleteDepartment(Department department);
        void AddDepartment(Department department);
        
        Task<Function[]> SearchFunctions(string searchTerm);

        //Function 
        Task<Function[]> GetAllFunctionsAsync();
        Function GetFunctionById(int id);
        Function UpdateFunction(int id, FunctionViewModel functionViewModel);
        void DeleteFunction(Function function);
        void AddFunction(Function function);
        Task<Department[]> Getfunctiondepartments(int functionID);

        bool SaveAll();
    }
}
