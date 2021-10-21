namespace MinimalApi.Handlers
{
    public class TestHandler : ITestHandler
    {
        private readonly ILogger<TestHandler> _logger;

        public TestHandler(ILogger<TestHandler> logger)
        {
            _logger = logger;
        }

        public string Handle(string name)
        {
            _logger.LogInformation("Handler recieved call!");
            return $"Hello {name} from handler!";
        }
    }
}
