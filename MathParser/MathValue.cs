using System;
using System.Collections.Generic;

namespace MathParser
{

    public class MathValue
    {
        private decimal _value;
        public decimal Value
        {
            get => GetValue();
            private set
            {
                _value = value;
            }
        }

        protected virtual decimal GetValue()
        {
            return _value;
        }

        public MathValue() { }
        public MathValue(decimal value)
        {
            _value = value;
        }

    }
}