﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.17929
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace JM.Common.Examples.web.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.test1Soap")]
    public interface test1Soap {
        
        // CODEGEN: 命名空间 http://tempuri.org/ 的元素名称 json 以后生成的消息协定未标记为 nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Get", ReplyAction="*")]
        JM.Common.Examples.web.ServiceReference1.GetResponse Get(JM.Common.Examples.web.ServiceReference1.GetRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="Get", Namespace="http://tempuri.org/", Order=0)]
        public JM.Common.Examples.web.ServiceReference1.GetRequestBody Body;
        
        public GetRequest() {
        }
        
        public GetRequest(JM.Common.Examples.web.ServiceReference1.GetRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string json;
        
        public GetRequestBody() {
        }
        
        public GetRequestBody(string json) {
            this.json = json;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetResponse", Namespace="http://tempuri.org/", Order=0)]
        public JM.Common.Examples.web.ServiceReference1.GetResponseBody Body;
        
        public GetResponse() {
        }
        
        public GetResponse(JM.Common.Examples.web.ServiceReference1.GetResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetResult;
        
        public GetResponseBody() {
        }
        
        public GetResponseBody(string GetResult) {
            this.GetResult = GetResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface test1SoapChannel : JM.Common.Examples.web.ServiceReference1.test1Soap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class test1SoapClient : System.ServiceModel.ClientBase<JM.Common.Examples.web.ServiceReference1.test1Soap>, JM.Common.Examples.web.ServiceReference1.test1Soap {
        
        public test1SoapClient() {
        }
        
        public test1SoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public test1SoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public test1SoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public test1SoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        JM.Common.Examples.web.ServiceReference1.GetResponse JM.Common.Examples.web.ServiceReference1.test1Soap.Get(JM.Common.Examples.web.ServiceReference1.GetRequest request) {
            return base.Channel.Get(request);
        }
        
        public string Get(string json) {
            JM.Common.Examples.web.ServiceReference1.GetRequest inValue = new JM.Common.Examples.web.ServiceReference1.GetRequest();
            inValue.Body = new JM.Common.Examples.web.ServiceReference1.GetRequestBody();
            inValue.Body.json = json;
            JM.Common.Examples.web.ServiceReference1.GetResponse retVal = ((JM.Common.Examples.web.ServiceReference1.test1Soap)(this)).Get(inValue);
            return retVal.Body.GetResult;
        }
    }
}