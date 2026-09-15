namespace Eval;

class Program () {
   static void Main () {
      var evaluate = new Evaluator ();
      for (; ; ) {
         Console.Write ("> ");
         string input = Console.ReadLine ();
         if (input == null || input == "") break;
         try {
            Console.ForegroundColor = ConsoleColor.Yellow;
            double result = evaluate.Evaluate (input.Trim().ToLower());
            Console.WriteLine (Math.Round(result, 10));
         }
         catch (Exception e) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine (e.Message);
         }
         Console.ResetColor ();
      }
   }
}
