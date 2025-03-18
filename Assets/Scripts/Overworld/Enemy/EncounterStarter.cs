using System;
using UnityEngine;

public class EncounterStarter : MonoBehaviour
{
    [SerializeField]
    private EncounterData _encounterData;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;
        
        CombatManager.Instance.StartCombat(_encounterData);
    }
}
