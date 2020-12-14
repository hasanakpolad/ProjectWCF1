using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace ProjectWCF1.Interfaces
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IProjectService" in both code and config file together.
    [ServiceContract]
    public interface IProjectService
    {
        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST", UriTemplate = "AddProject")]
        string AddProject(ProjectDto dto);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST", UriTemplate = "UpdateProject")]
        string UpdateProject(ProjectDto dto);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "GET", UriTemplate = "GetProject?Id={Id}")]
        ProjectDto GetProject(int Id);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST", UriTemplate = "DeleteProject")]
        string DeleteProject(ProjectDto dto);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST", UriTemplate = "AddRole")]
        string AddRole(ProjectRoleDto dto);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST", UriTemplate = "UpdateRole")]
        string UpdateRole(ProjectRoleDto dto);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "GET", UriTemplate = "GetRole?Id={Id}")]
        IEnumerable<ProjectRoleDto> GetRole(int Id);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST", UriTemplate = "DeleteRole")]
        string DeleteRole(ProjectRoleDto dto);
    }
}
