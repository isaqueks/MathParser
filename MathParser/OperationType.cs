using System;
using System.Collections.Generic;

namespace MathParser
{


    public enum OperationType
    {
        Sum,
        Subtract,
        Multiply,
        Divide,
        Pow
    }

    public static class OperatorUtils
    {
        private delegate decimal SolveOperation(decimal a, decimal b);

        private static decimal SolveSum(decimal a, decimal b)
        {
            return a + b;
        }

        private static decimal SolveSubtraction(decimal a, decimal b)
        {
            return a - b;
        }

        private static decimal SolveMultiplication(decimal a, decimal b)
        {
            return a * b;
        }

        private static decimal SolveDivision(decimal a, decimal b)
        {
            return a / b;
        }

        private static decimal SolvePower(decimal a, decimal b)
        {
            return (decimal)Math.Pow((double)a, (double)b);
        }

        private readonly static Dictionary<OperationType, SolveOperation> howToSolve = new Dictionary<OperationType, SolveOperation>()
        {
            { OperationType.Sum,        SolveSum },
            { OperationType.Subtract,   SolveSubtraction },
            { OperationType.Multiply,   SolveMultiplication },
            { OperationType.Divide,     SolveDivision },
            { OperationType.Pow,        SolvePower }
        };

        private readonly static Dictionary<OperationType, int> priority = new Dictionary<OperationType, int>()
        {
            { OperationType.Sum,        0 },
            { OperationType.Subtract,   0 },
            { OperationType.Multiply,   1 },
            { OperationType.Divide,     1 },
            { OperationType.Pow,        2 }
        };

        private readonly static Dictionary<OperationType, string> toString = new Dictionary<OperationType, string>()
        {
            { OperationType.Sum,        "+" },
            { OperationType.Subtract,   "-" },
            { OperationType.Multiply,   "*" },
            { OperationType.Divide,     "/" },
            { OperationType.Pow,        "^" }
        };

        private readonly static Dictionary<TokenType, OperationType> typePair = new Dictionary<TokenType, OperationType>()
        {
            { TokenType.Sum,    OperationType.Sum },
            { TokenType.Sub,    OperationType.Subtract },
            { TokenType.Mul,    OperationType.Multiply },
            { TokenType.Div,    OperationType.Divide },
            { TokenType.Pow,    OperationType.Pow }
        };

        public static decimal Solve(OperationType type, decimal a, decimal b)
        {
            return howToSolve[type](a, b);
        }

        public static int GetPriority(OperationType type)
        {
            return priority[type];
        }

        public static bool IsOperator(Token token)
        {
            return typePair.ContainsKey(token.Type);
        }

        public static OperationType GetOperatorTypeFromTokenType(TokenType tokType)
        {
            return typePair[tokType];
        }

        public static string ToString(OperationType op)
        {
            return toString[op];
        }
    }
}