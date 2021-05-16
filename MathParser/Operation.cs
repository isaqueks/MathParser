using System;
using System.Collections.Generic;
using System.Text;

namespace MathParser
{

    public class Operation : MathValue
    {
        public MathValue A;
        public MathValue B;
        public OperationType OpType;

        public Operation() { }

        public Operation(MathValue a, MathValue b, OperationType type) : base()
        {
            A = a;
            B = b;
            OpType = type;
        }

        public Operation(MathValue a, MathValue b) : this(a, b, OperationType.Sum)
        {

        }

        protected override decimal GetValue()
        {
            decimal a = A.Value;
            decimal b = B.Value;

            return OperatorUtils.Solve(OpType, a, b);
        }
    }

}