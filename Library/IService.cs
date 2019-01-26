namespace Library
{
    public interface IService<TRequest, TResponse>
    {
        TResponse Invoke(TRequest request);
    }
}
