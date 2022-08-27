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
       
        public int EvaluateStringExpression(string expression, string optionalDelim, string allowNegatives, string maxValue)
        {


            try
            {
                //GD - moved expression part into a class
                //var convertedExpression = ConvertExpression(expression);
                //GD - Removed this section of code to allow as many numbers as desired
                //if (convertedExpression.Length > 2)
                //{
                //    throw new Exception("Unable to evaluate expression due to too many numbers");
                //}
                //GD - moved everything into the Expression class and allowing this class
                //to handle just exception handling and logging

                var convertedExpression = new Expression(expression);
                ProcessArguments(optionalDelim, allowNegatives, maxValue, convertedExpression);


                if (convertedExpression.Errors != String.Empty)
                {
                    throw new Exception($"We found negative numbers which is not allowed, the values were: {convertedExpression.Errors}");
                }

                _log.Log(convertedExpression.ExpressionFullyQualifed);

                return convertedExpression.Result;

            }
            catch (Exception ex)
            {

                throw;
                //_log.Log("An error arose while evaulating expression:", ex);
                //return 0;
            }


        }

        private void ProcessArguments(string optionalDelim, string allowNegatives, string maxValue, Expression convertedExpression)
        {
            //OptionalDelimiter - Ignore if empty
            if (!String.IsNullOrEmpty(optionalDelim))
            {
                convertedExpression.OptionalDelimiter = optionalDelim;
            }

            //Validating the argunment and ensuring the value can be parsed into a bool
            if (!String.IsNullOrEmpty(allowNegatives))
            {
                if (bool.TryParse(allowNegatives, out bool convertedBool))
                    convertedExpression.AllowNegatives = convertedBool;
                else
                    throw new ArgumentException("The value passed in to Allow Negatives could not be parsed into a Boolean value");

            }

            //Validating the argunment and ensuring the value can be parsed into a int
            if (!String.IsNullOrEmpty(maxValue))
            {
                if (int.TryParse(maxValue, out int convertedInt))
                    convertedExpression.MaxValue = convertedInt;
                else
                    throw new ArgumentException("The value passed in for Max Value could not be parsed into a integer value");

            }

        }

        //private static int EvaluateValue(string value)
        //{
        //    var converteredValue = 0;
        //    //GD - Making sure the string has a value otherwise returning 0
        //    if (String.IsNullOrEmpty(value)) { return 0; }

        //    //GD - Making sure we are working with a number otherwise return a 0
        //    if (!int.TryParse(value, out converteredValue)) { return 0; }

        //    //GD - Requirements desire large numbers greater than 1000 to be treated as 0
        //    if (converteredValue > 1000) { return 0; }

        //    return converteredValue;
        //}

        //GD - this logic was moved to the Expression Class
        //private string[] ConvertExpression(string expression)
        //{
        //    //GD - We are adding the new line char here as a new delimiter option but breaking it into it's own method
        //    //as this will likely expand in function down the road

        //    return expression.Split(',', '\n');

        //}


    }
}