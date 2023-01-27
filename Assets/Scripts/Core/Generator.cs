using System.IO;
using UnityEngine;

public class Generator
{
    public static void GenerateInputsFile(string filename, int seed, int count)
    {
        FloatInputs inputs = new FloatInputs(seed);

        inputs.Insert(0x007fffff, "largest denormal");  // 1.17549421069e-38
        inputs.Insert(0x00001fff, "middle denormal");   // 1.14780357213e-41
        inputs.Insert(0x00000001, "smallest denormal"); // 1.40129846432e-45
        inputs.Insert(0x80000000, "signed zero");
        inputs.Insert(0x3f000000, "pointfive");
        inputs.Insert(0xbf800000, "negativeOne");
        inputs.Insert(0x3f800000, "one");
        inputs.Insert(0, "zero");
        inputs.Insert(FloatTools.FloatToBits(float.PositiveInfinity), "pos inf");
        inputs.Insert(FloatTools.FloatToBits(float.NegativeInfinity), "neg inf");

        inputs.AppendRandomFloats(count / 2, 10000);
        inputs.AppendRandomValues(count / 2);

        string floatInputsPath = Path.Combine(Application.streamingAssetsPath, filename);
        using var stream = new StreamWriter(floatInputsPath);

        inputs.Write(stream);
    }
}
