using System.Data;
using System.Diagnostics;

namespace Eval;
class EvalException : Exception {
   public EvalException (string message) : base (message) { }
}
class Evaluator () {
   public string Evaluate(string input) {
      Tokenizer tokenizer = new (input);
      for(; ; ) {
         var token = tokenizer.GetNext ();
         if (token is TEnd) break;
         Process (token);
      }
      return "ok";
   }

   public void Process(Token token) {
      Console.WriteLine (token);
   }
}