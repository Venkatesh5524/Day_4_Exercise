namespace Eval;

class Tokenizer {
   public Tokenizer (string input) {
      mText = input;
      mN = 0;
   }
   readonly string mText;
   int mN;

   public Token GetNext () {
      while(mN < mText.Length) {
         char c = mText[mN++];
         if (c is '+' or '-' or '*' or '/' or '^' or '=') return new TOpBinary (c);
         if (c is ' ') continue;
         if (c is >= '0' and <= '9') return GetLiteral ();
         if (c is '(' or ')') return new TPunctuation (c);
         if (c is >= 'a' and <= 'z') return GetIdentifier ();
         return new TError ();
      }
      return new TEnd ();
   }

   Token GetLiteral () {
      int start = mN - 1;
      while(mN < mText.Length) {
         char ch = mText[mN++];
         if (!(char.IsDigit (ch) || ch == '.')) { mN--; break; }
      }
      string number = mText.Substring(start, mN - start);
      double num = double.Parse (number);
      return new TLiteral (num);
   }

   Token GetIdentifier () {
      int start = mN - 1;
      while (mN < mText.Length) {
         char ch = mText[mN++];
         if (!(char.IsDigit (ch) || char.IsLetter(ch))) { mN--; break; }
      }
      string identifier = mText.Substring (start, mN - start);
      if (mFuncs.Contains (identifier)) return new TOpFunc (identifier);
      else return new TVariable (identifier);
   }
   readonly string[] mFuncs = { "sin", "cos", "tan", "sqrt", "log", "exp", "asin", "acos", "atan" };
}