using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace example.wcf
{
    [System.ServiceModel.Activation.AspNetCompatibilityRequirements(RequirementsMode = System.ServiceModel.Activation.AspNetCompatibilityRequirementsMode.Allowed)]
    public class Test2:ITest
    {
        public string say(string word)
        {
            Console.WriteLine("client2 say:" + word);
            return "thank you2!";
        }
    }
}
