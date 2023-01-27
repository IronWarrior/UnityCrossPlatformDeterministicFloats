using System.Collections.Generic;

public interface ITest
{
    string Name { get; }

    int Execute(IReadOnlyList<FloatInputs.Input> inputs, int[] indices, ref uint[] results);
}
