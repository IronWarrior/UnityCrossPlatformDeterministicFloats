using Unity.Mathematics;

public class MathematicsSin : SingleOperatorTest
{
    public override uint Operate(float f) => FloatTools.FloatToBits(math.sin(f));
}

public class MathematicsCos : SingleOperatorTest
{
    public override uint Operate(float f) => FloatTools.FloatToBits(math.cos(f));
}

public class MathematicsTan : SingleOperatorTest
{
    public override uint Operate(float f) => FloatTools.FloatToBits(math.tan(f));
}

public class MathematicsAsin : SingleOperatorTest
{
    public override uint Operate(float f) => FloatTools.FloatToBits(math.asin(f));
}

public class MathematicsAcos : SingleOperatorTest
{
    public override uint Operate(float f) => FloatTools.FloatToBits(math.acos(f));
}

public class MathematicsAtan : SingleOperatorTest
{
    public override uint Operate(float f) => FloatTools.FloatToBits(math.atan(f));
}

public class MathematicsFloor : SingleOperatorTest
{
    public override uint Operate(float f) => FloatTools.FloatToBits(math.floor(f));
}

public class MathematicsCeil : SingleOperatorTest
{
    public override uint Operate(float f) => FloatTools.FloatToBits(math.ceil(f));
}

public class MathematicsRound : SingleOperatorTest
{
    public override uint Operate(float f) => FloatTools.FloatToBits(math.round(f));
}