using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ganss.XSS;

namespace ExperimentApplication.Classes
{
    public static class SecurityTestClass
    {

        public static void TestSanitization(string stringUnderTest)
        {
            var strictHtmlSanitizer = new HtmlSanitizer(new List<string>(), new List<string>(), new List<string>(), new List<string>(), new List<string>());
            var result = strictHtmlSanitizer.Sanitize(stringUnderTest);
            var a = 0;
        }
    }
}
