using System;
using System.Collections.Generic;

public class MathValue
{
    private decimal m_Value;
    public decimal Value
    {
        get => GetValue();
        private set
        {
            m_Value = value;
        }
    }

    private virtual decimal GetValue()
    {
        return m_Value;
    }

    public MathValue() { }
    public MathValue(decimal value)
    {
        m_Value = value;
    }

}
