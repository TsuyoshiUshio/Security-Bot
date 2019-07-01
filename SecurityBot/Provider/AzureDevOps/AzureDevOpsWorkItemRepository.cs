using System;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using System.Threading.Tasks;


namespace SecurityBot.Provider.AzureDevOps
{
    public interface IAzureDevOpsWorkItemRepository
    {
        Task<WorkItem> CreateWorkItem(WorkItemSource workItem);
    }

    public class AzureDevOpsWorkItemRepository : IAzureDevOpsWorkItemRepository
    {
        private WorkItemTrackingHttpClientBase client;

        public AzureDevOpsWorkItemRepository(WorkItemTrackingHttpClientBase client)
        {
            this.client = client;
        }


        public async Task<WorkItem> CreateWorkItem(WorkItemSource workItem)
        {
            try
            {
                var project = workItem.Project;
                var type = workItem.Type;
                var document = workItem.ToJsonPatchDocument();

                return await client.CreateWorkItemAsync(document, project, type);
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
