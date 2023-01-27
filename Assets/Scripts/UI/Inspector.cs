using System;
using UnityEngine;
using UnityEngine.UI;

public class Inspector : MonoBehaviour
{
    [SerializeField]
    InputField floatToBitsInput, floatToBitsOutput;

    [SerializeField]
    InputField bitsToFloatInput, bitsToFloatOutput;

    [SerializeField]
    InputField intCastToFloatInput, intCastToFloatOutput;

    [SerializeField]
    InputField floatCastToIntInput, floatCastToIntOutput;

    private void Update()
    {
        if (!string.IsNullOrEmpty(floatToBitsInput.text))
        {
            floatToBitsOutput.text = FloatTools.FloatToBits(float.Parse(floatToBitsInput.text)).ToString();
        }

        if (!string.IsNullOrEmpty(bitsToFloatInput.text))
        {
            bitsToFloatOutput.text = FloatTools.BitsToFloat(uint.Parse(bitsToFloatInput.text)).ToString();
        }

        if (!string.IsNullOrEmpty(intCastToFloatInput.text))
        {
            float f = int.Parse(intCastToFloatInput.text);

            uint bits = FloatTools.FloatToBits(f);

            intCastToFloatOutput.text = $"{bits} | {f}";
        }

        if (!string.IsNullOrEmpty(floatCastToIntInput.text))
        {
            float f = FloatTools.BitsToFloat(uint.Parse(floatCastToIntInput.text));

            int i = (int)f;

            string suffix = f > int.MaxValue || f < int.MinValue ? " (overflow)" : "";

            floatCastToIntOutput.text = $"(int){f} = {i}{suffix}";
        }
    }
}
