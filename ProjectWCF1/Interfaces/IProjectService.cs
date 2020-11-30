using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ProjectWCF1.Interfaces
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IProjectService" in both code and config file together.
    [ServiceContract]
    public interface IProjectService
    {
        [OperationContract]
        bool AddProject(ProjectDto dto);

        [OperationContract]
        ProjectDto GetProject(int Id);

        [OperationContract]
        bool AddRole(ProjectRoleDto dto);

        [OperationContract]
        List<ProjectRoleDto> GetRole(int Id);
    }
}
