using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using WebApplication15.Shared;
using System.Net;
using System.Diagnostics;
namespace WebApplication15.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnvironmentVariableController : Controller                  
    {
        private readonly ILogger<EnvironmentVariableController> _logger;

        public EnvironmentVariableController(ILogger<EnvironmentVariableController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<EnVar> Get()
        {

            List<EnVar> envars = new List<EnVar>();
            if (!bool.TryParse(Environment.GetEnvironmentVariable("ISSHOWENVVAR"), out bool isshow)) { isshow = false; }

            if (isshow)
            {
                envars.Add(new EnVar { ID = "Process", Value = Process.GetCurrentProcess().ProcessName });

                string localIpAddress = string.Empty;

                var dns = Dns.GetHostName();
                var ips = Dns.GetHostAddressesAsync(dns).Result;

                foreach (IPAddress ip in ips)
                {
                    envars.Add(new EnVar { ID = "IP", Value = ip.ToString() });
                }
                envars.Add(new EnVar { ID = "DNS", Value = dns });

                foreach (DictionaryEntry de in Environment.GetEnvironmentVariables())
                {
                    envars.Add(new EnVar() { ID = de.Key.ToString(), Value = de.Value.ToString() });
                }
            }
            else
            {
                envars.Add(new EnVar{ ID = "nil", Value = "nil" });
            }
            return envars.ToArray();
        }
    }
}
