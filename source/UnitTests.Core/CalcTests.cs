using Annex.Core.Scenes.Layouts.Html;
using Xunit;

namespace UnitTests.Core;

public class CalcTests
{
    // ----- Basic arithmetic -----

    [Theory]
    [InlineData("2 + 3", 5f)]
    [InlineData("10 - 4", 6f)]
    [InlineData("3 * 4", 12f)]
    [InlineData("10 / 4", 2.5f)]
    [InlineData("5", 5f)]
    public void SingleOperator_ComputesCorrectly(string expr, float expected)
    {
        Assert.Equal(expected, Calc.Compute(expr), 3);
    }

    // ----- Operator precedence -----

    [Theory]
    [InlineData("2 + 3 * 4", 14f)]        // * before +
    [InlineData("2 * 3 + 4", 10f)]        // * before +, other order
    [InlineData("10 - 4 / 2", 8f)]        // / before -
    [InlineData("2 + 3 * 4 - 5 / 5", 13f)]// mixed chain
    public void RespectsOperatorPrecedence(string expr, float expected)
    {
        Assert.Equal(expected, Calc.Compute(expr), 3);
    }

    // ----- Parentheses -----

    [Theory]
    [InlineData("(2 + 3) * 4", 20f)]
    [InlineData("4 * (2 + 3)", 20f)]
    [InlineData("(2 + 3) * (4 - 1)", 15f)]
    [InlineData("((2 + 3))", 5f)]
    [InlineData("(2 + (3 * (4 - 1)))", 11f)]
    public void ParenthesesOverridePrecedence(string expr, float expected)
    {
        Assert.Equal(expected, Calc.Compute(expr), 3);
    }

    // ----- Unary minus -----

    [Theory]
    [InlineData("-5", -5f)]
    [InlineData("-5 + 10", 5f)]
    [InlineData("-(2 + 3)", -5f)]
    [InlineData("10 * -2", -20f)]
    [InlineData("-(-5)", 5f)]
    public void UnaryMinus_ComputesCorrectly(string expr, float expected)
    {
        Assert.Equal(expected, Calc.Compute(expr), 3);
    }

    // ----- Whitespace / formatting tolerance -----

    [Theory]
    [InlineData("2+3*4", 14f)]
    [InlineData("  2   +   3   ", 5f)]
    [InlineData("2.5 + 1.5", 4f)]
    public void ToleratesWhitespaceAndDecimals(string expr, float expected)
    {
        Assert.Equal(expected, Calc.Compute(expr), 3);
    }

    // ----- Error handling -----

    [Fact]
    public void MismatchedOpenParen_Throws()
    {
        Assert.Throws<FormatException>(() => Calc.Compute("(2 + 3"));
    }

    [Fact]
    public void MismatchedCloseParen_Throws()
    {
        Assert.Throws<FormatException>(() => Calc.Compute("2 + 3)"));
    }

    [Fact]
    public void UnexpectedCharacter_Throws()
    {
        Assert.Throws<FormatException>(() => Calc.Compute("2 + a"));
    }

    [Fact]
    public void EmptyExpression_Throws()
    {
        Assert.Throws<FormatException>(() => Calc.Compute(""));
    }

    [Fact]
    public void TrailingOperator_Throws()
    {
        Assert.Throws<FormatException>(() => Calc.Compute("2 +"));
    }

    [Fact]
    public void DivisionByZero_ReturnsInfinity()
    {
        // float division by zero doesn't throw — matches CalcNode.Calculate's
        // existing '/' behavior, so this pins down that (perhaps surprising)
        // contract rather than silently changing it.
        var result = Calc.Compute("5 / 0");
        Assert.True(float.IsPositiveInfinity(result));
    }
}