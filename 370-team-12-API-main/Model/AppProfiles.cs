using BMWIgnition_API.ViewModels;
using AutoMapper;

namespace BMWIgnition_API.Model
{
    public class AppProfiles:Profile
    {
        public AppProfiles() {
            this.CreateMap<Department, DepartmentViewModel>().ReverseMap();
            this.CreateMap<Function, FunctionViewModel>().ReverseMap();
        }
    }
}
