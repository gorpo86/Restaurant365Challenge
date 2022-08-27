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
            AdditionalDelimitors = string.Empty;
            ExtractDelimiter();


        }

        //public string MultiValuedDelimiter { get { return ExtractMultiValuedDelimiter(); } }

        public string MasterDelimitors { get { return $"(,|\n{AdditionalDelimitors})"; } }

        public string AdditionalDelimitors { get; set; }

        public string NumberExpression { get {
                return ExtractNumberExpression(); } }

        //public string Delimiter { get { return ExtractDelimiter(); } }

        public string[] NumberArray { get {
                
                return Regex.Split(NumberExpression, MasterDelimitors); } }

        private string ExtractNumberExpression()
        {

            Regex regex = new( @"(//).*(\\n)", RegexOptions.IgnoreCase);
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
                for(int i = 0; i < match2.Count; i++)
                {
                    //GD -  A few things going on here.  We are wrapping the value in (?:value) here to
                    //ensure that we are mathing on the full word (delimiter) as well as excaping any 
                    //characters that need it such as *
                    AdditionalDelimitors += $"|(?:{Regex.Escape(match2[i].Groups[1].Value)})";
                }
                return;
            }
            

            //GD - We are going to find the character between // and \n and add it to the split caharacters
            //GD- If we don't finf and square braces we finish up here
            if (match.Success) {
                AdditionalDelimitors += $"|{match.Groups[1].Value}";
                return;
            }

        }

       
    }
}
