using UnityEngine;

public static class LevelCalculator
{
    private const float BASE_XP_REQUIREMENT = 100f;

    private const float XP_REQUIREMENT_INCREASE_FACTOR = 1.33f;

    public static float GetXpRequirementFor(int level)
    {
        return BASE_XP_REQUIREMENT * Mathf.Pow(XP_REQUIREMENT_INCREASE_FACTOR, level - 1);
    }
}
