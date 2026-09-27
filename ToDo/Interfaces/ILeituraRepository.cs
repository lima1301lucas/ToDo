namespace ToDo.Interfaces
{
    public interface ILeituraRepository<T>
    {
        Task<T?> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
    }
}