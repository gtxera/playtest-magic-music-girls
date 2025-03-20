using System.Collections.Generic;
using UnityEngine;

public interface ITargetSelectionStrategy
{
    IEnumerable<Unit> GetSelection(IEnumerable<Unit> units, int targetCount);
}
