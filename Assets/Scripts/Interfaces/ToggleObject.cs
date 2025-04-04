using UnityEngine;

public class ToggleObject : MonoBehaviour
{
    [SerializeField]
    private GameObject _target;

    public void Toggle()
    {
        _target.SetActive(!_target.activeInHierarchy);
    }
}
