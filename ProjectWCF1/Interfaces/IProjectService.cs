using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ProjectWCF1.Interfaces
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IProjectService" in both code and config file together.
    [ServiceContract]
    public interface IProjectService
    {
        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, Method = "POST")]
        bool AddProject(ProjectDto dto);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, Method = "POST")]
        bool UpdateProject(ProjectDto dto);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, Method = "GET")]
        ProjectDto GetProject(int Id);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, Method = "POST")]
        bool DeleteProject(int id);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, Method = "POST")]
        bool AddRole(ProjectRoleDto dto);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, Method = "POST")]
        bool UpdateRole(ProjectRoleDto dto);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, Method = "GET")]
        List<ProjectRoleDto> GetRole(int Id);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, Method = "POST")]
        bool DeleteRole(int id);
    }
}
