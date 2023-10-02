using BMWIgnition_API.ViewModels;

namespace BMWIgnition_API.Data
{
    public interface IChallengerAuthRepository
    {
        public Task<UserManagertReponse> LogginChallengerAsync(LoginViewModel loginViewModel);
        public Task<UserManagertReponse> LogginRewardArchitectAsync(LoginViewModel model);
    }
}
