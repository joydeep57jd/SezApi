namespace SezApi.Model.Request
{
    public class GatePassRequest
    {
        public GatePass GatePass { get; set; }
        public List<GatePassDtl> GatePassDetails { get; set; }
    }
}
