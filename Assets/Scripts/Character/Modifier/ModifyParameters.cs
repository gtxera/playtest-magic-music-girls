using System.Collections.Generic;

public class ModifyParameters
{
    private readonly Dictionary<string, object> _parameters;

    public ModifyParameters(params KeyValuePair<string, object>[] parameters)
    {
        _parameters = new(parameters);
    }

    public T Get<T>(string key)
    {
        return (T) _parameters[key];
    }
}
