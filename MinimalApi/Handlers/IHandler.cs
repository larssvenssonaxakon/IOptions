namespace MinimalApi.Handlers
{
    public interface IHandler<INPUT,RETURN> 
    {
        RETURN Handle(INPUT input);
    }
}