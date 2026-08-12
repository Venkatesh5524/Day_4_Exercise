using static System.Console;
class Program {
   static void Main () {
      int low = 1, high = 100, guess, attempts = 0;
      Write ($"Think of a number between {low} and {high}: \nPress enter when you are ready");
      while (ReadKey (true).Key != ConsoleKey.Enter) ;
      ConsoleKey response;
      while (low <= high) {
         guess = (low + high) / 2;
         Write ($"\nIs your number {guess} (Y)es (H)igh (L)ow: ");
         response = IsVaildGuess ();
         attempts++;
         WriteLine (response == ConsoleKey.Y ? "Yes" : response == ConsoleKey.L ? "Low" : "High");
         switch (response) {
            case ConsoleKey.Y:
               WriteLine ($"I guessed the number in {attempts} attempts.");
               return;
            case ConsoleKey.L:
               low = guess + 1; break;
            default:
               high = guess - 1; break;
         }
      }
      static ConsoleKey IsVaildGuess () {
         ConsoleKey key;
         while (!((key = ReadKey(true).Key) is ConsoleKey.Y or ConsoleKey.L or ConsoleKey.H)) ;
         return key;
      }
   }
}
