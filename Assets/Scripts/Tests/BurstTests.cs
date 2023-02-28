using Unity.Mathematics;

public class BurstSin : SingleOperatorTest
{
    public override uint Operate(float f) => FloatTools.FloatToBits(math.sin(f));
}

public class BurstCos : SingleOperatorTest
{
    public override uint Operate(float f) => FloatTools.FloatToBits(math.cos(f));
}

public class BurstTan : SingleOperatorTest
{
    public override uint Operate(float f) => FloatTools.FloatToBits(math.tan(f));
}

public class BurstAsin : SingleOperatorTest
{
    public override uint Operate(float f) => FloatTools.FloatToBits(math.asin(f));
}

public class BurstAcos : SingleOperatorTest
{
    public override uint Operate(float f) => FloatTools.FloatToBits(math.acos(f));
}

public class BurstAtan : SingleOperatorTest
{
    public override uint Operate(float f) => FloatTools.FloatToBits(math.atan(f));
}