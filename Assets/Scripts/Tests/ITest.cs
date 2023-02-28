using System.Collections.Generic;

public interface ITest
{
    int Execute(IReadOnlyList<FloatInputs.Input> inputs, int[] indices, ref uint[] results);
}

public static class ITestExtensions
{
    public static string Name(this ITest test)
    {
        return test.GetType().Name;
    }
}
