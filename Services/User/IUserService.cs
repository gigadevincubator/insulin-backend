﻿using System.Collections.Generic;
using System.Threading.Tasks;
using insulin_backend.Database.Models;

namespace insulin_backend.Services.User
{
    public interface IUserService
    {
        IList<TutorialLanguage> GetAllUsersTutorials(int userId);
    }
}