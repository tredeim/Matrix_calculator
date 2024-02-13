using System;
using static System.Math;
using System.IO;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http.Headers;

namespace Matrix_calculator
{
    class Program
    {
        static void Main(string[] args)
        {

            // Welcome and review.

            Console.WriteLine("                                                  Matrix Calculator");
            Console.WriteLine();
            Console.WriteLine(" Welcome to Matrix Calculator! What operation do you want to perform?");
            Console.WriteLine();
            Console.WriteLine(" 1) Finding the trace of a matrix." + "    5) Product of two matrices.");
            Console.WriteLine(" 2) Matrix transpose." + "                 6) Matrix multiplication by number.");
            Console.WriteLine(" 3) Sum of two matrices." + "              7) Finding the determinant of the matrix.");
            Console.WriteLine(" 4) Difference of two matrices." + "       8) Solve a system of linear equations.");
            Console.WriteLine();
            Console.WriteLine(" Select the number of the operation you want to perform.");

            // Repetition for default.

            var res = true;
            do
            {
                try
                {
                    Console.Write(' ');
                    var input = Console.ReadLine();

                    // Check.

                    switch (input)
                    {
                        case "1":
                            double[,] matrix1 = Type();
                            Console.WriteLine(Mtrace(matrix1));
                            res = true;
                            break;
                        case "2":
                            double[,] matrix2 = Type();
                            Console.WriteLine(Mtr(matrix2.GetLength(0), matrix2.GetLength(1), matrix2));
                                res = true;
                            break;
                        case "3":
                            double[,] matrix3_1 = Type();
                            double[,] matrix3_2 = Type();
                            Console.WriteLine(Msum(matrix3_1, matrix3_2));
                            res = true;
                            break;
                        case "4":
                            double[,] matrix4_1 = Type();
                            double[,] matrix4_2 = Type();
                            Console.WriteLine(Mdiff(matrix4_1, matrix4_2));
                            res = true;
                            break;
                        case "5":
                            double[,] matrix5_1 = Type();
                            double[,] matrix5_2 = Type();
                            Console.WriteLine(Mprod(matrix5_1, matrix5_2));
                            res = true;
                            break;
                        case "6":

                            res = true;
                            break;
                        case "7":
                            double[,] matrix7_1 = Type();
                            Console.WriteLine(Det(matrix7_1));
                            res = true;
                            break;
                        case "8":
                            double[,] m = new double[3, 3];
                            for (int i = 0; i < 3; i++)
                            {
                                for (int j = 0; j < 3; j++)
                                {
                                    Console.Write($"a[{i},{j}] = ");
                                    m[i, j] = double.Parse(Console.ReadLine());
                                }
                            }
                            double[] v = new double[3];
                            for (int i = 0; i < 3; i++)
                            {
                                v[i] = double.Parse(Console.ReadLine());
                            }
                            double[] s = Sole(m, v);
                            for (int i = 0; i < 3; i++)
                            {
                                Console.WriteLine(s[i]);
                            }
                            res = true;
                            break;
                        default:
                            res = false;
                            Console.WriteLine(" Сhoose a number from 1 to 8!");
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            while (res == false);
        }

        // Method for getting the trace of a matrix.

        static double Mtrace(double[,] matrix)
        {
            int n = int.Parse(Sqrt(matrix.Length).ToString());
            double trace = 0;
            for (int i = 0; i < n; i++)
            {
                trace += matrix[i, i];
            }
            return trace;
        }

        // Method for obtaining a transposed matrix.

        static double[,] Mtr(int m, int n, double[,] matrix)
        {
            double[,] tr = new double[n, m];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    tr[j, i] = matrix[i, j];
                }
            }
            return tr;
        }

        // Method for getting the sum of two matrices.

        static double[,] Msum(double[,] a, double[,] b)
        {
            double[,] c = new double[a.GetLength(0), a.GetLength(1)];
            int m = a.GetLength(0);
            int n = a.GetLength(1);
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    c[i, j] = a[i, j] + b[i, j];
                }
            }
            return c;
        }

