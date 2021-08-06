using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Capstone.DAO;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Controllers
{
    public abstract class ControllerBase : Controller
    {
        protected int GetUserId()
        {
            return int.Parse(this.User.Claims.First(i => i.Type == "sub").Value);
        }
    }
}