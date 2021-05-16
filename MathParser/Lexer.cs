using System;
using System.Collections.Generic;
using System.Text;

namespace MathParser
{
    public class Lexer
    {
        private static string allowedChars =
        " abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789.,()-+/*=^αβΓγΔδεζηΘθικΛλμνΞξΠπρΣσςτυΦφχΨψΩω";

        private static string numbers = "0123456789.,";

        private static Dictionary<string, TokenType> symbolSpliters =
        new Dictionary<string, TokenType>()
        {
            { "+", TokenType.Sum },
            { "-", TokenType.Sub },
            { "*", TokenType.Mul },
            { "/", TokenType.Div },
            { "^", TokenType.Pow },

            { "(", TokenType.OpenParenthesis },
            { ")", TokenType.CloseParenthesis },
            { "=", TokenType.Equal }

        };

        public Token[] SplitTokens(string line)
        {
            List<Token> tokens = new List<Token>();
            Token current = new Token(string.Empty, TokenType.Unknown);

            int i = 0;
            foreach(char ch in line)
            {
                if (!allowedChars.Contains(ch))
                {
                    throw new Exception($"Invalid token \"{ch}\"!");
                }
                bool isSymbol = symbolSpliters.ContainsKey(ch.ToString());
                if (isSymbol || ch == ' ')
                {
                    if (!string.IsNullOrEmpty(current.TextElement))
                    {
                        tokens.Add(current);
                    }
                    current = new Token(string.Empty, TokenType.Unknown);

                    if (isSymbol)
                    {
                        // Now, add the symbol
                        tokens.Add(new Token(ch.ToString(), symbolSpliters[ch.ToString()]));
                    }
                    continue;
                }

                current.TextElement += ch;
            }

            if (!string.IsNullOrEmpty(current.TextElement))
                tokens.Add(current);

            return tokens.ToArray();
        }

        public Token[] IdentifyTokens(Token[] tokens)
        {
            List<Token> identifiedTokens = new List<Token>();

            foreach(Token tokenElement in tokens)
            {
                Token tok = tokenElement;
                if (tok.Type == TokenType.Unknown)
                {
                    bool isNumber = true;
                    bool containsNumber = false;
                    foreach(char tokenCh in tok.TextElement)
                    {
                        if (!numbers.Contains(tokenCh))
                        {
                            isNumber = false;
                        }
                        else
                        {
                            containsNumber = true;
                        }
                    }

                    if (isNumber)
                    {
                        tok.Type = TokenType.Number;
                    }
                    else if (!containsNumber)
                    {
                        tok.Type = TokenType.Variable;
                    }
                    else
                    {
                        throw new Exception($"Unable to identify \"{tok.TextElement}\"!");
                    }
                }

                identifiedTokens.Add(tok);
            }

            return identifiedTokens.ToArray();
        }

        public Token[] Lex(string expression)
        {
            Token[] preLexed = SplitTokens(expression);
            return IdentifyTokens(preLexed);
        }
    }
}
