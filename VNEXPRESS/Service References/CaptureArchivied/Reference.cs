﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VNEXPRESS.CaptureArchivied {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ConvertPDFRequest", Namespace="http://schemas.datacontract.org/2004/07/PDFConverterConSoleService.Entities")]
    [System.SerializableAttribute()]
    public partial class ConvertPDFRequest : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string contentIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string deleteItemsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int filterByField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string filterTextField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string pageURLField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string contentId {
            get {
                return this.contentIdField;
            }
            set {
                if ((object.ReferenceEquals(this.contentIdField, value) != true)) {
                    this.contentIdField = value;
                    this.RaisePropertyChanged("contentId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string deleteItems {
            get {
                return this.deleteItemsField;
            }
            set {
                if ((object.ReferenceEquals(this.deleteItemsField, value) != true)) {
                    this.deleteItemsField = value;
                    this.RaisePropertyChanged("deleteItems");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int filterBy {
            get {
                return this.filterByField;
            }
            set {
                if ((this.filterByField.Equals(value) != true)) {
                    this.filterByField = value;
                    this.RaisePropertyChanged("filterBy");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string filterText {
            get {
                return this.filterTextField;
            }
            set {
                if ((object.ReferenceEquals(this.filterTextField, value) != true)) {
                    this.filterTextField = value;
                    this.RaisePropertyChanged("filterText");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string pageURL {
            get {
                return this.pageURLField;
            }
            set {
                if ((object.ReferenceEquals(this.pageURLField, value) != true)) {
                    this.pageURLField = value;
                    this.RaisePropertyChanged("pageURL");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="CaptureArchivied.IPDFConverterService")]
    public interface IPDFConverterService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPDFConverterService/ConvertToPDF", ReplyAction="http://tempuri.org/IPDFConverterService/ConvertToPDFResponse")]
        string ConvertToPDF(VNEXPRESS.CaptureArchivied.ConvertPDFRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPDFConverterService/ConvertToPDF", ReplyAction="http://tempuri.org/IPDFConverterService/ConvertToPDFResponse")]
        System.Threading.Tasks.Task<string> ConvertToPDFAsync(VNEXPRESS.CaptureArchivied.ConvertPDFRequest request);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IPDFConverterServiceChannel : VNEXPRESS.CaptureArchivied.IPDFConverterService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class PDFConverterServiceClient : System.ServiceModel.ClientBase<VNEXPRESS.CaptureArchivied.IPDFConverterService>, VNEXPRESS.CaptureArchivied.IPDFConverterService {
        
        public PDFConverterServiceClient() {
        }
        
        public PDFConverterServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public PDFConverterServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PDFConverterServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PDFConverterServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string ConvertToPDF(VNEXPRESS.CaptureArchivied.ConvertPDFRequest request) {
            return base.Channel.ConvertToPDF(request);
        }
        
        public System.Threading.Tasks.Task<string> ConvertToPDFAsync(VNEXPRESS.CaptureArchivied.ConvertPDFRequest request) {
            return base.Channel.ConvertToPDFAsync(request);
        }
    }
}
