using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace OnlineMusicApp.Areas.Identity.Data;

// Add profile data for application users by adding properties to the OnlineMusicAppUser class
public class OnlineMusicAppUser : IdentityUser
{
    public static implicit operator OnlineMusicAppUser(Task<OnlineMusicAppUser> v)
    {
        throw new NotImplementedException();
    }
}

