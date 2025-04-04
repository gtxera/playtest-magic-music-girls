using System;
using UnityEngine;

public enum ComboEmotion
{
    Happiness,
    Sadness,
    Anger,
    Love
}

public static class ComboEmotionExtensions
{
    public static string Traduzido(this ComboEmotion comboEmotion)
    {
        return comboEmotion switch
        {
            ComboEmotion.Happiness => "Felicidade",
            ComboEmotion.Sadness => "Tristeza",
            ComboEmotion.Anger => "Raiva",
            ComboEmotion.Love => "Amor",
            _ => throw new ArgumentOutOfRangeException(nameof(comboEmotion), comboEmotion, null)
        };
    }
}