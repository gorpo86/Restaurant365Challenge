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
            ;
            
        }

        public char[] Delimiters { get { return new char[] { ',', '\n',Delimiter }; } }

        public string NumberExpression { get {
                return ExtractNumberExpression(); } }

        public char Delimiter { get { return ExtractDelimiter(); } }

        public string[] NumberArray { get { return NumberExpression.Split(Delimiters); } }

        private string ExtractNumberExpression()
        {

            Regex regex = new( @"(//).*(\\n)", RegexOptions.IgnoreCase);
            //GD We are simply going to remove the Delimiter section of the expression if it exists 
            //to leave just the numbers we want behind
            return regex.Replace(_expression, "");


        }
        private char ExtractDelimiter()
        {
            Regex regex = new Regex(@"//(.*?)\\n", RegexOptions.IgnoreCase);
            //string pattern = Regex.Escape(_expression);
            var match = regex.Match(_expression);

            //GD - We are going to find the character between // and \n and add it to the split caharacters
            if (match.Success) {
                return match.Groups[1].Value.ToCharArray()[0]; }

            return new char();


        }
    }
}
