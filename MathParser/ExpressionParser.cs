using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace MathParser
{
    public class ExpressionParser
    {
        public enum OperationItem
        {
            ValueA = 0,
            Operator = 1,
            ValueB = 2
        }

        private MathValue GetValueFromToken(Token[] tokens, ref int currentTokenIndex, int currentOperationPriority = 0)
        {
            Token current = tokens[currentTokenIndex];
            MathValue currentTokenAsMathValue;
            if (current.Type == TokenType.OpenParenthesis)
            {
                int openedParenthesis = 1;
                List<Token> tokensInsideParenthesis = new List<Token>();

                int j;
                for (j = currentTokenIndex + 1; j < tokens.Length; j++)
                {
                    Token innerToken = tokens[j];
                    if (innerToken.Type == TokenType.OpenParenthesis)
                    {
                        openedParenthesis++;
                    }
                    else if (innerToken.Type == TokenType.CloseParenthesis)
                    {
                        openedParenthesis--;
                    }
                    if (openedParenthesis == 0)
                        break;

                    tokensInsideParenthesis.Add(innerToken);
                }

                currentTokenAsMathValue = ParseNext(tokensInsideParenthesis.ToArray(), 0);

                currentTokenIndex = j;
            }
            else
            {
                currentTokenAsMathValue = new MathValue(current.ToDecimalNumber());
            }
            if (currentTokenIndex >= tokens.Length - 1)
            {
                return currentTokenAsMathValue;
            }
            else
            {
                // Speculate if next operation is a higher priority
                // As number is received, next SHOULD be an operator

                Token nextOperator = tokens[currentTokenIndex + 1];
                OperationType operationType = OperatorUtils.GetOperatorTypeFromTokenType(nextOperator.Type);
                int nextPriority = OperatorUtils.GetPriority(operationType);

                if (nextPriority > currentOperationPriority)
                {
                    // So, next operation will be evaluated as a single value
                    currentTokenIndex += 2;
                    return new Operation(currentTokenAsMathValue, GetValueFromToken(
                        tokens,
                        ref currentTokenIndex,
                        nextPriority
                    ), operationType);
                }

                return currentTokenAsMathValue;

            }
        }

        public MathValue ParseNext(Token[] tokens, int i, MathValue lastValue = null, int priority = 0)
        {
            Token token = tokens[i];

            MathValue result;

            if (OperatorUtils.IsOperator(token))
            {
                if (lastValue == null)
                {
                    throw new Exception("Error");
                }

                i++;

                OperationType op = OperatorUtils.GetOperatorTypeFromTokenType(token.Type);
                result = new Operation(lastValue, 
                    GetValueFromToken(tokens, ref i, priority),
                 op);
                i++;
            }
            else
            {
                result = GetValueFromToken(tokens, ref i, priority);
                i++;
            }

            if (i >= tokens.Length - 1)
            {
                return result;
            }

            return ParseNext(tokens, i, result, priority);
        }

        void printOp(MathValue val)
        {

            if (!(val is Operation))
            {
                Console.Write(val.Value);
            }
            else
            {
                Console.Write("[");
                Operation op = (Operation)val;
                if (op.A != null)
                {
                    printOp(op.A);
                    Console.Write($"{OperatorUtils.ToString(op.OpType)}");
                }
                if (op.B != null)
                {
                    printOp(op.B);
                }
                Console.Write("]");
            }

        }

        public MathValue Parse(Token[] tokens, bool debug = false)
        {
            var result = ParseNext(tokens, 0);

            if (debug)
            {
                printOp(result);
                Console.WriteLine();
            }

            return result;
        }
    }
}
