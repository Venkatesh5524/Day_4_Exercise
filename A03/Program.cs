class Program {
   static void Main () {
      char[] letters = { 'U', 'X', 'A', 'L', 'T', 'N', 'E' };
      string[] words = File.ReadAllLines ("words.txt");
      Dictionary<string, int> result = [];
      foreach (string word in words)
         if (word.Length >= 4 && word.Contains (letters[0]) && word.All (letters.Contains))
            result[word] = word.Length == 4 ? 1 : IsPanagram (word) ? word.Length + 7 : word.Length;
      int total = 0;
      foreach (var ans in result.OrderByDescending (x => x.Value).ThenBy (x => x.Key)) {
         if (IsPanagram(ans.Key)) Console.ForegroundColor = ConsoleColor.Green;
         else Console.ResetColor ();
         total += ans.Value;
         Console.WriteLine ($"{ans.Value,3}: {ans.Key}");
      }
      Console.WriteLine ("----");
      Console.WriteLine ($"{total,3}: Total");

      bool IsPanagram (string word) => letters.All (word.Contains);
   }
}
