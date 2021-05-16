using System;

namespace MathParser
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.Title = "Advαncεd Cαlculατοr!";

            Lexer theLexer = new Lexer();

            while (true)
            {
                //try
                //{
                    string line = Console.ReadLine();
                    Token[] tokens = theLexer.Lex(line);

                    ExpressionParser expr = new ExpressionParser();
                    var x = expr.Parse(tokens, true);

                    Console.WriteLine(x.Value);
                //}
                //catch (Exception ex)
                //{
                //    Console.ForegroundColor = ConsoleColor.Red;
                //    Console.WriteLine(ex.Message);
                //    Console.WriteLine(ex.StackTrace);
                //    Console.ResetColor();
                //}
            }
        }
    }
}
