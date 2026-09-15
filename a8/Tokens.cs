using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace Eval;

class Token { }

class TNumber : Token { }

class TOperator : Token { }
class TLiteral : TNumber {
   public TLiteral(double value) => mValue = value;
   public double Value => mValue;
   readonly double mValue;
   public override string ToString () => $"literal : {Value}";
}
class TVariable : TNumber {
   public TVariable (string name) => mName = name;
   public string Name => mName;
   readonly string mName;
   public override string ToString () => $"Variable : {mName}";
}
class TOpBinary : TOperator {
   public TOpBinary (char op) => mOp = op;
   public char Op => mOp;
   readonly char mOp;
   public override string ToString () => $"Operator : {Op}";
}
class TOpFunc : TOperator {
   public TOpFunc(string func) => mFunc = func;
   public string Func => mFunc;
   readonly string mFunc;
   public override string ToString () => $"Function : {mFunc}";
}
class TEnd : Token { }
class TError : Token { }
class TPunctuation : Token {
   public TPunctuation (char p) => mP = p;
   public char Punct => mP;
   readonly char mP;
   public override string ToString () => $"Punctuation : {Punct}";
}