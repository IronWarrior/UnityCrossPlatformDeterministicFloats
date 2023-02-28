using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ManualTester : MonoBehaviour
{
    [SerializeField]
    InputField testNameInput, testBitsInputA, testBitsInputB, testOutput;

    private readonly int[] indices = new int[] { 0, 1 };
    private uint[] results = new uint[2];

    private readonly List<FloatInputs.Input> inputs = new List<FloatInputs.Input>(2);

    private void Awake()
    {
        inputs.Add(default);
        inputs.Add(default);
    }

    private void Update()
    {
        if (!string.IsNullOrEmpty(testNameInput.text))
        {
            var test = TestRunner.Tests.Where(t => t.Name().ToUpper() == testNameInput.text.ToUpper()).FirstOrDefault();

            if (test != null)
            {
                uint a = string.IsNullOrEmpty(testBitsInputA.text) ? 0 : uint.Parse(testBitsInputA.text);
                uint b = string.IsNullOrEmpty(testBitsInputB.text) ? 0 : uint.Parse(testBitsInputB.text);

                inputs[0] = new FloatInputs.Input() { bits = a };
                inputs[1] = new FloatInputs.Input() { bits = b };

                int count = test.Execute(inputs, indices, ref results);

                string result = "";

                for (int i = 0; i < count; i++)
                {
                    result += FloatTools.BitsToVerboseString(results[i]) + "\n\n";
                }

                testOutput.text = result;
            }
        }
    }
}
