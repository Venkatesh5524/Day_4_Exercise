

namespace Eval;

class Token { }

class TNumber : Token {
   public virtual double Value {  get;}
}

class TOperator : Token {
   public virtual int Priority { get;}
   public  int FinalPriority { get;  set;}
}
class TLiteral : TNumber {
   public TLiteral(double value) => mValue = value;
   public override double Value => mValue;
   readonly double mValue;
   public override string ToString () => $"literal : {Value}";
}
class TVariable : TNumber {
   public TVariable (Evaluator eval,string name) { mName = name; mEval = eval; }
   public string Name => mName;
   readonly string mName;
   readonly Evaluator mEval;
   public override double Value => mEval.GetVariable(Name);
   public override string ToString () => $"Variable : {mName}";
}
class TOpBinary : TOperator {
   public TOpBinary (char op) => mOp = op;
   public override int Priority
          => Op switch {
            '-' => 1,
            '+' => 2,
            '*' => 3,
            '/' => 4,
            '^' => 5,
            _ => throw new NotImplementedException (),
         };
   public char Op => mOp;
   readonly char mOp;
   public override string ToString () => $"Operator : {Op}";
   public double Apply (double a, double b)
      => Op switch {
         '+' => a + b,
         '-' => a - b,
         '*' => a * b,
         '/' => a / b,
         '^' => Math.Pow (a, b),
         _ => throw new NotImplementedException (),
      };
}
class TOpFunc : TOperator {
   public TOpFunc(string func) => mFunc = func;
   public string Func => mFunc;
   readonly string mFunc;
   public override string ToString () => $"Function : {mFunc}";
   public override int Priority => 6;
   public double Apply(double f) {
      return Func switch {
         "sin" => Math.Sin (D2R (f)),
         "cos" => Math.Cos (D2R (f)),
         "tan" => Math.Tan (D2R (f)),
         "log" => Math.Log (f),
         "exp" => Math.Exp (f),
         "sqrt" => Math.Sqrt (f),
         "asin" => R2D (Math.Asin (f)),
         "acos" => R2D (Math.Acos (f)),
         "atan" => R2D (Math.Atan (f)),
         _ => throw new NotImplementedException (),
      };
      double D2R (double f) => f * Math.PI / 180;
      double R2D (double f) => f * 180 / Math.PI;
   }
}
class TEnd : Token { }
class TError : Token {
   public TError(string msg) => mMessage = msg;
   public string Message => mMessage;
   readonly string mMessage;
   public override string ToString () => $"Error : {Message}";
}
class TPunctuation : Token {
   public TPunctuation (char p) => mP = p;
   public char Punct => mP;
   readonly char mP;
   public override string ToString () => $"Punctuation : {Punct}";
}