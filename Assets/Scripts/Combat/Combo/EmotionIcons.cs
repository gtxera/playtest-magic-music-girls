using AYellowpaper.SerializedCollections;
using UnityEngine;

public class EmotionIcons : PersistentSingletonBehaviour<EmotionIcons>
{
    [SerializeField]
    private SerializedDictionary<ComboEmotion, Sprite> _emotionImages;

    public Sprite GetEmotionIcon(ComboEmotion emotion)
    {
        return _emotionImages[emotion];
    }
}
