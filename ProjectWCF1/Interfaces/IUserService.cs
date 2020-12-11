using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace ProjectWCF1.Interfaces
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUserService" in both code and config file together.
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST", UriTemplate = "AddUser")]
        UserDto AddUser(SaveUserDto dto);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST", UriTemplate = "UpdateUser")]
        string UpdateUser(SaveUserDto dto);

        [OperationContract]
        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetUser?Id={Id}")]
        UserDto GetUser(int Id);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST", UriTemplate = "DeleteUser")]
        string DeleteUser(SaveUserDto dto);
    }

    [DataContract]
    public class Error
    {

        [DataMember]
        public int Code { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public UserDto Model { get; set; }

        public Error(int code, string message)
        {
            this.Code = code;
            this.Message = message;
        }
        public Error(int code, string message, UserDto model)
        {
            this.Code = code;
            this.Message = message;
            this.Model = model;
        }
    }
}
