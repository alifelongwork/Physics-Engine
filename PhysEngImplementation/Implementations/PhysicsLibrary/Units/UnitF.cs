using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace PhysicsLibrary
{
    public static class UnitFStaticStuff
    {
        public static UnitF ToUnitF(this Unit<float> unit)
        {
            return new UnitF(unit.Value, unit.UsingPixel, unit.ConversionFactor);
        }
    }

    public class UnitF : Unit<float>
    {
        public static UnitF Zero => new UnitF(0, PhysicsHelper.Instance.ConversionFactor);
        public static UnitF One => new UnitF(1, PhysicsHelper.Instance.ConversionFactor);
        public static UnitF ZeroSI => new UnitF(0, false, PhysicsHelper.Instance.ConversionFactor);
        public static UnitF OneSI => new UnitF(1, false, PhysicsHelper.Instance.ConversionFactor);
        public static UnitF NaN => new UnitF(float.NaN, PhysicsHelper.Instance.ConversionFactor);
        public static UnitF MaxValue => new UnitF(float.MaxValue, PhysicsHelper.Instance.ConversionFactor);
        public static UnitF MinValue => new UnitF(float.MinValue, PhysicsHelper.Instance.ConversionFactor);
        public static UnitF PositiveInfinity => new UnitF(float.PositiveInfinity, PhysicsHelper.Instance.ConversionFactor);
        public static UnitF NegativeInfinity => new UnitF(float.NegativeInfinity, PhysicsHelper.Instance.ConversionFactor);

        public UnitF(float pixelValue)
            : this (pixelValue, PhysicsHelper.Instance.ConversionFactor)
        {
        }
        public UnitF(float pixelValue, bool isPixelValue)
            : base(pixelValue, isPixelValue, PhysicsHelper.Instance.ConversionFactor)
        {
        }

        public UnitF(float pixelValue, float conversionFactor)
            : base(pixelValue, conversionFactor)
        {
        }

        public UnitF(float value, bool isPixelValue, float conversionFactor)
            : base(value, isPixelValue, conversionFactor)
        {
        }
        /*public static UnitF operator +(UnitF first, Unit<float> second)
        {
            return (UnitF)((Unit<float>)first + second);
        }
        public static UnitF operator +(Unit<float> first, UnitF second)
        {
            return (UnitF)(first + (Unit<float>)second);
        }*/
        public static UnitF operator +(UnitF first, UnitF second)
        {
            return ((Unit<float>)first + second).ToUnitF();
        }
        public static UnitF operator -(UnitF first, UnitF second)
        {
            return ((Unit<float>)first - second).ToUnitF();
        }
        public static UnitF operator *(UnitF first, UnitF second)
        {
            return ((Unit<float>)first * second).ToUnitF();
        }
        public static UnitF operator /(UnitF first, UnitF second)
        {
            return ((Unit<float>)first / second).ToUnitF();
        }

        public static UnitF operator +(UnitF first, float second)
        {
            return ((Unit<float>)first + second).ToUnitF();
        }
        public static UnitF operator -(UnitF first, float second)
        {
            return ((Unit<float>)first - second).ToUnitF();
        }
        public static UnitF operator *(UnitF first, float second)
        {
            return ((Unit<float>)first * second).ToUnitF();
        }
        public static UnitF operator /(UnitF first, float second)
        {
            return ((Unit<float>)first / second).ToUnitF();
        }

        public static implicit operator double(UnitF unit)
        {
            return unit.Value;
        }
        public static explicit operator int(UnitF unit)
        {
            return (int)unit.Value;
        }

        public override float Add(float first, float second)
        {
            return first + second;
        }
        public override float Subtract(float first, float second)
        {
            return first - second;
        }
        public override float Multiply(float first, float second)
        {
            return first * second;
        }
        public override float Divide(float first, float second)
        {
            return first / second;
        }
        public override float Negate(float first)
        {
            return -first;
        }
    }
}

