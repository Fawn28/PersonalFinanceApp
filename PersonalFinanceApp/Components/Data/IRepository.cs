namespace PersonalFinanceApp.Components.Data;

public interface IRepository<T>
{
    List<T> Load();
    void Save(List<T> items);
    void Append(T item);
}