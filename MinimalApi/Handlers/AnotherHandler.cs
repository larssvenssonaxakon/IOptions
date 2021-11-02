namespace MinimalApi.Handlers
{
    public class AnotherHandler : IHandler<int, string>
    {
        public string Handle(int input)
        {
            return $"Got an int, returning a string. This was the int btw: {input}";
        }
    }
}
