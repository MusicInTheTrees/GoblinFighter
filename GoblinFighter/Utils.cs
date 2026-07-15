using System;

public class Utils
{
    public static int coerce(int value, int min, int max)
    {
        if (value < min)
        {
            return min;
        }
        else if (value > max)
        {
            return max;
        }
        else
        {
            return value;
        }
    }

    public static int coerceToMin(int value, int min)
    {
        return coerce(value, min, Int32.MaxValue);
    }
}
