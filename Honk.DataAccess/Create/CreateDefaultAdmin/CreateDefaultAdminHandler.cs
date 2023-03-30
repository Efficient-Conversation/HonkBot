using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Honk.DataAccess.Read.GetAdminUsersList;
using Honk.Services;
using Honk.Services.Models.Enums;
using MediatR;

namespace Honk.DataAccess.Create.CreateDefaultAdmin;

public class CreateDefaultAdminHandler : INotificationHandler<CreateDefaultAdminNotification>
{
    private readonly IMediator _mediator;
    
    public CreateDefaultAdminHandler(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public async Task Handle(CreateDefaultAdminNotification notification, CancellationToken cancellationToken)
    {
        LogService.LogDebug("Entered CreateDefaultAdminHandler");
        
        string? ownerId = ConfigurationService.GetConfigValue(ConfigKey.OWNER_ID);
        List<string>? adminUsers = await _mediator.Send(new GetAdminUsersListRequest(), cancellationToken);

        // Both null
        if (ownerId == null && adminUsers == null)
        {
            LogService.LogWarning($"No {ConfigKey.OWNER_ID} specified in environment and no admin users have been setup previously.");
            return;
        }

        // Owner ID null
        if (ownerId == null)
        {
            LogService.LogInfo($"No {ConfigKey.OWNER_ID} specified in environment, assuming saved admins.");
            return;
        }
        
        // Admin Users null
        if (adminUsers == null)
        {
            LogService.LogInfo("No admin users previously specified, generating new Admin Users config.");
            adminUsers = new();
        }
        
        // Neither null
        if (adminUsers.Contains(ownerId))
        {
            LogService.LogInfo($"Discord User ID '{ownerId}' is already an admin.");
        }
        else
        {
            LogService.LogInfo($"Added Discord User ID '{ownerId}' to list of admin users.");
            adminUsers.Add(ownerId);

            string adminUsersJson = JsonSerializer.Serialize(adminUsers);
            await File.WriteAllTextAsync(DataAccessConfig.AdminUsers, adminUsersJson, cancellationToken);
        }

    }
}
