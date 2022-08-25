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
        /*Remove the maximum constraint for numbers e.g. 1,2,3,4,5,6,7,8,9,10,11,12 will return 78 */
        public int EvaluateStringExpression(string expression)
        {
            var result = 0;
            try
            {
                var convertedExpression = expression.Split(',');
                //GD - Removed this section of code to allow as many numbers as desired
                //if (convertedExpression.Length > 2)
                //{
                //    throw new Exception("Unable to evaluate expression due to too many numbers");
                //}
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