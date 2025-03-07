using System.Collections.ObjectModel;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Scriptable Objects/Dialogue")]
public class Dialogue : ScriptableObject
{
    [SerializeField]
    private DialogueLine[] _lines;

    [SerializeReference]
    private IEvent _event;

    public ReadOnlyCollection<DialogueLine> Lines => new (_lines);
}
