using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Tools.Auth;
public class AuthDbContext : IdentityDbContext<IdentityUser>
{
}
