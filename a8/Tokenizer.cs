namespace Eval;

class Tokenizer {
   public Tokenizer (Evaluator eval,string input) {
      mText = input;
      mN = 0;
      mEval = eval;
   }
   readonly string mText;        // The input text we're parsing through
   readonly Evaluator mEval;     // The evaluator that owns this
   int mN;                       // Position within the text

   public Token GetNext () {
      while(mN < mText.Length) {
         char c = mText[mN++];
         switch (c) {
            case '+' or '-' or '*' or '/' or '^' or '=': return new TOpBinary (c);
            case ' ': continue;
            case >= '0' and <= '9': return GetLiteral ();
            case '(' or ')': return new TPunctuation (c);
            case (>= 'a' and <= 'z') or (>= 'A' and <= 'Z'): return GetIdentifier ();
            default: return new TError ($"Unexpected character {c}");
         }
      }
      return new TEnd ();
   }

   Token GetLiteral () {
      int start = mN - 1;
      while(mN < mText.Length) {
         char ch = mText[mN++];
         if (!(char.IsDigit (ch) || ch == '.')) { mN--; break; }
      }
      string number = mText[start..mN];
      double num = double.Parse (number);
      return new TLiteral (num);
   }

   Token GetIdentifier () {
      int start = mN - 1;
      while (mN < mText.Length) {
         char ch = mText[mN++];
         if (!(char.IsDigit (ch) || char.IsLetter(ch))) { mN--; break; }
      }
      string identifier = mText[start..mN];
      if (mFuncs.Contains (identifier)) return new TOpFunc (identifier);
      else return new TVariable (mEval, identifier);
   }
   readonly string[] mFuncs = { "sin", "cos", "tan", "sqrt", "log", "exp", "asin", "acos", "atan" };
}