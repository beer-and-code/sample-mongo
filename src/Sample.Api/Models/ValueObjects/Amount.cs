using System.Globalization;

namespace Sample.Api.Models.ValueObjects
{
    public class Amount
    {
        public Amount()
        {
        }

        public Amount(decimal value)
        {
            Value = value;
        }

        public decimal Value { get; set; }

        public override string ToString()
        {
            return Value.ToString(CultureInfo.InvariantCulture);
        }


        public static implicit operator Amount(decimal value)
        {
            return new Amount(value);
        }

        public static implicit operator decimal(Amount value)
        {
            return value.Value;
        }

        public static Amount operator +(Amount amount1, Amount amount2)
        {
            return new Amount(amount1.Value + amount2.Value);
        }

        public static Amount operator -(Amount amount1, Amount amount2)
        {
            return new Amount(amount1.Value - amount2.Value);
        }

        public static bool operator <(Amount amount1, Amount amount2)
        {
            return amount1.Value < amount2.Value;
        }

        public static bool operator >(Amount amount1, Amount amount2)
        {
            return amount1.Value > amount2.Value;
        }

        public static bool operator <=(Amount amount1, Amount amount2)
        {
            return amount1.Value <= amount2.Value;
        }

        public static bool operator >=(Amount amount1, Amount amount2)
        {
            return amount1.Value >= amount2.Value;
        }
    }
}