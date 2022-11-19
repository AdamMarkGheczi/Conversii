using System;
using System.Collections.Generic;
using System.Linq;

namespace Conversii
{
    internal class Program
    {

        static public int digitToDecimal(char d)
        {
            switch (d)
            {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    return int.Parse(d.ToString());
                case 'a':
                case 'A':
                    return 10;
                case 'b':
                case 'B':
                    return 11;
                case 'c':
                case 'C':
                    return 12;
                case 'd':
                case 'D':
                    return 13;
                case 'e':
                case 'E':
                    return 14;
                case 'f':
                case 'F':
                    return 15;

                default:
                    return 0;
            }

        }
        static public char digitToDifferentBase(int d)
        {

            switch (d)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                    return d.ToString()[0];
                case 10:
                    return 'A';
                case 11:
                    return 'B';
                case 12:
                    return 'C';
                case 13:
                    return 'D';
                case 14:
                    return 'E';
                case 15:
                    return 'F';

                default:
                    return ' ';
            }
        }

        static public long wholeNumberToDecimal(string n, int b)
        {
            long result = 0;

            for (int i = n.Length - 1; i >= 0; i--)
                result += digitToDecimal(n[i]) * (long)Math.Pow(b, n.Length - i - 1);

            return result;
        }


        static public string wholeNumberToDifferentBase(string s, int b1, int b2)
        {
            if (s == "0")
                return "0";
            long n = wholeNumberToDecimal(s, b1);

            string result = "";

            while (n > 0)
            {
                result += digitToDifferentBase((int)(n % b2));
                n /= b2;
            }

            char[] temp = result.ToCharArray();
            Array.Reverse(temp);

            return new string(temp);
        }

        static string fractionToDifferentBase(string fraction, int b1, int b2)
        {
            if (fraction == "")
                return "";

            string result = ".";

            int[] digits = new int[fraction.Length];

            for (int i = 0; i < fraction.Length; i++)
                digits[i] = digitToDecimal(fraction[i]);

            int carry, temp;

            List<string> prev = new List<string>();

            do
            {
                prev.Add(string.Join("", digits));
                carry = 0;
                for (int j = digits.Length - 1; j >= 0; j--)
                {
                    temp = digits[j] * b2 + carry;

                    digits[j] = temp % b1;
                    carry = temp / b1;
                }

                result += digitToDifferentBase(carry);

            } while (!prev.Contains(string.Join("", digits)) && !digits.All(d => d == 0));

            if (prev.Contains(string.Join("", digits)))
            {
                result = result.Insert(prev.IndexOf(string.Join("", digits)) + 1, "(");
                result += ")";
            }

            return result;

        }

        static string numberToDifferentBase(string number, int b1, int b2)
        {
            string result = "";

            if (number[0] == '-') {
                result = "-";
                number = number.Remove(0, 1);
            }

            string[] parts = number.Split('.', ',');

            result += wholeNumberToDifferentBase(parts[0], b1, b2);

            if(parts.Length == 2)
                result += fractionToDifferentBase(parts[1], b1, b2);

            return result;

        }

        static void Main(string[] args)
        {
            int b1, b2;
            string x;

            Console.WriteLine("Introduceti numarul");
            x = Console.ReadLine();

            Console.WriteLine("Introduceti baza numarului (2<=b1<=16)");
            b1 = int.Parse(Console.ReadLine());

            Console.WriteLine("Introduceti baza tinta (2<=b2<=16)");
            b2 = int.Parse(Console.ReadLine());


            Console.WriteLine($"{numberToDifferentBase(x, b1, b2)}");

            Console.ReadKey();

        }
    }
}