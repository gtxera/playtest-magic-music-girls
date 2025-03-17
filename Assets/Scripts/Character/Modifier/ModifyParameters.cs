using System.Collections.Generic;

public class ModifyParameters
{
    private readonly Dictionary<string, object> _parameters;

    public ModifyParameters(Dictionary<string, object> parameters)
    {
        _parameters = new Dictionary<string, object>(parameters);
    }

    public T Get<T>(string key)
    {
        return (T) _parameters[key];
    }
    
    public static implicit operator ModifyParameters(Dictionary<string, object> dictionary)
    {
        return new ModifyParameters(dictionary);
    }
}
