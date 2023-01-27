using System;

public unsafe static class FloatTools
{
    public static float BitsToFloat(uint bits)
    {
        return *(float*)&bits;
    }

    public static uint FloatToBits(float f)
    {
        return *(uint*)&f;
    }

    public static bool CastToIntWillOverflow(float f)
    {
        return f > int.MaxValue || f < int.MinValue;
    }

    public static string BitsToVerboseString(uint bits)
    {
        return $"{BitsToFloat(bits)}f : {Convert.ToString(bits, 2).PadLeft(32, '0')} : {bits}";
    }
}
