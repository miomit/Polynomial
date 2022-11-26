Polynomial p = new("-1 0 0 1");
Polynomial p2 = new("-1 0 1");

Console.WriteLine($"p \t= {p}\np2 \t= {p2}\n");


Console.WriteLine(p / p2);
Console.WriteLine(p % p2);
Console.WriteLine(Polynomial.NOD(p, p2));