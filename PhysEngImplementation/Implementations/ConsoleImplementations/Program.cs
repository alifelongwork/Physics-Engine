using System;
using ImplementationsLibrary;

namespace ConsoleImplementations
{
    class Program
    {
        static Matrix3x3MathArray matrix = Matrix3x3MathArray.CreateTranslation(0, 0);

        static void PrintMatrix(bool shouldRound = false)
        {
            //┌┐│└┘
            int cursorTop = Console.CursorTop;
            int cursorLeft = 2;

            int maxX = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.SetCursorPosition(cursorLeft, cursorTop + j + 1);

                    if (shouldRound)
                    {
                        Console.Write(Math.Round(matrix[i, j], 3));
                    }
                    else
                    {
                        Console.Write(matrix[i, j]);
                    }

                    if (maxX < Console.CursorLeft)
                    {
                        maxX = Console.CursorLeft;
                    }
                }
                cursorLeft = maxX + 1;
            }

            maxX++;

            Console.SetCursorPosition(0, cursorTop);
            Console.Write('┌');
            for (int i = 1; i < 4; i++)
            {
                Console.SetCursorPosition(0, cursorTop + i);
                Console.Write('│');
            }
            Console.SetCursorPosition(0, cursorTop + 4);
            Console.Write('└');

            Console.SetCursorPosition(maxX, cursorTop);
            Console.Write('┐');
            for (int i = 1; i < 4; i++)
            {
                Console.SetCursorPosition(maxX, cursorTop + i);
                Console.Write('│');
            }
            Console.SetCursorPosition(maxX, cursorTop + 4);
            Console.Write('┘');

            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            PrintMatrix();
            matrix.Transform(Matrix3x3MathArray.CreateTranslation(10, 5));
            PrintMatrix();
            matrix.Transform(Matrix3x3MathArray.CreateRotationZ((float)Math.PI/2));
            PrintMatrix(true);
        }
    }
}
