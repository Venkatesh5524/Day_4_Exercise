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
         if (c is '+' or '-' or '*' or '/' or '^') return new TOpBinary (c);
         if (c is ' ') continue;
         if (c is >= '0' and <= '9') return GetLiteral ();
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
      string number = mText.Substring(start, mN - start - 1);
      double num = double.Parse (number);
      return new TLiteral (num);
   }
}