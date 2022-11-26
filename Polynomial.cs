using System;
using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;
using System.Xml.Linq;

class Polynomial
{
    public List<int> Coefficients { get; private set; }

    public Polynomial(string line) : this(Regex.Replace(line, @"\s+", " ").Split(" ").ToList().Select(int.Parse).ToList())
    {
    }

    public Polynomial(Polynomial polynomial) : this(new List<int>(polynomial.Coefficients)) 
    { 
    }

    public Polynomial(List<int> coefficients)
    {
        Coefficients = coefficients;

        for (int i = Coefficients.Count - 1; i >= 0; i--)
            if (Coefficients[i] != 0) break;
            else Coefficients.RemoveAt(i);
    }

    public Polynomial ShiftRight(int shift)
    {
        var coefficients = new List<int> (Coefficients);

        for (int i = 0; i < shift; i++) coefficients.Insert(0, 0);

        return new(coefficients);
    }

    public override string ToString()
    {
        string res = "";
        int i = Coefficients.Count-1;
        bool flag = false;
        Coefficients.Reverse();
        foreach (var coefficient in Coefficients)
        {
            if (coefficient != 0)
            {
                res += $"{(coefficient < 0 ? $"- {(i != 0 && -coefficient == 1? "" : -coefficient)}" : (flag ? $"+ {(i != 0 && coefficient == 1? "" : coefficient)}" : $"{(i != 0 && coefficient == 1 ? "" : coefficient)}"))}{(i != 0 ? $"x{(i==1? "" : $"^{i}")}" : "")} ";
                flag = true;
            }
            i--;
        }
        Coefficients.Reverse();
        return res;
    }

    public static (Polynomial, Polynomial) DivisionWithRemainder(Polynomial polynomial1, Polynomial polynomial2)
    {
        List<int> coefficients = new();
        Polynomial remain = new(polynomial1);

        while (remain.Coefficients.Count >= polynomial2.Coefficients.Count)
        {
            var coefficient = remain.Coefficients[^1] / polynomial2.Coefficients[^1];

            coefficients.Add(coefficient);

            remain = remain - (polynomial2.ShiftRight(remain.Coefficients.Count - polynomial2.Coefficients.Count) * coefficient);
        }

        coefficients.Reverse();

        return (new(coefficients), remain);
    }
   
    public static Polynomial NOD(Polynomial polynomial1, Polynomial polynomial2)
    {
        if (polynomial2.Coefficients.Count == 0) return polynomial1;
        return NOD(polynomial2, polynomial1 % polynomial2);
    }

    public static Polynomial operator +(Polynomial polynomial1, Polynomial polynomial2)
    {
        List<int> coefficients = new();

        for (int i = 0; i < Math.Max(polynomial1.Coefficients.Count, polynomial2.Coefficients.Count); i++)
        {
            coefficients.Add(0);
            if (i < polynomial1.Coefficients.Count) coefficients[i] += polynomial1.Coefficients[i];
            if (i < polynomial2.Coefficients.Count) coefficients[i] += polynomial2.Coefficients[i];
        }

        return new(coefficients);
    }

    public static Polynomial operator -(Polynomial polynomial1, Polynomial polynomial2) => polynomial1 + (-1 * polynomial2);

    public static Polynomial operator /(Polynomial polynomial1, Polynomial polynomial2)
    {
        Polynomial res;
        (res, _) = DivisionWithRemainder(polynomial1, polynomial2);

        return res;
    }

    public static Polynomial operator %(Polynomial polynomial1, Polynomial polynomial2)
    {
        Polynomial res;
        (_, res) = DivisionWithRemainder(polynomial1, polynomial2);

        return res;
    }

    public static Polynomial operator *(int num, Polynomial polynomial)
    {
        List<int> coefficients = new();

        foreach (var p in polynomial.Coefficients)
            coefficients.Add(p * num);

        return new(coefficients);
    }

    public static Polynomial operator *(Polynomial polynomial, int num) => num * polynomial;
}