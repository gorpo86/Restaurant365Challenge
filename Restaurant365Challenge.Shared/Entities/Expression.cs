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

        public string MasterDeliminators { get { return $@"(,|\n{AdditionalDelimitors})"; } }

        public string AdditionalDelimitors { get; set; }

        public string NumberExpression { get {
                return ExtractNumberExpression(); } }

        //public string Delimiter { get { return ExtractDelimiter(); } }

        public string[] NumberArray { get {
                
                return Regex.Split(NumberExpression, MasterDeliminators); } }

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
            //for square brace enclosures and grabbing each group
            //GD - We are keeping the square bracers in the group search as they encpsulate multi valued 
            // delimiters in later regex for the Delimiter Split.
            var match2 = Regex.Match(match.Groups[1].Value, @"(\[.*?\])");

            //GD - Assuming we find at least one square brace group we will add that to the 
            //Spilt delimiter options
            if (match2.Success)
            {
                for(int i = 1; i < match2.Groups.Count; i++)
                {
                    AdditionalDelimitors += $"|{match2.Groups[i].Value}";
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
