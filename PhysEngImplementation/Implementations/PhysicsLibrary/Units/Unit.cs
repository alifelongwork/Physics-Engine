using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PhysicsLibrary
{
    public class Unit<T> where T : IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
    {
        public override string ToString()
        {
            return $"p:{PixelValue}  si:{SIValue}";
        }

        public T Value { get; set; }

        public T PixelValue
        {
            get
            {
                if (UsingPixel)
                {
                    return Value;
                }
                else
                {
                    return Multiply(Value, ConversionFactor);
                }
            }
            set
            {
                if (UsingSI)
                {
                    value = Divide(value, ConversionFactor);
                }
                Value = value;
            }
        }

        public T SIValue
        {
            get
            {
                if (UsingSI)
                {
                    return Value;
                }
                else
                {
                    return Divide(Value, ConversionFactor);
                }
            }
            set
            {
                if (UsingPixel)
                {
                    value = Multiply(value, ConversionFactor);
                }
                Value = value;
            }
        }

        public bool UsingPixel { get; set; }
        public bool UsingSI
        {
            get
            {
                return !UsingPixel;
            }
            private set
            {
                UsingPixel = !value;
            }
        }

        public T ConversionFactor { get; internal set; } //convert pixel to SI? or SI to pixel?

        public Unit(T value, bool isPixelValue, T conversionFactor)
        {
            UsingPixel = isPixelValue;
            Value = value;
            ConversionFactor = conversionFactor;
        }
        public Unit(T pixelValue, T conversionFactor)
            : this(pixelValue, true, conversionFactor)
        { }

        public void SwitchToOther()
        {
            if (UsingPixel)
            {
                SwitchToSI();
            }
            else
            {
                SwitchToPixel();
            }
        }
        public void SwitchToSI()
        {
            if (UsingPixel)
            {
                Value = SIValue;
                UsingSI = true;
            }
        }
        public void SwitchToPixel()
        {
            if (UsingSI)
            {
                Value = PixelValue;
                UsingPixel = true;
            }
        }

        public virtual T Add(T first, T second)
        {
            return Expression.Lambda<Func<T>>(Expression.Add(Expression.Constant(first), Expression.Constant(second))).Compile().Invoke();
        }
        public virtual T Subtract(T first, T second)
        {
            return Expression.Lambda<Func<T>>(Expression.Subtract(Expression.Constant(first), Expression.Constant(second))).Compile().Invoke();
        }
        public virtual T Multiply(T first, T second)
        {
            return Expression.Lambda<Func<T>>(Expression.Multiply(Expression.Constant(first), Expression.Constant(second))).Compile().Invoke();
        }
        public virtual T Divide(T first, T second)
        {
            return Expression.Lambda<Func<T>>(Expression.Divide(Expression.Constant(first), Expression.Constant(second))).Compile().Invoke();
        }
        public virtual T Negate(T first)
        {
            return Expression.Lambda<Func<T>>(Expression.Negate(Expression.Constant(first))).Compile().Invoke();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public static bool operator ==(Unit<T> first, Unit<T> second)
        {
            bool usingPixel = first.UsingPixel;
            if (usingPixel)
            {
                return first.PixelValue.Equals(second.PixelValue);
            }
            else
            {
                return first.SIValue.Equals(second.SIValue);
            }
        }
        public static bool operator !=(Unit<T> first, Unit<T> second)
        {
            return !(first == second);
        }
        public static Unit<T> operator +(Unit<T> first)
        {
            return first;
        }
        public static Unit<T> operator -(Unit<T> first)
        {
            T newValue = first.Negate(first.Value);
            return new Unit<T>(newValue, first.UsingPixel, first.ConversionFactor);
        }
        public static Unit<T> operator +(Unit<T> first, T value)
        {
            T newValue = first.Add(first.Value, value);
            return new Unit<T>(newValue, first.UsingPixel, first.ConversionFactor);
        }
        public static Unit<T> operator -(Unit<T> first, T value)
        {
            T newValue = first.Subtract(first.Value, value);
            return new Unit<T>(newValue, first.UsingPixel, first.ConversionFactor);
        }
        public static Unit<T> operator *(Unit<T> first, T value)
        {
            T newValue = first.Multiply(first.Value, value);
            return new Unit<T>(newValue, first.UsingPixel, first.ConversionFactor);
        }
        public static Unit<T> operator /(Unit<T> first, T value)
        {
            T newValue = first.Divide(first.Value, value);
            return new Unit<T>(newValue, first.UsingPixel, first.ConversionFactor);
        }
        public static Unit<T> operator +(Unit<T> first, Unit<T> second)
        {
            T newValue = first.Add(first.PixelValue, second.PixelValue);

            return new Unit<T>(first.UsingPixel == true ? newValue : first.Divide(newValue, first.ConversionFactor), first.UsingPixel, first.ConversionFactor);
        }
        public static Unit<T> operator -(Unit<T> first, Unit<T> second)
        {
            T newValue = first.Subtract(first.PixelValue, second.PixelValue);
            return new Unit<T>(first.UsingPixel == true ? newValue : first.Divide(newValue, first.ConversionFactor), first.UsingPixel, first.ConversionFactor);
        }
        public static Unit<T> operator *(Unit<T> first, Unit<T> second)
        {
            T newValue = first.Multiply(first.PixelValue, second.PixelValue);
            return new Unit<T>(first.UsingPixel == true ? newValue : first.Divide(newValue, first.ConversionFactor), first.UsingPixel, first.ConversionFactor);
        }
        public static Unit<T> operator /(Unit<T> first, Unit<T> second)
        {
            T newValue = first.Divide(first.PixelValue, second.PixelValue);
            return new Unit<T>(first.UsingPixel == true ? newValue : first.Divide(newValue, first.ConversionFactor), first.UsingPixel, first.ConversionFactor);
        }

        public static implicit operator T(Unit<T> unit)
        {
            return unit.Value;
        }
    }
}

