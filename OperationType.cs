using System;

public enum OperationType
{
    Sum,
    Subtract,
    Multiply,
    Divide
}

public static class OperationSolver
{
    private delegate void SolveOperation(decimal a, decimal b);

    private static void SolveSum(decimal a, decimal b)
    {
        return a + b;
    }

    private static void SolveSubtraction(decimal a, decimal b)
    {
        return a - b;
    }

    private static void SolveMultiplication(decimal a, decimal b)
    {
        return a * b;
    }

    private static void SolveDivision(decimal a, decimal b)
    {
        return a / b;
    }

    private readonly static Dictionary<OperationType, SolveOperation> howToSolve = new Dictionary<OperationType, SolveOperation>()
    {
        { OperationType.Sum,        SolveSum },
        { OperationType.Subtract,   SolveSubtraction },
        { OperationType.Multiply,   SolveMultiplication },
        { OperationType.Divide,     SolveDivision }
    };

    public static void Solve(OperationType type, decimal a, decimal b)
    {
        return howToSolve[type](a, b);
    }

}

public class Operation : MathValue
{
    public MathValue A;
    public MathValue B;
    public OperationType OpType;

    public Operation() { }

    public Operation(MathValue a, MathValue b, OperationType type) : base()
    {
        A = a;
        B = b;
        OpType = type;
    }

    public Operation(MathValue a, MathValue b) : base(a, b, OperationType.Sum)
    {

    }

    private override decimal GetValue()
    {
        decimal a = A.Value;
        decimal b = B.Value;

        return OperationSolver.Solve(OpType, a, b);
    }
}