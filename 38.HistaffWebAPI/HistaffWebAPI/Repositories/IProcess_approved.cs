using HistaffWebAPI.Models;
namespace HistaffWebAPI.Repositories
{
    public interface IProcess_approved{
        object GetProcess_Approves();  
  
        object GetProcess_ApprovesDetails(string  token);  
        object PRI_PROCESS(Process_approveEncrypt _proces_approve);
    }
}
