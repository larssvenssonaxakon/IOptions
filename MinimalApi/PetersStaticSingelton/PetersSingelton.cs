using MinimalApi.Handlers;

namespace MinimalApi.PetersStaticSingelton
{
    public class PetersSingelton
    {
        private readonly ITestHandler _testHandler;

        public PetersSingelton(ITestHandler testHandler)
        {
            _testHandler = testHandler;
        }

        public string PetersNotStaticStaticMethod(string input)
        {
           return $"Peter non-static-static-singelton method calling: {_testHandler.Handle(input)}";
        }
    }
}
