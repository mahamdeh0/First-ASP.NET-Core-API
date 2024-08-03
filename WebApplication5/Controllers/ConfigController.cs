using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FirstASP.NETCoreAPI.Controllers
{
    public class ConfigController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IOptionsMonitor<AttachmentOptions> _attachmentOptions;

        public ConfigController(IConfiguration configuration , IOptionsMonitor<AttachmentOptions> attachmentOptions)
        {
            this.configuration = configuration;
            this._attachmentOptions = attachmentOptions;
        }


        [HttpGet]
        [Route("")]

        public ActionResult GetConfig()
        {
            var config = new
            {
                AllowedHosts = configuration["AllowedHosts"],
                DefultConnection = configuration.GetConnectionString("DefultConnection"),
                DefaultLogLevel = configuration["Logging:LogLevel:Default"],
                TestKey = configuration["TestKey"],
                AttachmentOptions = _attachmentOptions.CurrentValue,
            };

            return Ok(config);  
        }
    }
}
