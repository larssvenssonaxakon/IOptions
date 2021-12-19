using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace TestNet6.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {

        private readonly AppSettings _options;
        private readonly AppSettings _optionsSnapshot;
        private AppSettings _optionsMonitor;

        public TestController(IOptions<AppSettings> options, IOptionsSnapshot<AppSettings> optionsSnapshot, IOptionsMonitor<AppSettings> optionsMonitor)
        {
            _options = options.Value;
            _optionsSnapshot = optionsSnapshot.Value;
            _optionsMonitor = optionsMonitor.CurrentValue;
            optionsMonitor.OnChange(config => {
                _optionsMonitor = config;
            });
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new List<string>()
            {
                $"Option: {_options.SubSettings?.MySubSetting}",
                $"OptionSnapShot: {_optionsSnapshot.SubSettings?.MySubSetting}",
                $"OptionMonitor: {_optionsMonitor.SubSettings?.MySubSetting}",
                "Now put a breakpoint on _optionsMonitor = config; and change the MySubSetting value in the appsettings.development.json file! Note that the event might fire twice!"

            };
        }

        [HttpGet("asdf")]
        public IEnumerable<string> GetSubsection([FromServices] IOptions<SubSettings> options)
        {
            return new List<string>()
            {
                $"Option: {options.Value.MySubSetting}"
            };
        }
    }
}