namespace ProductApplication.Domain.Validations
{
    internal static class CnpjValidator
    {
        public static bool Validate(string cnpj)
        {
            cnpj = cnpj?.Trim();
            cnpj = cnpj?.Replace(".", "").Replace("-", "").Replace("/", "");

            if (string.IsNullOrWhiteSpace(cnpj) || cnpj.Length != 14)
            {
                return false;
            }

            var tempCnpj = cnpj.Substring(0, 12);
            var digit1 = CalculateDigit(tempCnpj);
            var digit2 = CalculateDigit(tempCnpj + digit1);

            return cnpj.EndsWith(digit1 + digit2);
        }

        private static string CalculateDigit(string tempCnpj)
        {
            var multiplier = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            var sum = 0;
            var sizeCnpj = tempCnpj.Length;
            var shift = multiplier.Length - sizeCnpj;

            for (int i = 0; i < sizeCnpj; i++)
            {
                sum += int.Parse(tempCnpj[i].ToString()) * multiplier[i + shift];
            };

            return CheckRest(sum).ToString();
        }

        private static int CheckRest(int sum)
        {
            var rest = (sum % 11);

            if (rest < 2)
            {
                rest = 0;
            }
            else
            {
                rest = 11 - rest;
            }
            return rest;
        }
    }
}
