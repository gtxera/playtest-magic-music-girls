using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Combo", menuName = "Scriptable Objects/Combo")]
public class Combo : ScriptableObject
{
    [SerializeField]
    private ComboEmotion _firstEmotion;

    [SerializeField]
    private ComboEmotion _secondEmotion;

    [SerializeField]
    private ComboEmotion _thirdEmotion;

    [SerializeField]
    private ComboEmotion _fourthEmotion;

    [field: SerializeField]
    public Skill Skill { get; private set; }

    private Dictionary<ComboEmotion, int> _comboDictionary;

    private IReadOnlyDictionary<ComboEmotion, int> ComboDefinition
    {
        get
        {
            if (_comboDictionary != null)
                return _comboDictionary;

            _comboDictionary = new Dictionary<ComboEmotion, int>();
            InitializeOrAdd(_comboDictionary, _firstEmotion);
            InitializeOrAdd(_comboDictionary, _secondEmotion);
            InitializeOrAdd(_comboDictionary, _thirdEmotion);
            InitializeOrAdd(_comboDictionary, _fourthEmotion);

            return _comboDictionary;
        }
    }

    public bool Matches(IReadOnlyDictionary<ComboEmotion, int> emotions)
    {
        return emotions.Count == ComboDefinition.Count && !emotions.Except(ComboDefinition).Any();
    }

    private static void InitializeOrAdd(Dictionary<ComboEmotion, int> dictionary, ComboEmotion emotion)
    {
        if (!dictionary.TryAdd(emotion, 1))
            dictionary[emotion] += 1;
    }
}
