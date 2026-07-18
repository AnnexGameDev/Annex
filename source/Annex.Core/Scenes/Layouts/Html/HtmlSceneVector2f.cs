using Annex.Core.Data;
using Scaffold.Extensions;

namespace Annex.Core.Scenes.Layouts.Html;

internal class HtmlSceneVector2f : IVector2<float>
{
    private static readonly char[] KnownCalcOperators = ['-', '+', '/', '*'];
    private readonly string _stringValue;

    public float X { get; private set; }
    public float Y { get; private set; }

    public HtmlSceneVector2f(string stringValue, IVector2<float>? parentValue, IVector2<float>? offset)
    {
        _stringValue = stringValue;
        Refresh(parentValue, offset);
    }

    public void Refresh(IVector2<float>? parentValue, IVector2<float>? offset)
    {
        var data = _stringValue.Split(',').Select(val => val.Trim()).ToArray();
        string x = data[0];
        string y = data[1];

        X = ComputeVectorValue(x, parentValue?.X ?? 0) + (offset?.X ?? 0);
        Y = ComputeVectorValue(y, parentValue?.Y ?? 0) + (offset?.Y ?? 0);
    }

    private static float ComputeVectorValue(string val, float parentVal)
    {
        val = val.Trim();

        if (val.StartsWith("calc(") && val.EndsWith(")"))
        {
            val = val[5..^1];
            var terms = val.Split(KnownCalcOperators).Select(term => ComputeVectorValue(term, parentVal)).ToList();
            var operators = val.FindAll(KnownCalcOperators).ToList();
            operators.Insert(0, '+'); // to match the length of the terms collection

            return Calc.Compute(terms, operators);
        }

        return val.EndsWith("%") ? parentVal * float.Parse(val[..^1]) / 100 : float.Parse(val);
    }

    public void Set(IVector2<float> vector)
    {
        X = vector.X;
        Y = vector.Y;
    }

    public void Set(float x, float y)
    {
        X = x;
        Y = y;
    }
}