namespace Exam
{
    public abstract class Expression
    {
        public abstract double Evaluate();
    }

    public abstract class BinaryExpression : Expression
    {
        public Expression Left;
        public Expression Right;
        protected BinaryExpression(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }
    }

    public class Sum : BinaryExpression
    {
        public Sum(Expression left, Expression right) : base(left, right) { }

        public override double Evaluate()
        {
            return Left.Evaluate() + Right.Evaluate();
        }
    }

    public class Mul : BinaryExpression
    {
        public Mul(Expression left, Expression right) : base(left, right) { }
        public override double Evaluate()
        {
            return Left.Evaluate() * Right.Evaluate();
        }
    }
    public class Pow : BinaryExpression
    {
        public Pow(Expression left, Expression right) : base(left, right) { }
        public override double Evaluate()
        {
            return Math.Pow(Left.Evaluate(), Right.Evaluate());
        }
    }

    public class Log : BinaryExpression
    {
        public Log(Expression left, Expression right) : base(left, right) { }
        public override double Evaluate()
        {
            if (Left.Evaluate() <= 0 || Right.Evaluate() == 0) throw new Exception("Log no definido");
            return Math.Log(Right.Evaluate(), Left.Evaluate());
        }
    }
    public class Div : BinaryExpression
    {
        public Div(Expression left, Expression right) : base(left, right) { }
        public override double Evaluate()
        {
            if (Right.Evaluate() == 0)
                throw new Exception("No se puede dividir por cero");
            else
                return Left.Evaluate() / Right.Evaluate();
        }
    }

    public class Sub : BinaryExpression
    {
        public Sub(Expression left, Expression right) : base(left, right) { }
        public override double Evaluate()
        {
            return Left.Evaluate() - Right.Evaluate();
        }
    }

    public class Constant : Expression
    {
        public double Value { get; }
        public Constant(double value)
        {
            Value = value;
        }
        public override double Evaluate()
        {
            return Value;
        }
    }

    public class Let : Expression
    {

        string variable { get; set; }
        Expression Left { get; set; }
        Expression Right { get; set; }
        public static Dictionary<string, double> context = new();
        public Let(string variable, Expression left, Expression right)
        {
            this.variable = variable;
            Left = left;
            Right = right;
        }
        public override double Evaluate()
        {
            double value = Left.Evaluate();
            context[variable] = Left.Evaluate();
            double result = Right.Evaluate();
            return result;
        }
    }

    public class Variable:Expression
    {
        public string Name { get; }
        public double Value { get; set; }
        public Variable(string name)
        {
            Name = name;
        }
        public override double Evaluate()
        {
            if (!Let.context.ContainsKey(Name)) throw new Exception("Variable no inicializada");
            return Let.context[Name];
        }
    }
}