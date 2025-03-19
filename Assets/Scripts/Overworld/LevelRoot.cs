using System;
using UnityEngine;

public class LevelRoot : SingletonBehaviour<LevelRoot>
{
    public void Enable()
    {
        enabled = true;
        Input.Instance.SetInputContext(InputContext.Player);
    }

    public void Disable()
    {
        enabled = false;
        Input.Instance.SetInputContext(InputContext.UI);
    }
}
