using Restaurant365Challenge.Shared.Interfaces;

namespace Restaurant365Challenge.Shared
{
    public class StringEvaluator : IStringEvaluator
    {
        private ILog _log;
        public StringEvaluator(ILog log)
        {
            _log = log;
        }
        /*Support a maximum of 2 numbers using a comma delimiter. 
         *  Throw an exception when more than 2 numbers are provided
            examples: 20 will return 20; 1,5000 will return 5001; 4,-3 will return 1
            empty input or missing numbers should be converted to 0
            invalid numbers should be converted to 0 e.g. 5,tytyt will return 5 */
        public int EvaluateStringExpression(string expression)
        {
            var result = 0;
            try
            {
                var convertedExpression = expression.Split(',');
                if (convertedExpression.Length > 2)
                {
                    throw new Exception("Unable to evaluate expression due to too many numbers");
                }
                foreach (var value in convertedExpression)
                {
                    if (String.IsNullOrEmpty(value) || !int.TryParse(value, out int converteredValue))
                    {
                        //Empty Values  or non numbers will be treated as 0's
                        converteredValue = 0;
                    }

                    result += converteredValue;

                }

            }
            catch (Exception ex)
            {

                throw;
                //ToDo: Log this rather than throw it back to the user
               // _log.Log("An error arose while evaulating expression:", ex);
            }

            return result;
        }
    }
}