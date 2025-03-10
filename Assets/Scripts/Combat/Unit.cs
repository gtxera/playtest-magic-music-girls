using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    protected Character Character;

    public void TakeDamage(float damage)
    {
        Character.TakeDamage(damage);
    }

    public void Heal(float heal)
    {
        Character.Heal(heal);
    }
}
