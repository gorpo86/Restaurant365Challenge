using Restaurant365Challenge.Shared.Entities;
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
                 /*7.	Support 1 custom delimiter of any length using the format: //[{delimiter}]\n{numbers}
	                    example: //[***]\n11***22***33 will return 66
                   all previous formats should also be supported
                 */
        public int EvaluateStringExpression(string expression)
        {
            var result = 0;
            var errors = string.Empty;
            try
            {
                //GD - moved expression part into a class
                //var convertedExpression = ConvertExpression(expression);
                //GD - Removed this section of code to allow as many numbers as desired
                //if (convertedExpression.Length > 2)
                //{
                //    throw new Exception("Unable to evaluate expression due to too many numbers");
                //}

                var convertedExpression = new Expression(expression);
               
                foreach (var value in convertedExpression.NumberArray)
                {
                    //GD - Extracted into a method to handle future evaluation needs
                    int converteredValue = EvaluateValue(value);

                    //GD - Checking for negative number and buillding up an error to report back to the user
                    if (converteredValue < 0) { errors += $"{value},"; }

                    result += converteredValue;

                }

                if (!string.IsNullOrEmpty(errors))
                {
                    throw new Exception($"We found negative numbers which is not allowed, the values were: {errors}");
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

        private static int EvaluateValue(string value)
        {
            var converteredValue = 0;
            //GD - Making sure the string has a value otherwise returning 0
            if (String.IsNullOrEmpty(value)) { return 0; }

            //GD - Making sure we are working with a number otherwise return a 0
            if (!int.TryParse(value, out converteredValue)) { return 0; }

            //GD - Requirements desire large numbers greater than 1000 to be treated as 0
            if (converteredValue > 1000) { return 0; }

            return converteredValue;
        }

        //GD - this logic was moved to the Expression Class
        //private string[] ConvertExpression(string expression)
        //{
        //    //GD - We are adding the new line char here as a new delimiter option but breaking it into it's own method
        //    //as this will likely expand in function down the road

        //    return expression.Split(',', '\n');

        //}


    }
}