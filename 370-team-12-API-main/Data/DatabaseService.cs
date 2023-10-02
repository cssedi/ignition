using Microsoft.EntityFrameworkCore;

namespace BMWIgnition_API.Data
{
    public class DatabaseService
    {
        private readonly AppDbContext _appDbContext;
        public DatabaseService(AppDbContext appDbContext)
        {
                _appDbContext = appDbContext;
        }
        public async void BackupDatabase(string backupFilePath)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.CancelAfter(TimeSpan.FromMinutes(30)); 
            await _appDbContext.Database.ExecuteSqlRawAsync($"BACKUP DATABASE [BMWIgnitionDB] TO DISK = '{backupFilePath}'", cancellationToken: cancellationTokenSource.Token);

        }

        public void RestoreDatabase(string backupFilePath)
        {
            try
            {
                // Switch to the master database
                _appDbContext.Database.ExecuteSqlRaw("USE master");

                //restore database
                _appDbContext.Database.ExecuteSqlRaw($"RESTORE DATABASE [BMWIgnitionDB] FROM DISK = '{backupFilePath}'");
            }
            catch (Exception ex) 
            {

            }
        }
    }
}
