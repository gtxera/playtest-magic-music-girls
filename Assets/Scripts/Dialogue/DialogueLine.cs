using System;
using UnityEngine;

[Serializable]
public class DialogueLine
{
    [SerializeField]
    private DialogueCharacter _character;

    [SerializeField, TextArea]
    private string _text;

    public DialogueCharacter Character => _character;
    public string Text => _text;

    public float GetLineDuration(float charactersPerSecond) => _text.Length / charactersPerSecond;
}
