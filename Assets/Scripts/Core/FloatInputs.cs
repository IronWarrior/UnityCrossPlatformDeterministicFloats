using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

public class FloatInputs
{
    public struct Input
    {
        public uint bits;
        public string name;

        public float Float => FloatTools.BitsToFloat(bits);
    }

    public IReadOnlyList<Input> Inputs { get; private set; }

    private readonly List<Input> inputs = new List<Input>();
    private readonly int seed;
    private readonly Random random;

    public FloatInputs(int seed)
    {
        random = new Random(seed);

        this.seed = seed;

        Inputs = new ReadOnlyCollection<Input>(inputs);
    }

    public FloatInputs(StreamReader reader)
    {
        string[] seedSplit = reader.ReadLine().Split(':');

        seed = int.Parse(seedSplit[1]);

        while (!reader.EndOfStream)
        {
            string[] split = reader.ReadLine().Split(',');
            Insert(uint.Parse(split[0]), split[1]);
        }

        Inputs = new ReadOnlyCollection<Input>(inputs);
    }

    public void Insert(uint bits, string name)
    {
        inputs.Add(new Input { bits = bits, name = name });
    }

    public void Write(StreamWriter writer)
    {
        writer.WriteLine($"Random seed used to generate all values labeled random:{seed}");

        foreach (var input in inputs)
        {
            writer.WriteLine($"{input.bits},{input.name}");
        }
    }

    public void AppendRandomFloats(int count, float bound)
    {       
        for (int i = 0; i < count; i++)
        {
            float value = (float)((random.NextDouble() - 0.5) * bound);

            uint bits = FloatTools.FloatToBits(value);

            Insert(bits, $"random +/- {bound}");
        }
    }

    public void AppendRandomValues(int count)
    {
        for (int i = 0; i < count; i++)
        {
            uint bits = (uint)random.Next(-int.MaxValue, int.MaxValue);

            Insert(bits, "random bits");
        }
    }
}
