using System.Security.Claims;

namespace Eval;

class Token { }

class TNumber : Token { }

class TOperator : Token { }
class TLiteral : TNumber {
   public TLiteral(double value) => mValue = value;
   public double Value => mValue;
   readonly double mValue;
}
class TVariable : TNumber { }
class TOpBinary : TOperator {
   public TOpBinary (char op) => mOp = op;
   public char Op => mOp;
   readonly char mOp;
}
class TOpFunc : TOperator { }
class TEnd : Token { }
class TError : Token { }
class TPunctuation : Token { }