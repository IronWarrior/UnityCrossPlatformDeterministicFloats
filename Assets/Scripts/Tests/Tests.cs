using System.Collections.Generic;

public abstract class OperatorTest : ITest
{
    public abstract string Name { get; }

    public abstract uint Operate(float a, float b);

    public int Execute(IReadOnlyList<FloatInputs.Input> inputs, int[] indices, ref uint[] results)
    {
        float a = inputs[indices[0]].Float;
        float b = inputs[indices[1]].Float;

        results[0] = Operate(a, b);

        return 1;
    }
}

public class Add : OperatorTest
{
    public override string Name => "Add";

    public override uint Operate(float a, float b)
    {
        float r = a + b;
        return FloatTools.FloatToBits(r);
    }
}

public class Subtract : OperatorTest
{
    public override string Name => "Subtract";

    public override uint Operate(float a, float b)
    {
        float r = a - b;
        return FloatTools.FloatToBits(r);
    }
}

public class Multiply : OperatorTest
{
    public override string Name => "Multiply";

    public override uint Operate(float a, float b)
    {
        float r = a * b;
        return FloatTools.FloatToBits(r);
    }
}

public class Divide : OperatorTest
{
    public override string Name => "Divide";

    public override uint Operate(float a, float b)
    {
        float r = a / b;
        return FloatTools.FloatToBits(r);
    }
}

public class MultiplyAdd : OperatorTest
{
    public override string Name => "MultiplyAdd";

    public override uint Operate(float a, float b)
    {
        float c = b * 2;
        a += (b * c);

        return FloatTools.FloatToBits(a);
    }
}

public class Equals : OperatorTest
{
    public override string Name => "Equals";

    public override uint Operate(float a, float b)
    {
        return a == b ? 1u : 0;
    }
}

public class NotEquals : OperatorTest
{
    public override string Name => "NotEquals";

    public override uint Operate(float a, float b)
    {
        return a != b ? 1u : 0;
    }
}

public class GreaterThan : OperatorTest
{
    public override string Name => "GreaterThan";

    public override uint Operate(float a, float b)
    {
        return  a > b ? 1u : 0;
    }
}

public class LessThan : OperatorTest
{
    public override string Name => "LessThan";

    public override uint Operate(float a, float b)
    {
        return a < b ? 1u : 0;
    }
}

public class GreaterThanEquals : OperatorTest
{
    public override string Name => "GreaterThanEquals";

    public override uint Operate(float a, float b)
    {
        return a >= b ? 1u : 0;
    }
}

public class LessThanEquals : OperatorTest
{
    public override string Name => "LessThanEquals";

    public override uint Operate(float a, float b)
    {
        return a <= b ? 1u : 0;
    }
}

// Casting to and from integer.
public class Cast : ITest
{
    public string Name => "Cast";

    public int Execute(IReadOnlyList<FloatInputs.Input> inputs, int[] indices, ref uint[] results)
    {
        float f = inputs[indices[0]].Float;

        if (float.IsNaN(f) || f > int.MaxValue || f < int.MinValue)
        {
            results[0] = 0;
            results[1] = 0;

            return 2;
        }

        int toInteger = (int)f;
        float toFloat = toInteger;

        results[0] = (uint)(toInteger + int.MaxValue);
        results[1] = FloatTools.FloatToBits(toFloat);

        return 2;
    }
}

// Not entirely sure my original logic behind this, probably just an insane smoke test???
public class MultiOperator : ITest
{
    public string Name => "MultiOperator";

    public int Execute(IReadOnlyList<FloatInputs.Input> inputs, int[] indices, ref uint[] results)
    {
        int a = indices[0];
        int b = (a + 1) % inputs.Count;
        int c = (a + 1) % inputs.Count;
        int d = (a + 1) % inputs.Count;

        float fa = inputs[a].Float;
        float fb = inputs[b].Float;
        float fc = inputs[c].Float;
        float fd = inputs[d].Float;

        float result = fa * fb + fa * fb - fd + fb / fc * fd - fa * fd + fc / fd
            + fc * fd + fc * fd - fb / fa;

        results[0] = FloatTools.FloatToBits(result);

        return 1;
    }
}


