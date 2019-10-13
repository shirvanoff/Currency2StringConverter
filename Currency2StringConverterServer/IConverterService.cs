using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Currency2StringConverterServer
{
    [ServiceContract]
    public interface IConverterService
    {
        /// <summary>
        /// Check connection. Returns OK.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string TestConnetion();

        [OperationContract]
        string ConvertString(string value);
    }
}
