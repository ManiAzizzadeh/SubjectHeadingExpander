using NUnit.Framework;
using SubjectHeadingExpander;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectHeadingExpanderTests
{
    public class InputValidatorTest
    {
        [Test]
        public void ValidationFailsWhenNoArgumentsProvided()
        {
            string[] args = new string[0];
            InputValidator inputValidator = new InputValidator(args);
            Assert.IsFalse(inputValidator.validate());

        }

        [Test]
        public void ValidationFailsSubjectPrefixLengthToSmall()
        {
            string[] args = new string[] { "A" };
            InputValidator inputValidator = new InputValidator(args);
            Assert.IsFalse(inputValidator.validate());

        }

        [Test]
        public void ValidationSucceedsWhenSubjectPrefixIsAtLeastTwoCharacters()
        {
            string[] args = new string[] { "Öl" };
            InputValidator inputValidator = new InputValidator(args);
            Assert.IsTrue(inputValidator.validate());

        }
    }
}
