using SezApi.Model.DBModels;
namespace SezApi.Model.Request
{
    public class RequestDestuffingEntry
    {
         public ImpDestuffingEntryHdr DestuffingEntryHdr { get; set; }
         public List<ImpDestuffingEntryDtl> DestuffingEntryDtl { get; set; }
    }
}
