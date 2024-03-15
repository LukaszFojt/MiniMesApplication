public interface IParameters
{
    int Id { get; set; }
    string Name { get; set; }
    string Unit { get; set; }
}

public interface IParametersRepository
{
    Task<List<IParameters>> GetAllParameters();
    Task AddParameters(IParameters parameters);
    Task DeleteParameters(int id);
    Task UpdateParameters(int id, IParameters updatedParametersData);
}
