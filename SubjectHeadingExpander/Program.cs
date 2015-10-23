using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Support.UI;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SubjectHeadingExpander
{
    class Program
    {

        static void Main(string[] args)
        {
            InputValidator inputValidator = new InputValidator(args);
            if (inputValidator.validate())
            {
                String validSubjectPrefix = args[0];
                TaskOrchestrator orchestrator = new TaskOrchestrator(validSubjectPrefix);
                orchestrator.PerformExpansion();
            }

        }

    }
}
