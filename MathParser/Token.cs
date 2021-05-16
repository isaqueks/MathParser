using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace MathParser
{
    public enum TokenType
    {
        Unknown = 0,

        OpenParenthesis,
        CloseParenthesis,

        Sum,
        Sub,
        Div,
        Mul,
        Pow,

        Equal,

        Number,
        Variable
    }

    public struct Token
    {
        public string TextElement;
        public TokenType Type;

        public Token(string tok, TokenType type)
        {
            TextElement = tok;
            Type = type;
        }

        public decimal ToDecimalNumber()
        {
            if (this.Type != TokenType.Number)
            {
                throw new Exception("Token isn't a number!");
            }

            return decimal.Parse(this.TextElement, new CultureInfo("en-US"));
        }
    }
}
