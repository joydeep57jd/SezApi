using SezApi.Model.DBModels;

namespace SezApi.Model.Response
{
	public class RequestContainerStuffing
	{
		public ContainerStuffingHeader ContainerStuffingHeader { get; set; }
		public List<ContainerStuffingDetails> ContainerStuffingDetails { get; set; }
	}
}
