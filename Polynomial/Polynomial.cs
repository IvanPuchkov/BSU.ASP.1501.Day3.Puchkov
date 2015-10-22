using System;
using System.Linq;
using System.Text;

namespace Polynomial
{
    public class Polynomial
    {
        public int Length => _array.Length;

        private readonly double[] _array;


        public Polynomial(int size)
        {
            _array=new double[size];
        }

        public Polynomial(double[] array)
        {
            if (array==null)
                throw new ArgumentNullException();
            _array = array;
        }

        public double[] ToArray() => _array.ToArray();

        public double this[int index]
        {
            get
            {
                if ((index<0)||(index>_array.Length))
                    throw new ArgumentOutOfRangeException();
                return _array[index];
            }
            set
            {
                if ((index < 0) || (index >= _array.Length))
                    throw new ArgumentOutOfRangeException();
                _array[index] = value;
            }
        }

        public static Polynomial Add(Polynomial a, Polynomial b)
        {
            if ((a==null)||(b==null))
                throw new ArgumentNullException();
            double[] firstArray = a.ToArray();
            Array.Reverse(firstArray);
            double[] secondArray = b.ToArray();
            Array.Reverse(secondArray);
            double[] resultArray=new double[Math.Max(a.Length,b.Length)];
            for (int i = 0; i < firstArray.Length; i++)
                resultArray[i] = firstArray[i];
            for (int i = 0; i < secondArray.Length; i++)
                resultArray[i] += secondArray[i];
            Array.Reverse(resultArray);
            return new Polynomial(resultArray);
        }

        public static Polynomial Substract(Polynomial a, Polynomial b)
        {
            if ((a == null) || (b == null))
                throw new ArgumentNullException();
            Polynomial c=new Polynomial(b.ToArray());
            for (int i = 0; i < c.Length; i++)
                c[i] = -c[i];
            return Add(a,c);
        }

        public static Polynomial Multiply(Polynomial polynomial, double value)
        {
            if (polynomial==null)
                throw new ArgumentNullException();
            Polynomial result=new Polynomial(polynomial.Length);
            for (int i = 0; i < polynomial.Length; i++)
            {
                result[i] = polynomial[i]*value;
            }
            return result;
        }

        public static Polynomial Multiply(Polynomial a, Polynomial b)
        {
            if ((a == null) || (b == null))
                throw new ArgumentNullException();
            var result=new Polynomial(a.Length+b.Length-1);
            for (int i=0;i<a.Length;i++)
                for (int j = 0; j < b.Length; j++)
                {
                    result[i + j] = result[i+j]+a[i]*b[j];
                }
            return result;
        }
        

        public Polynomial Multiply(Polynomial value) => Polynomial.Multiply(this, value);

        public Polynomial Add(Polynomial value) => Polynomial.Add(this, value);

        public Polynomial Substract(Polynomial value) => Polynomial.Substract(this, value);

        public static Polynomial operator *(Polynomial a, Polynomial b)=>Multiply(a, b);

        public static Polynomial operator *(Polynomial polynomial, double number) => Multiply(polynomial, number);

        public static Polynomial operator *(double number, Polynomial polynomial) => Multiply(polynomial, number);

        public static Polynomial operator +(Polynomial a, Polynomial b) => Add(a, b);

        public static Polynomial operator -(Polynomial a, Polynomial b) => Substract(a, b);

        public static bool operator ==(Polynomial a, Polynomial b)
        {
            if (object.ReferenceEquals(a, b))
            {
                return true;
            }
            
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }
            return a.Equals(b);
        }

        public static bool operator !=(Polynomial a, Polynomial b) => !(a == b);
        

        public override string ToString()
        {
            StringBuilder sb=new StringBuilder();
            for (int i = 0; i < _array.Length;i++)
            {
                int power = _array.Length - i - 1;
                if (i > 0)
                    sb.Append(" + ");
                sb.Append(_array[i]);
                if (power > 0)
                {
                    sb.Append("*a");
                    if (power > 1)
                    {
                        sb.Append('^');
                        sb.Append(_array.Length - i-1);
                    }
                }
            }
            return sb.ToString()==string.Empty ? "0" : sb.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj == this)
                return true;
            if (obj == null)
                return false;
            Polynomial polynomial=obj as Polynomial;
            if (polynomial == null)
                return false;
            if (this.Length != polynomial.Length)
                return false;
            for (int i=0;i<this.Length;i++)
                if (Math.Abs(this[i] - polynomial[i])>0.0000001)
                    return false;
            return true;
        }

        

        public override int GetHashCode()
        {
            int sum = 0;
            for (int i = 0; i < _array.Length; i++)
                sum += i;
            return sum;
        }
    }
}
