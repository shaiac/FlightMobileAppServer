using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightMobileApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightMobileApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class commandController : ControllerBase
    {
        private ICommandsManager commandsManager;

        public commandController(ICommandsManager commandsManager)
        {
            this.commandsManager = commandsManager;
        }

        // POST: api/command
        [HttpPost]
        public void Post([FromBody] Command value)
        {
            try
            {
                commandsManager.SetCommandValues(value);
            }
            catch (Exception)
            {
                //write the http response for error
            }
        }

        [HttpGet]
        public string Get()
        {
            return this.commandsManager.imgToAnd();
        }

    }
}
