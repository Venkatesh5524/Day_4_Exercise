using System.Text;
using static System.Console;

class Program {
   static void Main () {
      Console.OutputEncoding = new UnicodeEncoding ();
      Write ("Enter Number of rows: ");
      int n = ValidInt ();
      List<List<int>> results = [];
      List<int> rows = [];
      Solve (rows);
      WriteLine ($"Total solutions for {n} queens: {results.Count}");
      Write ("Press Enter to print the solution... ");
      while (!(ReadKey (true).Key == ConsoleKey.Enter)) ;
      int solution = 0;
      while (solution < results.Count) {
         rows = results[solution];
         WriteLine ($"\nSolution {solution + 1} of {results.Count}");
         DisplayBoard (n);
         Write ("\nPress \u279C to see the next solution... " +
            "\nPress \u2190 to see the previous solution... \nPress esc to exit... ");
         ConsoleKey key;
         while (!((key = ReadKey (true).Key) == ConsoleKey.RightArrow || key == ConsoleKey.LeftArrow || key == ConsoleKey.Escape)) ;
         if (key == ConsoleKey.Escape) break;
         solution = key == ConsoleKey.RightArrow ? solution < results.Count - 1
                                                 ? solution + 1 : solution : solution > 0 ? solution - 1 : 0;
      }

      void Solve (List<int> rows) {
         if (rows.Count == n) {
            results.Add ([.. rows]);
            return;
         }
         for (int col = 0; col < n; col++) {
            if (IsSafe (rows.Count, col)) {
               rows.Add (col);
               Solve (rows);
               rows.RemoveAt (rows.Count - 1);
            }
         }

         bool IsSafe (int row, int col) {
            for (int i = 0; i < rows.Count; i++)
               if (rows[i] == col || Math.Abs (col - rows[i]) == row - i) return false;
            return true;
         }
      }



      void DisplayBoard (int num) {
         const string TLC = "┌", TE = "────┬", TRC = "────┐", ML = "├", ME = "────┼", MR = "────┤",
                      BLC = "└", BE = "────┴", BRC = "────┘", VERT = "│", EMPTY = "    │", QUEEN = " ♕  │";
         for (int i = 0; i <= num * 2; i++) {
            if (i == 0) WriteLine (TLC + string.Join ("", Enumerable.Repeat (TE, num - 1)) + TRC);
            else if (i == num * 2) WriteLine (BLC + string.Join ("", Enumerable.Repeat (BE, num - 1)) + BRC);
            else if (i % 2 == 0) WriteLine (ML + string.Join ("", Enumerable.Repeat (ME, num - 1)) + MR);
            else
               WriteLine (VERT + string.Join ("", Enumerable.Range (1, num).Select (j => rows[i / 2] == j - 1 ? QUEEN : EMPTY)));
         }
      }

      int ValidInt () {
         for (; ; ) {
            if (int.TryParse (Console.ReadLine (), out int n)) return n;
            Console.Write ("Enter a valid number: ");
         }
      }
   }
}