        // Method for getting the difference of two matrices.

        static double[,] Mdiff(double[,] a, double[,] b)
        {
            double[,] c = new double[a.GetLength(0), a.GetLength(1)];
            int m = a.GetLength(0);
            int n = a.GetLength(1);
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    c[i, j] = a[i, j] - b[i, j];
                }
            }
            return c;
        }

        // Method for getting the product of two matrices.

        static double[,] Mprod(double[,] a, double[,] b)
        {
            double[,] c = new double[a.GetLength(0), b.GetLength(1)];
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < b.GetLength(1); j++)
                {
                    c[i, j] = 0;
                    for (int l = 0; l < a.GetLength(1); l++)
                    {
                        c[i, j] += a[i, l] * b[l, j];
                    }
                }
            }
            return c;
        }

        // Method for multiplying a matrix by a number.

        static double[,] Mmult(double[,] matrix, double k)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = k * matrix[i, j];
                }
            }
            return matrix;
        }

        // Methods for finding the determinant of a matrix.

        // We will find the determinant using the minors located in the first line.

        // 1. Method for determining the sign of elements.

        static int Sgn(int i, int j)
        {
            if ((i + j) % 2 == 0)
                return 1;
            else
                return -1;
        }

        // 2. Method for determining the sub-matrix corresponding to a given element.

        static double[,] Sub(double[,] matrix, int i, int j)
        {
            int order = int.Parse(Sqrt(matrix.Length).ToString());
            double[,] minor = new double[order - 1, order - 1];
            int u = 0, v = 0;
            for (int m = 0; m < order; m++, u++)
            {
                if (m != i)
                {
                    v = 0;
                    for (int n = 0; n < order; n++)
                    {
                        if (n != j)
                        {
                            minor[u, v] = matrix[m, n];
                            v++;
                        }
                    }
                }
                else
                    u--;
            }
            return minor;
        }

        //Method for finding the determinant (using recursion).

        static double Det(double[,] matrix)
        {
            int order = int.Parse(Sqrt(matrix.Length).ToString());
            double determ = 0;
            if (order == 1)
                return matrix[0, 0];
            if (order == 2)
                return ((matrix[0, 0] * matrix[1, 1]) - (matrix[0, 1] * matrix[1, 0]));
            else
            {
                for (int j = 0; j < order; j++)
                {
                    determ += Sgn(0, j) * matrix[0, j] * Det(Sub(matrix, 0, j));
                }
                return determ;
            }

        }

        // We will solve a system of linear equations using the kramer method.

        // Method for solving SOLE.

        static double[] Sole(double[,] matrix, double[] vector)
        {
            int order = int.Parse(Sqrt(matrix.Length).ToString());

            // D - determinant of a matrix.

            double D = Det(matrix);

            // d - the matrix obtained from our matrix where the place of the i-th column is a vector column.

            // Create matrices d[i].

            double[][,] d = new double[order][,];

            for (int i = 0; i < order; i++)
            {
                d[i] = new double[order, order];
                for (int m = 0; m < order; m++)
                {
                    for (int n = 0; n < order; n++)
                    {
                        if (n == i)
                        {
                            d[i][m, n] = vector[m];
                        }
                        else
                            d[i][m, n] = matrix[m, n];
                    }
                }
            }

            // Now we will find solutions using the kramer method .

            double[] solutions = new double[order];
            for (int i = 0; i < order; i++)
            {
                solutions[i] = Det(d[i]) / D;
            }
            return solutions;
        }

        // Method for selecting the input data type.

        static double[,] Type()
        {

            Console.WriteLine(" Сhoose a way to get input");
            Console.WriteLine();
            Console.WriteLine(" 1) Random matrix\n 2) From console\n 3) From file");
            double[,] matrix = null;
            // Repetition for default.

            var res = true;
            do
            {
                Console.Write(' ');
                var t = Console.ReadLine();
                switch (t)
                {
                    case "1":
                        var r = true;
                        int a, b, x, y;
                        Console.WriteLine(" Select generation options for matrix size.");
                        Console.WriteLine(" Borders must be from 1 to 10.");
                        do
                        {
                            Console.Write(" Bottom border - ");
                            var result1 = int.TryParse(Console.ReadLine(), out a);
                            Console.Write(" Upper border - ");
                            var result2 = int.TryParse(Console.ReadLine(), out b);
                            if (result1 == false || result2 == false || a < 1 || a > 10 || b < 1 || b > 10 || a > b)
                            {
                                r = false;
                                Console.WriteLine(" Please enter correct input!");
                            }
                            else
                            {
                                r = true;
                            }
                        }
                        while (r == false);
                        Console.WriteLine(" Select generation options for matrix elemente.");
                        Console.WriteLine(" Borders must be from -1000 to 1000.");
                        do
                        {
                            Console.Write(" Bottom border - ");
                            var result1 = int.TryParse(Console.ReadLine(), out x);
                            Console.Write(" Upper border - ");
                            var result2 = int.TryParse(Console.ReadLine(), out y);
                            if (result1 == false || result2 == false || x < -1000 || x > 1000 || y < -1000 || y > 1000 || x > y)
                            {
                                r = false;
                                Console.WriteLine(" Please enter correct input!");
                            }
                            else
                            {
                                r = true;
                            }
                        }
                        while (r == false);
                        matrix = Mrandom(a, b, x, y);
                        res = true;
                        break;
                    case "2":
                        matrix = ReadFromConsole();
                        res = true;
                        break;
                    case "3":
                        matrix = ReadFromFile();
                        res = true;
                        break;
                    default:
                        res = false;
                        Console.WriteLine(" Сhoose a number from 1 to 3!");
                        break;
                }
            }
            while (res == false);
            return matrix;
        }

        // Method for obtaining a random matrix. 

        static double[,] Mrandom(int a, int b, int x, int y)
        {
            Random rnd = new Random();
            int m = rnd.Next(a, b + 1);
            int n = rnd.Next(a, b + 1);
            double[,] mrand = new double[m, n];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    mrand[i, j] = rnd.Next(x, y + 1) + rnd.NextDouble();
                }
            }
            return mrand;
        }

        // Method for getting matrix from console .

        static double[,] ReadFromConsole()
        {
            Console.WriteLine(" Enter the number of rows");
            Console.Write(' ');
            int.TryParse(Console.ReadLine(), out int m);
            Console.WriteLine(" Enter the number of columns");
            Console.Write(' ');
            int.TryParse(Console.ReadLine(), out int n);
            double[,] matrix = new double[m, n];
            for (var i = 0; i < m; i++)
            {
                var line = Console.ReadLine();
                var doubles = line.Split(" ");

                if (doubles.Length != n)
                {
                    throw new Exception();
                }

                for (var j = 0; j < n; j++)
                {
                    matrix[i, j] = double.Parse(doubles[j]);
                }
            }

            return matrix;
        }

        static double[,] ReadFromFile()
        {
            Console.WriteLine(" Enter the file path");
            Console.Write(' ');
            var filePath = Console.ReadLine();
            var lines = File.ReadAllLines(filePath);
            var pars = lines[0].Split(' ');
            var m = int.Parse(pars[0]);
            var n = int.Parse(pars[1]);

            double[,] matrix = new double[m, n];

            for (var i = 0; i < m; i++)
            {
                var line = lines[i + 1];
                var doubles = line.Split(" ");

                if (doubles.Length != n)
                {
                    throw new Exception();
                }

                for (var j = 0; j < n; j++)
                {
                    matrix[i, j] = double.Parse(doubles[j]);
                }
            }

            return matrix;
        }
    }

}


