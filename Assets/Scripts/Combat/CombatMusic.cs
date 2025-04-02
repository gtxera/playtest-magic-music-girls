using System.Collections;
using UnityEngine;

public class CombatMusic : SingletonBehaviour<CombatMusic>
{
    public void PlayInstrument(string instrument, float duration)
    {
        CombatAnimationsController.Instance.AddAnimation(duration);
        StartCoroutine(PlayInstrumentCoroutine(instrument, duration));
    }

    private IEnumerator PlayInstrumentCoroutine(string instrument, float duration)
    {
        yield return new WaitForSeconds(duration);
    }
}
