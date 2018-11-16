using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPR_Lab_LearnProg
{
    public class Number : IComparable, IComparable<Number>, IEquatable<Number>
    {
        public object Value { get; private set; }

        public Number(object value)
        {
            if (value.IsNumber())
                Value = value;
            else throw new Exception("WrongObjectType: value isn't Number!");
        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public int CompareTo(Number other)
        {
            throw new NotImplementedException();
        }

        public bool Equals(Number other)
        {
            throw new NotImplementedException();
        }
    }

    static class ObjectExtension
    {
        public static bool IsNumber(this Object obj)
        {
            Type t = obj.GetType();
            return t == typeof(Int16) || t == typeof(Int32) || t == typeof(Int64) ||
                t == typeof(UInt16) || t == typeof(UInt32) || t == typeof(UInt64) ||
                t == typeof(Double) || t == typeof(Decimal) || t == typeof(SByte) ||
                t == typeof(Single) || t == typeof(Byte);
        }
    }
}
