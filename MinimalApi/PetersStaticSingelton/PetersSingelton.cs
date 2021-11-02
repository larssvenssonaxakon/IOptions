using MinimalApi.Handlers;

namespace MinimalApi.PetersStaticSingelton
{
    public class PetersSingelton
    {
        private readonly IHandler<string, string> _testHandler;

        public PetersSingelton(IHandler<string, string> testHandler)
        {
            _testHandler = testHandler;
        }

        public string PetersNotStaticStaticMethod(string input)
        {
           return $"Peter non-static-static-singelton method calling: {_testHandler.Handle(input)}";
        }
    }
}
