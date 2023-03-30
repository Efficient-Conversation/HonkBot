using System.Collections.Generic;
using MediatR;

namespace Honk.DataAccess.Read.GetAdminUsersList;

public class GetAdminUsersListRequest : IRequest<List<string>?>
{
    
}