using SezApi.Model.DBModels;

namespace SezApi.Model.Request
{
	public class RequestLoadContainerRequest
	{
		public LoadContainerRequestHeader LoadContainerHeader { get; set; }
		public List<LoadContainerRequestDetails> LoadContainerRequestDetails { get; set; }
	}
}
