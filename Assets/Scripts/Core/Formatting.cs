using UnityEngine;

public class Formatting
{
    public static string GetFloatText(float value)
    {
        if (value < 100f)
        {
            var padded = Mathf.Floor(value * 100);
            padded = padded / 100;

            return padded.ToString("F1");
        }

        return Mathf.FloorToInt(value).ToString();
    }
}
