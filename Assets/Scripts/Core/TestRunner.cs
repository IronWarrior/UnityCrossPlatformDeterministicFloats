using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class TestRunner
{
    private static readonly ITest[] singleOperandTests = new ITest[]
    {
        new Cast(),
        new MultiOperator(),

        new Sin(),
        new Cos(),
        new Tan(),
        new Asin(),
        new Acos(),
        new Atan(),

        new BurstSin(),
        new BurstCos(),
        new BurstTan(),
        new BurstAsin(),
        new BurstAcos(),
        new BurstAtan(),
    };

    private static readonly ITest[] doubleOperandTests = new ITest[]
    {
        new Add(), 
        new Subtract(), 
        new Multiply(), 
        new Divide(), 
        new MultiplyAdd(),
        new Equals(),
        new GreaterThan(), 
        new GreaterThanEquals(), 
        new LessThan(), 
        new LessThanEquals(),
    };

    public static ITest[] Tests => doubleOperandTests.Concat(singleOperandTests).ToArray();

    private bool writing => writer != null;

    private readonly TextReader reader;
    private readonly TextWriter writer;
    private readonly FloatInputs inputs;
    private readonly StringBuilder log = new StringBuilder();

    private readonly bool treatAllNaNAlike;
    private readonly string[] selectedTests;

    public long TestCount { get; private set; }
    public long ErrorCount { get; private set; }

    public Dictionary<ITest, long> ErrorCountByTest { get; private set; }

    public TestRunner(FloatInputs inputs, TextReader reader, string[] selectedTests, bool treatAllNaNAlike)
    {
        this.reader = reader;
        this.inputs = inputs;

        this.selectedTests = selectedTests;
        this.treatAllNaNAlike = treatAllNaNAlike;
    }

    public TestRunner(FloatInputs inputs, TextWriter writer)
    {
        this.writer = writer;
        this.inputs = inputs;
    }

    public string Execute(long errorMessageLimit)
    {
        ErrorCountByTest = new Dictionary<ITest, long>();

        uint[] testResultBuffer = new uint[100];
        int[] testInputIndicesBuffer = new int[2];

        bool IsTestSelected(string test)
        {
            return selectedTests.Where((t, _) => test == t).Count() > 0;
        }

        foreach (ITest test in singleOperandTests)
        {
            bool recordTest = true;

            if (writing)
            {
                writer.WriteLine(test.Name());
            }
            else
            {
                string testName = reader.ReadLine();
                recordTest = IsTestSelected(testName);
            }

            for (int i = 0; i < inputs.Inputs.Count; i++)
            {
                testInputIndicesBuffer[0] = i;

                int resultCount = test.Execute(inputs.Inputs, testInputIndicesBuffer, ref testResultBuffer);

                ProcessResults(test, 1, resultCount, recordTest);
            }
        }

        foreach (ITest test in doubleOperandTests)
        {
            bool recordTest = true;

            if (writing)
            {
                writer.WriteLine(test.Name());
            }
            else
            {
                string testName = reader.ReadLine();
                recordTest = IsTestSelected(testName);
            }

            for (int i = 0; i < inputs.Inputs.Count; i++)
            {
                for (int j = 0; j < inputs.Inputs.Count; j++)
                {
                    testInputIndicesBuffer[0] = i;
                    testInputIndicesBuffer[1] = j;

                    int resultCount = test.Execute(inputs.Inputs, testInputIndicesBuffer, ref testResultBuffer);

                    ProcessResults(test, 2, resultCount, recordTest);
                }
            }
        }

        void ProcessResults(ITest test, int inputCount, int resultCount, bool recordTest=true)
        {
            if (recordTest)
                TestCount++;

            for (int i = 0; i < resultCount; i++)
            {
                uint result = testResultBuffer[i];

                if (writing)
                {
                    writer.WriteLine(result);
                }
                else
                {
                    string truthString = reader.ReadLine();

                    if (!recordTest)
                        continue;

                    uint truthBits = uint.Parse(truthString);

                    bool pass = truthBits == result || (treatAllNaNAlike && float.IsNaN(FloatTools.BitsToFloat(result)) && float.IsNaN(FloatTools.BitsToFloat(truthBits)));

                    if (!pass)
                    {
                        if (ErrorCount < errorMessageLimit)
                        {
                            string message = $"{test.Name()}\n" +
                                $"result {FloatTools.BitsToVerboseString(result)}\n" +
                                $"truth {FloatTools.BitsToVerboseString(truthBits)}\n";

                            var inputA = inputs.Inputs[testInputIndicesBuffer[0]];
                            message += $"input {inputA.name} {FloatTools.BitsToVerboseString(inputA.bits)}\n";

                            if (inputCount > 1)
                            {
                                var inputB = inputs.Inputs[testInputIndicesBuffer[1]];
                                message += $"input {inputB.name} {FloatTools.BitsToVerboseString(inputB.bits)}\n";
                            }

                            log.AppendLine(message);
                        }

                        ErrorCount++;

                        if (!ErrorCountByTest.ContainsKey(test))
                            ErrorCountByTest[test] = 0;

                        ErrorCountByTest[test]++;
                    }
                }
            }
        }

        return log.ToString();
    }
}