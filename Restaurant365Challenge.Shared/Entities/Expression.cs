using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Restaurant365Challenge.Shared.Entities
{
    public class Expression
    {

        string _expression;
        public Expression(string expression)
        {
            _expression = expression;
            //Avoiding null refrerence
            AdditionalDelimiters = string.Empty;
            //Setting some defaults
            AllowNegatives = false;
            OptionalDelimiter = string.Empty;
            MaxValue = 1000;
            ExtractDelimiter();


        }

        //public string MultiValuedDelimiter { get { return ExtractMultiValuedDelimiter(); } }

        //GD - Updated to use the otional Delimiter rather than using the hard coded value
        //Escaping the option Delimiter makes sure any value can be used
        public string MasterDelimiters { get { return $",{OptionalDelimiterRegex}{AdditionalDelimiters}"; } }

        public string AdditionalDelimiters { get; set; }

        //In Order to properly include the Optional Delim in the regex we need to add a little markup first.
        private string OptionalDelimiterRegex
        {
            get
            {
                return !String.IsNullOrEmpty(OptionalDelimiter) ? $"|(?:{Regex.Escape(OptionalDelimiter)})" : string.Empty;
            }
        }

        public string NumberExpression
        {
            get
            {
                return ExtractNumberExpression();
            }
        }

        public string Errors
        {
            get
            {
                //Moved Errors to the Entity to contain logic regarding the expression
                var errors = string.Empty;
                if (!AllowNegatives)
                {
                    errors = String.Join(',', Numbers.Where(w => w < 0));
                }
                return errors;
            }
        }

        //public string Delimiter { get { return ExtractDelimiter(); } }

        public List<int> Numbers
        {
            get
            {
                var numbers = Regex.Split(NumberExpression, MasterDelimiters);
                return numbers.Select(s => EvaluateValue(s)).ToList();

            }
        }

        public int Result { get { return Numbers.Sum(); } }

        public string Operator { get { return "+"; } }


        public string ExpressionFullyQualifed
        {
            get
            {
                var expression = string.Empty;
                //GD - using the Join funtion to create our forumla then adding the Result
                expression = String.Join(Operator, Numbers);
                expression += " = " + Result;
                return expression;

            }
        }

        public string OptionalDelimiter { get; internal set; }
        public bool AllowNegatives { get; internal set; }
        public int MaxValue { get; internal set; }

        private string ExtractNumberExpression()
        {

            Regex regex = new(@"(//).*(\\n)", RegexOptions.IgnoreCase);
            //GD We are simply going to remove the Delimiter section of the expression if it exists 
            //to leave just the numbers we want behind
            return regex.Replace(_expression, "");


        }
        private void ExtractDelimiter()
        {
            //GD - First we are going to match just on the enclosing stgring for delimiters
            var match = Regex.Match(_expression, @"//(.*?)\\n");

            //GD - Here we are checking inside what we found in the demiliter encosure
            //for square brace enclosures and grabbing each match
            var match2 = Regex.Matches(match.Groups[1].Value, @"\[(.*?)\]");

            //GD - Assuming we find at least one square brace group we will add that to the 
            //Spilt delimiter options
            if (match2.Count > 0)
            {
                for (int i = 0; i < match2.Count; i++)
                {
                    //GD -  A few things going on here.  We are wrapping the value in (?:value) here to
                    //ensure that we are mathing on the full word (delimiter) as well as excaping any 
                    //characters that need it such as *
                    AdditionalDelimiters += $"|(?:{Regex.Escape(match2[i].Groups[1].Value)})";
                }
                return;
            }


            //GD - We are going to find the character between // and \n and add it to the split caharacters
            //GD- If we don't finf and square braces we finish up here
            if (match.Success)
            {
                AdditionalDelimiters += $"|{match.Groups[1].Value}";
                return;
            }

        }

        private int EvaluateValue(string value)
        {
            var converteredValue = 0;
            //GD - Making sure the string has a value otherwise returning 0
            if (String.IsNullOrEmpty(value)) { return 0; }

            //GD - Making sure we are working with a number otherwise return a 0
            if (!int.TryParse(value, out converteredValue)) { return 0; }

            //GD - Requirements desire large numbers greater than 1000 to be treated as 0
            if (converteredValue > MaxValue) { return 0; }

            return converteredValue;
        }


    }
}
