class Fraction
{
    public int Numerator { get; private set; }

    public int Denominator { get; private set; }

    public double Result => Convert.ToDouble(Numerator) / Convert.ToDouble(Denominator);

    public Fraction() : this(0)
    {
    }

    public Fraction(int numerator) : this(numerator, 1)
    {
    }

    public Fraction(int numerator, int denominator)
    {
        Numerator = numerator;
        Denominator = denominator;

        Cut();
    }

    public override string ToString() => Numerator == 0? "0" : $"{Numerator}{(Denominator==1? "" : $"/{Denominator}")}";

    private void Cut()
    {
        if (Denominator < 0)
        {
            Numerator *= -1;
            Denominator *= -1;
        }

        int nod = Algorithm.NOD(Numerator, Denominator);

        if (nod == 1) return;

        Numerator /= nod;
        Denominator /= nod;
    }

    public static Fraction operator +(Fraction fraction1, Fraction fraction2) => new (
        fraction1.Numerator * fraction2.Denominator + fraction2.Numerator * fraction1.Denominator,
        fraction1.Denominator * fraction2.Denominator
    );

    public static Fraction operator +(Fraction fraction, int num) => fraction + (new Fraction(num));

    public static Fraction operator +(int num, Fraction fraction) => (new Fraction(num)) + fraction;

    public static Fraction operator -(Fraction fraction1, Fraction fraction2) => fraction1 + (-1 * fraction2);

    public static Fraction operator -(Fraction fraction, int num) => fraction - (new Fraction(num));

    public static Fraction operator -(int num, Fraction fraction) => (new Fraction(num)) - fraction;


    public static Fraction operator *(Fraction fraction1, Fraction fraction2) => new(
        fraction1.Numerator * fraction2.Numerator,
        fraction1.Denominator * fraction2.Denominator
    );

    public static Fraction operator /(Fraction fraction1, Fraction fraction2) => new(
        fraction1.Numerator * fraction2.Denominator,
        fraction1.Denominator * fraction2.Numerator
    );

    public static Fraction operator *(Fraction fraction, int num) => new(
        fraction.Numerator * num,
        fraction.Denominator
    );

    public static Fraction operator *(int num, Fraction fraction) => fraction * num;

    public static Fraction operator /(Fraction fraction, int num) => new(
        fraction.Numerator,
        fraction.Denominator * num
    );

    public static Fraction operator /(int num, Fraction fraction) => (new Fraction(num)) / fraction;

    public static Fraction operator %(Fraction fraction1, Fraction fraction2) => fraction1 - ((int)(fraction1 / fraction2).Result) * fraction2;

    public static Fraction operator %(Fraction fraction, int num) => fraction % (new Fraction(num));

    public static Fraction operator %(int num, Fraction fraction) => (new Fraction(num)) % fraction;
}
