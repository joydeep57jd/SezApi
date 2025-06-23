using SezApi.Model.DBModels;
namespace SezApi.Model.Request
{
    public class RequestExitThroughGate
    {
        public ExitThroughGateHeader ExitThroughGateHeader { get; set; }
        public List<ExitThroughGateDetails> ExitThroughGateDetails { get; set; }
    }
}
