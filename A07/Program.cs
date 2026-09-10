using static System.Console;
class Program {
   static void Main() {
      string? num = ReadLine();
      bool chek = DoubleParse (num, out double doble);
      //bool isDouble = double.TryParse(num, out double res);
      if (chek) WriteLine (doble);
      else WriteLine ("Invalid number");
   }

   static bool DoubleParse (string n, out double res) {
      res = 0;
      if (n == null) return false;
      int index = 0;
      int sign = 1;
      int digits = 0;
      while(index < n.Length) {
         if (index == 0)
            if (n[index] == '+' || n[index] == '-')
               if (n[index] == '-') {
                  sign = -1;
                  index++;
               }
         while(index < n.Length && char.IsDigit (n[index])) {
            res = (n[index] - '0') + res*10;
            digits++;
            index++;
         }
         if (digits == 0 && n[index] != '.') return false;
         double dec = 0;
         int deccount = 0;
         if (index < n.Length && n[index] == '.') {
            dec = 0.1;
            index++;
         }
         while(index < n.Length && char.IsDigit (n[index])) {
            res = (n[index] - '0') * dec;
            dec /= 10;
            deccount++;
            index++;
         }
         if(deccount == 0) return false;
      }
      return true;
   }
}