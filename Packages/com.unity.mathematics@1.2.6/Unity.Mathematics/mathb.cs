using System.Runtime.CompilerServices;
using Unity.Burst;

[BurstCompile]
internal static class mathb
{
    [MethodImpl(MethodImplOptions.AggressiveInlining), BurstCompile(FloatMode = FloatMode.Deterministic)]
    public static float sin(float x)
    {
        return (float)System.Math.Sin(x);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining), BurstCompile(FloatMode = FloatMode.Deterministic)]
    public static float cos(float x)
    {
        return (float)System.Math.Cos(x);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining), BurstCompile(FloatMode = FloatMode.Deterministic)]
    public static float tan(float x)
    {
        return (float)System.Math.Tan(x);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining), BurstCompile(FloatMode = FloatMode.Deterministic)]
    public static float asin(float x)
    {
        return (float)System.Math.Asin(x);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining), BurstCompile(FloatMode = FloatMode.Deterministic)]
    public static float acos(float x)
    {
        return (float)System.Math.Acos(x);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining), BurstCompile(FloatMode = FloatMode.Deterministic)]
    public static float atan(float x)
    {
        return (float)System.Math.Atan(x);
    }
}
