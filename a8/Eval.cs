namespace Eval;
class EvalException : Exception {
   public EvalException (string message) : base (message) { }
}
class Evaluator () {
   public double Evaluate(string input) {
      Reset();
      Tokenizer tokenizer = new (this, input);
      List<Token> tokens = [];
      for(; ; ) {
         var token = tokenizer.GetNext ();
         if (token is TEnd) break;
         tokens.Add (token);
      }
      TVariable var = null;
      if (tokens.Count > 1 && tokens[0] is TVariable tvar && tokens[1] is TOpBinary bin && bin.Op == '=') {
         var = tvar;
         tokens.RemoveRange (0, 2);
      }

      foreach (var token in tokens) Process (token);
      while (mOperators.Count > 0) ApplyOperator ();
      if (mBasePriority != 0) Error ("Mismatched paranthesis");
      if (mOperators.Count > 0) Error ("Too many operators");
      if (mOperands.Count > 1) Error ("Too many operands");
      double f = mOperands.Pop ();
      if(var != null) mVariables[var.Name] = f;
      return f;
   }

   void ApplyOperator () {
      var op = mOperators.Pop();
      if(op is TOpBinary bin) {
         if (mOperands.Count < 2) Error ("Too few operands");
         double f1 = mOperands.Pop(), f2 = mOperands.Pop();
         mOperands.Push(bin.Apply (f2, f1));
      }
      if(op is TOpFunc func) {
         if (mOperands.Count < 1) Error ("Too few operands");
         double f = mOperands.Pop();
         mOperands.Push (func.Apply (f));
      }
   }
   public void Process(Token token) {
      switch (token) {
         case TNumber num:
            mOperands.Push (num.Value);
            break;
         case TOperator op:
            op.FinalPriority = op.Priority + mBasePriority;
            while (!OkToPush (op)) ApplyOperator ();
            mOperators.Push (op);
            break;
         case TPunctuation p:
            if (p.Punct == '(') mBasePriority += 10;
            else if(p.Punct == ')') mBasePriority -= 10;
            break;
         default: throw new NotImplementedException ();
      }
   }
   int mBasePriority = 0;

   void Reset () {
      mOperands.Clear ();
      mOperators.Clear ();
      mBasePriority = 0;
   }

   void Error (string s) => throw new EvalException (s);
   public bool OkToPush(TOperator op) {
      if(mOperators.Count == 0) return true;
      TOperator prev = mOperators.Peek ();
      return prev.FinalPriority <= op.FinalPriority;
   }

   public double GetVariable(string name) {
      if (mVariables.TryGetValue (name, out double value)) return value;
      Error ($"Unknown variable {name}");
      return 0;
   }
   Stack<double> mOperands = new ();
   Stack<TOperator> mOperators = new ();
   Dictionary<string, double> mVariables = [];
}