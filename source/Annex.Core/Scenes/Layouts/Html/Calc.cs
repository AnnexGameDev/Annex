namespace Annex.Core.Scenes.Layouts.Html;

public class Calc
{
    public static float Compute(string expression, float? valueToApplyToPercentages = 0)
    {
        float percentBase = valueToApplyToPercentages ?? 0;

        var tokens = Tokenize(expression);
        int pos = 0;
        var node = ParseExpression(tokens, percentBase, ref pos);

        if (pos != tokens.Count)
            throw new FormatException($"Unexpected token '{tokens[pos]}'");

        return node.Calculate();
    }

    private static List<string> Tokenize(string expr)
    {
        var tokens = new List<string>();
        int i = 0;

        while (i < expr.Length)
        {
            char c = expr[i];

            if (char.IsWhiteSpace(c))
            { i++; continue; }

            if (c is '+' or '-' or '*' or '/' or '(' or ')')
            {
                tokens.Add(c.ToString());
                i++;
                continue;
            }

            if (char.IsDigit(c) || c == '.' || c == '%')
            {
                int start = i;
                while (i < expr.Length && (char.IsDigit(expr[i]) || expr[i] == '.' || expr[i] == '%'))
                    i++;
                tokens.Add(expr[start..i]);
                continue;
            }

            throw new FormatException($"Unexpected character '{c}' in expression");
        }

        return tokens;
    }

    // expression := term (('+' | '-') term)*
    private static CalcNode ParseExpression(List<string> tokens, float percentBase, ref int pos)
    {
        var node = ParseTerm(tokens, percentBase, ref pos);

        while (pos < tokens.Count && (tokens[pos] == "+" || tokens[pos] == "-"))
        {
            char op = tokens[pos][0];
            pos++;
            var rhs = ParseTerm(tokens, percentBase, ref pos);
            node = new CalcNode(op, node, rhs);
        }

        return node;
    }

    // term := factor (('*' | '/') factor)*
    private static CalcNode ParseTerm(List<string> tokens, float percentBase, ref int pos)
    {
        var node = ParseFactor(tokens, percentBase, ref pos);

        while (pos < tokens.Count && (tokens[pos] == "*" || tokens[pos] == "/"))
        {
            char op = tokens[pos][0];
            pos++;
            var rhs = ParseFactor(tokens, percentBase, ref pos);
            node = new CalcNode(op, node, rhs);
        }

        return node;
    }

    // factor := leaf | '(' expression ')' | '-' factor
    private static CalcNode ParseFactor(List<string> tokens, float percentBase, ref int pos)
    {
        if (pos >= tokens.Count)
            throw new FormatException("Unexpected end of expression");

        var token = tokens[pos];

        if (token == "(")
        {
            pos++; // consume '('
            var node = ParseExpression(tokens, percentBase, ref pos);

            if (pos >= tokens.Count || tokens[pos] != ")")
                throw new FormatException("Expected ')'");

            pos++; // consume ')'
            return node;
        }

        if (token == "-") // unary minus
        {
            pos++;
            var operand = ParseFactor(tokens, percentBase, ref pos);
            return new CalcNode('-', new CalcNode(0f), operand);
        }

        pos++;
        return new CalcNode(ResolveLeaf(token, percentBase));
    }

    private static float ResolveLeaf(string token, float percentBase)
    {
        return token.EndsWith('%')
            ? percentBase * float.Parse(token[..^1], System.Globalization.CultureInfo.InvariantCulture) / 100
            : float.Parse(token, System.Globalization.CultureInfo.InvariantCulture);
    }
}

internal record CalcNode
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

    public CalcNode(float value)
    {
        _op = '+';
        _value = value;
        _lhs = null;
        _rhs = null;
    }

    public float Calculate()
    {
        if (_value != null)
            return _value.Value;

        return _op switch
        {
            '+' => _lhs!.Calculate() + _rhs!.Calculate(),
            '-' => _lhs!.Calculate() - _rhs!.Calculate(),
            '*' => _lhs!.Calculate() * _rhs!.Calculate(),
            '/' => _lhs!.Calculate() / _rhs!.Calculate(),
            _ => throw new NotSupportedException(_op.ToString())
        };
    }
}