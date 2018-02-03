namespace KidsPos.Sources.Util
{
    public class Barcode
    {
        public static string CreateCheckDigit(string barcode)
        {
            var even = 0;
            var odd = 0;

            for (var i = 0; i < barcode.Length; i++)
                if (i == 0 || i % 2 == 0) odd += int.Parse(barcode[i].ToString());
                else even += int.Parse(barcode[i].ToString());

            var checkDigit = 10 - (even * 3 + odd) % 10;
            if (checkDigit == 10) checkDigit = 0;

            return checkDigit.ToString();
        }
    }
}