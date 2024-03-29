using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Mathematics;

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

    [MethodImpl(MethodImplOptions.AggressiveInlining), BurstCompile(FloatMode = FloatMode.Deterministic)]
    public static float atan2(float x, float y)
    {
        return (float)System.Math.Atan2(x, y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining), BurstCompile(FloatMode = FloatMode.Deterministic)]
    public static float sinh(float x)
    {
        return (float)System.Math.Sinh(x);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining), BurstCompile(FloatMode = FloatMode.Deterministic)]
    public static float cosh(float x)
    {
        return (float)System.Math.Cosh(x);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining), BurstCompile(FloatMode = FloatMode.Deterministic)]
    public static float tanh(float x)
    {
        return (float)System.Math.Tanh(x);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining), BurstCompile(FloatMode = FloatMode.Deterministic)]
    public static float pow(float x, float y)
    {
        return (float)System.Math.Pow(x, y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining), BurstCompile(FloatMode = FloatMode.Deterministic)]
    public static float exp(float x)
    {
        return (float)System.Math.Exp(x);
    }
}
