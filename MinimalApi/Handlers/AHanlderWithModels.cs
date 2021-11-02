namespace MinimalApi.Handlers
{
    public class AHanlderWithModels : IHandler<CommandToHandle, ResponseToCommand>
    {
        public ResponseToCommand Handle(CommandToHandle input)
        {
            var responseToCommand = new ResponseToCommand()
            {
                ResponseString = $"handling models now: got this number in the input model: {input.InputInt}"
            };

            return responseToCommand;
       
        }
    }

    public class ResponseToCommand
    {
        public string? ResponseString { get; set; }
    }

    public class CommandToHandle
    {
        public int InputInt { get; set; }
    }
}
