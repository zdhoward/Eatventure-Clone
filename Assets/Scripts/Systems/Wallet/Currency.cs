using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Currency
{
    string[] shortNotation = new string[12] { "", "k", "M", "B", "T", "Qa", "Qi", "Sx", "Sp", "Oc", "No", "Dc" };

    double value = 0;

    public Currency(double value)
    {
        this.value = value;
    }

    public string ToShortString()
    {
        // Load the current value
        string rawValueString = value.ToString("0");

        // Count how many digits are in the number
        int digits = rawValueString.Length;

        // Get the index value for the shortNotation
        int shortNotationIndex = (int)Mathf.Ceil(digits / 3f - 1f);

        string valueString = "";

        if (shortNotationIndex > 0)
        {
            int wholeNumberEndIndex = rawValueString.Length - shortNotationIndex * 3;
            string wholeValue = rawValueString.Substring(0, wholeNumberEndIndex);

            string decimalValue = rawValueString.Substring(wholeNumberEndIndex, 2).TrimEnd('0');

            if (decimalValue.Length > 0)
                decimalValue = "." + decimalValue;

            valueString = wholeValue + decimalValue;

            valueString = wholeValue;
        }
        else
        {
            valueString = rawValueString;
        }

        return valueString + shortNotation[shortNotationIndex];
    }

    public override string ToString()
    {
        // Load the current value
        string rawValueString = value.ToString("0");

        // Count how many digits are in the number
        int digits = rawValueString.Length;

        // Get the index value for the shortNotation
        int shortNotationIndex = (int)Mathf.Ceil(digits / 3f - 1f);

        string valueString = "";

        if (shortNotationIndex > 0)
        {
            int wholeNumberEndIndex = rawValueString.Length - shortNotationIndex * 3;
            string wholeValue = rawValueString.Substring(0, wholeNumberEndIndex);

            string decimalValue = rawValueString.Substring(wholeNumberEndIndex, 2).TrimEnd('0');

            if (decimalValue.Length > 0)
                decimalValue = "." + decimalValue;

            valueString = wholeValue + decimalValue;
        }
        else
        {
            valueString = rawValueString;
        }

        return valueString + shortNotation[shortNotationIndex];
    }

    public static Currency operator +(Currency a, Currency b)
    {
        return new Currency(a.value + b.value);
    }

    public static Currency operator -(Currency a, Currency b)
    {
        return new Currency(a.value - b.value);
    }

    public static bool operator >(Currency a, Currency b)
    {
        return a.value > b.value;
    }

    public static bool operator <(Currency a, Currency b)
    {
        return a.value < b.value;
    }

    public static bool operator >=(Currency a, Currency b)
    {
        return a.value >= b.value;
    }

    public static bool operator <=(Currency a, Currency b)
    {
        return a.value <= b.value;
    }

    public static bool operator ==(Currency a, Currency b)
    {
        return a.value == b.value;
    }

    public static bool operator !=(Currency a, Currency b)
    {
        return a.value != b.value;
    }

    public static Currency operator +(Currency a, double b)
    {
        return new Currency(a.value + b);
    }

    public static Currency operator -(Currency a, double b)
    {
        return new Currency(a.value - b);
    }

    public static bool operator >(Currency a, double b)
    {
        return a.value > b;
    }

    public static bool operator <(Currency a, double b)
    {
        return a.value < b;
    }

    public static bool operator >=(Currency a, double b)
    {
        return a.value >= b;
    }

    public static bool operator <=(Currency a, double b)
    {
        return a.value <= b;
    }

    public static bool operator ==(Currency a, double b)
    {
        return a.value == b;
    }

    public static bool operator !=(Currency a, double b)
    {
        return a.value != b;
    }
}
