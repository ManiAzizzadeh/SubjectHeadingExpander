using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectHeadingExpander
{
    /// <summary>
    /// Validates user-submitted input arguments from the command line.
    /// </summary>
    public class InputValidator
    {
        private string[] args;

        public InputValidator(string[] args)
        {
            this.args = args;
        }

        public bool validate()
        {
            if (args.Length != 1)
            {
                PrintHelpText();
                return false;
            }

            if (args[0].Length < 2)
            {
                PrintHelpText();
                return false;
            }

            return true;
        }

        private void PrintHelpText() {
            Console.WriteLine("This program is used to expand a subject prefix into actual subject headings for which details are then collected in Libris \n");
            Console.WriteLine("       The program accepts only one argument - the subject prefix to expand.");
            Console.WriteLine("       The subject prefix must be at least 2 characters long.");
        }
    }
}
