using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Mfti
{
    public class Number
    {
        private string _number;
        private int[] _vector;

        public Number(string number)
        {
            _number = number;

            if (number.StartsWith("-"))
            {
                IsNegative = true;
                _vector = new int[number.Length - 1];
                for (int i = 0, j = number.Length - 1; i < number.Length - 1; i++, j--)
                    _vector[i] = int.Parse(number[j].ToString());
            }
            else
            {
                _vector = new int[number.Length];
                for (int i = 0, j = number.Length - 1; i < number.Length; i++, j--)
                    _vector[i] = int.Parse(number[j].ToString());
            }
        }

        public Number(bool isNegative, int[] vector)
        {
            IsNegative = isNegative;

            _vector = new int[vector.Length];
            for (int i = 0; i < vector.Length; i++)
                _vector[i] = vector[i];

            StringBuilder numberSb;
            if (isNegative)
            {
                numberSb = new StringBuilder(vector.Length + 1);
                numberSb.Append("-");
            }
            else
                numberSb = new StringBuilder(vector.Length);

            for (int i = vector.Length - 1; i >= 0; i--)
                numberSb.Append(vector[i].ToString());
            _number = numberSb.ToString();
        }

        public bool IsNegative { get; }
        public int Length => _vector.Length;

        public override string ToString() => _number;

        public int this[int i]
        {
            get { return _vector[i]; }
        }

        public static Number operator +(Number a, Number b)
        {
            if (a.IsNegative && b.IsNegative)
            {
                return new Number(true, SumVectors(a, b));
            }

            if (a.IsNegative)
                return DifferenceVectors(b, a);

            if (b.IsNegative)
                return DifferenceVectors(a, b);

            return new Number(false, SumVectors(a, b));
        }

        public static Number operator *(Number a, Number b)
        {
            if (a.IsNegative && b.IsNegative || !a.IsNegative && !b.IsNegative)
                return new Number(false, MultipleVectors(a, b));

            return new Number(true, MultipleVectors(a, b));
        }

        private static int[] SumVectors(Number a, Number b)
        {
            int length = a.Length > b.Length ? a.Length : b.Length;
            var tmpVector = new List<int>(length);

            int carry = 0;

            for (int i = 0; i < length; i++)
            {
                int aVal = i < a.Length ? a[i] : 0;
                int bVal = i < b.Length ? b[i] : 0;
                int tmpSum = aVal + bVal + carry;
                if (tmpSum > 9)
                {
                    tmpVector.Add(tmpSum % 10);
                    carry = 1;
                }
                else
                {
                    tmpVector.Add(tmpSum);
                    carry = 0;
                }
            }

            if (carry > 0)
                tmpVector.Add(carry);

            return tmpVector.ToArray();
        }

        private static Number DifferenceVectors(Number a, Number b)
        {
            int compare = CompareVerctors(a, b);
            if (compare == 0)
                return new Number(false, new[] { 0 });

            if (compare > 0)
                return new Number(false, DifferenceVectorsImpl(a, b));

            return new Number(true, DifferenceVectorsImpl(b, a));
        }

        private static int[] DifferenceVectorsImpl(Number a, Number b)
        {
            int length = a.Length > b.Length ? a.Length : b.Length;
            var tmpVector = new Stack<int>();

            bool carry = false;
            for (int i = 0; i < length; i++)
            {
                int aVal = i < a.Length ? a[i] : 0;
                int bVal = i < b.Length ? b[i] : 0;

                if (carry)
                {
                    if (aVal > bVal)
                    {
                        aVal -= 1;
                        carry = false;
                    }
                    else
                    {
                        aVal += 10;
                        bVal += 1;
                    }
                }

                if (aVal < bVal)
                {
                    carry = true;
                    aVal += 10;
                }

                tmpVector.Push(aVal - bVal);
            }

            while (tmpVector.Peek() == 0)
                tmpVector.Pop();

            return tmpVector.Reverse().ToArray();
        }

        private static int[] MultipleVectors(Number a, Number b)
        {
            var tmpVector = new int[a.Length + b.Length];
            int carry = 0;

            for (int i = 0; i < a.Length; i++)
            {
                for (int j = 0; j < b.Length || carry > 0; j++)
                {
                    int currentValue;
                    if (j < b.Length)
                        currentValue = tmpVector[i + j] + a[i] * b[j] + carry;
                    else
                        currentValue = tmpVector[i + j] + carry;

                    tmpVector[i + j] = currentValue % 10;
                    carry = currentValue / 10;
                }
            }

            int index = tmpVector.Length - 1;
            while (index > 0)
            {
                if (tmpVector[index] != 0)
                    break;
                index--;
            }

            if (index == tmpVector.Length - 1)
                return tmpVector;

            var result = new int[index + 1];
            for (int i = 0; i < result.Length; i++)
                result[i] = tmpVector[i];

            return result;
        }

        private static int CompareVerctors(Number a, Number b)
        {
            if (a.Length > b.Length)
                return 1;

            if (a.Length < b.Length)
                return -1;

            for (int i = a.Length - 1; i >= 0; i--)
            {
                int aVal = a[i];
                int bVal = b[i];

                if (aVal > bVal)
                    return 1;

                if (aVal < bVal)
                    return -1;
            }

            return 0;
        }
    }
}