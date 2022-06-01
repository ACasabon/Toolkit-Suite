using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolkitSuite.Models
{
    public enum ValuesOperator
    {
        And,
        Or,
        Greater,
        GreaterOrEqual,
        Less,
        LessOrEqual,
        Equals,
        Multiply,
        Add,
        Subtract
    }

    public enum ConditionsOperator
    {
        Or,
        Greater,
        GreaterOrEqual,
        Less,
        LessOrEqual,
        Equals,
        Distinct,
    }

    public enum MotorVarsOperator
    {
        And,
        Or
    }
}
