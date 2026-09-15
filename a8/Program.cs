namespace Eval;

class Program () {
   static void Main () {
      var evaluate = new Evaluator ();
      for (; ; ) {
         Console.Write ("> ");
         string input = Console.ReadLine ();
         if (input == null || input == "exit") break;
         try {
            Console.ForegroundColor = ConsoleColor.Yellow;
            string result = evaluate.Evaluate (input.Trim().ToLower());
            Console.WriteLine (result);
         }
         catch (Exception e) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine (e.Message);
         }
         Console.ResetColor ();
      }
   }
}
