static class Algorithm
{
    public static int NOD(int a, int b)
    {
        if (a < 0) return NOD(-a, b);
        if (b < 0) return NOD(a, -b);
        if (b > a) return NOD(b, a);
        if (b == 0) return a;
        return NOD(b, a % b);
    }
}