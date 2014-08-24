using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace example.wcf
{
    [ServiceContract(SessionMode = SessionMode.Allowed)]
    public interface ITest
    {
        [OperationContract]
        string say(string word);
    }
}
