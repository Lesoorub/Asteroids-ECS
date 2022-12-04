using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLogger
{
    public class StringFormatter
    {
        public List<Variable> staticVariables = new List<Variable>();

        public StringFormatter(params Variable[] staticVariables)
        {
            this.staticVariables = staticVariables.ToList();
        }

        public string Format(string pattern, params Variable[] variables)
        {
            string text = pattern;
            text = Apply(text, staticVariables.ToArray());
            text = Apply(text, variables);
            return text;
        }

        private string Apply(string text, Variable[] variables)
        {
            foreach (var variable in variables)
                text = text.Replace($"{{{variable.name}}}", variable.value.ToString());
            return text;
        }

        public struct Variable
        {
            public string name;
            public object value;

            public Variable(string name, object value)
            {
                this.name = name;
                this.value = value;
            }
            public Variable((string name, object value) turple)
            {
                (name, value) = turple;
            }

            public static implicit operator Variable((string name, object value) turple) => new Variable(turple);
        }
    }
}