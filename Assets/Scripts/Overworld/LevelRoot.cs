using System;
using UnityEngine;

public class LevelRoot : SingletonBehaviour<LevelRoot>
{
    public void Enable()
    {
        gameObject.SetActive(true);
        Input.Instance.SetInputContext(InputContext.Player);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
        Input.Instance.SetInputContext(InputContext.UI);
    }
}
