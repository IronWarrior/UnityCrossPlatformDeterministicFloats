using System.Collections.Generic;
using UnityEngine;

public abstract class SingleOperatorTest : ITest
{
    public abstract string Name { get; }

    public abstract uint Operate(float f);

    public int Execute(IReadOnlyList<FloatInputs.Input> inputs, int[] indices, ref uint[] results)
    {
        float f = inputs[indices[0]].Float;

        results[0] = Operate(f);

        return 1;
    }
}

// Unity's Mathf just casts things to and from double, so not really the best test.
public class Sin : SingleOperatorTest
{
    public override string Name => "Sin";

    public override uint Operate(float f) => FloatTools.FloatToBits(Mathf.Sin(f));
}

public class Cos : SingleOperatorTest
{
    public override string Name => "Cos";

    public override uint Operate(float f) => FloatTools.FloatToBits(Mathf.Cos(f));
}

public class Tan : SingleOperatorTest
{
    public override string Name => "Tan";

    public override uint Operate(float f) => FloatTools.FloatToBits(Mathf.Tan(f));
}

public class Asin : SingleOperatorTest
{
    public override string Name => "ASin";

    public override uint Operate(float f) => FloatTools.FloatToBits(Mathf.Asin(f));
}

public class Acos : SingleOperatorTest
{
    public override string Name => "Acos";

    public override uint Operate(float f) => FloatTools.FloatToBits(Mathf.Acos(f));
}

public class Atan : SingleOperatorTest
{
    public override string Name => "Atan";

    public override uint Operate(float f) => FloatTools.FloatToBits(Mathf.Atan(f));
}


