﻿using AoCHelper;


if (args.Length == 0)
{
    // Solver.SolveAll();
    Solver.SolveLast();
}
else if (args.Length == 1 && args[0].Contains("all", System.StringComparison.CurrentCultureIgnoreCase))
{
    Solver.SolveAll();
}
else
{
    var indexes = args.Select(arg => uint.TryParse(arg, out var index) ? index : uint.MaxValue);

    Solver.Solve(indexes.Where(i => i < uint.MaxValue));
}
