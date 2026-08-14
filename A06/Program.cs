using System.Text;
using static System.Console;

class Program {
   static void Main () {
      Write ("Enter Number of rows: ");
      int.TryParse (ReadLine (), out int n);
      //Write (string.Join("", Enumerable.Repeat("♕",n)));
      Write ($"┌{string.Join ("", Enumerable.Repeat ("────┬", n - 1))}────┐");
      //Write (string.Concat (n));
      //DisplayBoard (n);
   }

   static void DisplayBoard (int num) {
      OutputEncoding = new UnicodeEncoding ();
      const string TOPPATTERN = "┌┬┐";
      const string MIDPATTERN = "├┼┤";
      const string BOTPATTERN = "└┴┘";
      const string HORILINE = "────";
      const string VERTLINE = "│";
      const string QUEEN = " ♕  ";
      const string EMPTY = "    ";

      for (int i = 0; i < num + 1; i++) {
         if (i == 0) Write ($"┌{string.Join("", Enumerable.Repeat("────┬", num - 1))}────┐");
         Write (i == num - 1 ? "│\n└" : "│\n├");
         for (int j = 0; j < num; j++) {
            if (i == 0) Write (($"┌{string.Join ("", Enumerable.Repeat ("────┬", num - 1))}────┐"));
            else if (i == num - 1) Write (j == num - 1 ? HORILINE + BOTPATTERN[2] : HORILINE + BOTPATTERN[1]);
            else if (i % 2 == 0) Write (j == num - 1 ? HORILINE + MIDPATTERN[2] : HORILINE + MIDPATTERN[1]);
            else Write (EMPTY + VERTLINE);
         }
         WriteLine ();
      }
   }
}
