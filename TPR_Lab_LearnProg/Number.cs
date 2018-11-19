using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPR_Lab_LearnProg
{
    /*
    public struct Number : IComparable, IComparable<Number>, IEquatable<Number>
    {
        public IComparable Value { get; set; }
        
        public Number(IComparable value)
        {
            if (value.IsNumber())
                Value = value;
            else throw new Exception("WrongObjectType: value isn't Number!");
        }

        #region Overridden
        public override string ToString()
        {
            return Value.ToString();
        }

        public override bool Equals(object other)
        {
            return Value.Equals(other);
        }
        #endregion

        public int CompareTo(object obj)
        {
            return Value.CompareTo(obj);
        }

        public int CompareTo(Number other)
        {
            return Value.CompareTo(other.Value);
        }

        public bool Equals(Number other)
        {
            return Value.Equals(other.Value);
        }

        #region Operators
        public static Number operator +(Number num1, Number num2)
        {
            return new Number((dynamic)num1.Value + (dynamic)num2.Value);
        }

        //public static implicit operator Number(object value)
        //{
        //    return new Number(value);
        //}
        #endregion
    }

    static class ObjectExtension
    {
        public static bool IsNumber(this Object obj)
        {
            switch (Type.GetTypeCode(obj.GetType()))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }
    }
    */
}
