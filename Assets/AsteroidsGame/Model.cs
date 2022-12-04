public class Model<T> where T : new()
{
    public delegate void ChangedArgs(T model);
    public event ChangedArgs OnChanged;

    public void OnChangedInvoke(T model)
    {
        OnChanged?.Invoke(model);
    }
}
