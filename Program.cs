Polynomial p = new("-3 -4 -1 3 1");
Polynomial p2 = new("-3 2 10 3");

Console.WriteLine($"p \t= {p}\np2 \t= {p2}\n");

Console.WriteLine(p / p2);
Console.WriteLine(p % p2);

Console.WriteLine(Polynomial.NOD(p, p2));