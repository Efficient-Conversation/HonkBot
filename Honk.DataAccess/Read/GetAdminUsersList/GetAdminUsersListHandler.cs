using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Honk.Services;
using MediatR;

namespace Honk.DataAccess.Read.GetAdminUsersList;

public class GetAdminUsersListHandler : IRequestHandler<GetAdminUsersListRequest, List<string>?> 
{
    public async Task<List<string>?> Handle(GetAdminUsersListRequest notification, CancellationToken cancellationToken)
    {
        LogService.LogDebug("Entered GetAdminUsersListHandler");
        
        List<string>? adminUsers = null;
        if (File.Exists(DataAccessConfig.AdminUsers))
        {
            string adminUsersJson = await File.ReadAllTextAsync(DataAccessConfig.AdminUsers, cancellationToken);
            adminUsers = JsonSerializer.Deserialize<List<string>?>(adminUsersJson);
        }

        return adminUsers;
    }
}