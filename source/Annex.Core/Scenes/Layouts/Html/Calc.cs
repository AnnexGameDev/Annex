namespace Annex.Core.Scenes.Layouts.Html;

internal class Calc
{
    public static float Compute(List<float> terms, List<char> operators)
    {
        var nodes = terms.Select(value => new CalcNode(value)).ToList();
        Assert.IsTrue(nodes.Count == operators.Count);

        for (int i = 0; i < nodes.Count; i++)
        {
            if (operators[i] == '/' ||  operators[i] == '*')
            {
                var replacement = new CalcNode(operators[i], nodes[i - 1], nodes[i]);

                operators.RemoveAt(i);
                nodes.RemoveAt(i);
                nodes.RemoveAt(i - 1);
                nodes.Insert(i - 1, replacement);

                i--;

                Assert.IsTrue(nodes.Count == operators.Count);
            }
        }

        float result = 0;
        for (int i = 0; i < nodes.Count; i++)
        {
            var op = operators[i];
            var term = nodes[i];

            result = op switch
            {
                '+' => result + term.Calculate(),
                '-' => result - term.Calculate(),
                _ => throw new NotSupportedException(op.ToString())
            };
        }

        return result;
    }
}

file record CalcNode
{
    private char _op;
    private float? _value;
    private CalcNode? _lhs;
    private CalcNode? _rhs;

    public CalcNode(char op, CalcNode lhs, CalcNode rhs)
    {
        _op = op;
        _lhs = lhs;
        _rhs = rhs;
        _value = null;
    }

    public CalcNode(float Value)
    {
        _op = '+';
        _value = Value;
        _lhs = null;
        _rhs = null;
    }

    public float Calculate()
    {
        if (_value != null)
        {
            return _value.Value;
        }

        return _op switch
        {
            '+' => _lhs!.Calculate() + _rhs!.Calculate(),
            '-' => _lhs!.Calculate() - _rhs!.Calculate(),
            '*' => _lhs!.Calculate() * _rhs!.Calculate(),
            '/' => _lhs!.Calculate() / _rhs!.Calculate(),
        };
    }
}