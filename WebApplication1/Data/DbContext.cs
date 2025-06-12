
using Azure;
using Microsoft.EntityFrameworkCore;
using SezApi.Model;
using SezApi.Model.DBModels;
using SezApi.Model.Response;

namespace SezApi.Data
{
    public class SezApiDbContext : DbContext
    {
        public SezApiDbContext(DbContextOptions<SezApiDbContext> options)
            : base(options)
        {
        }

        public DbSet<test> test { get; set; }
        public DbSet<mststoragecharge> mststoragecharge { get; set; }
        public DbSet<AddEditResponse> AddEditResponse { get; set; }
        public DbSet<GateEntry> GetEntryList { get; set; }
        public DbSet<HTCharges> HTChargesList { get; set; }
       public DbSet<FSCTHCcharges> FSCTHCchargesList { get; set; }
        public DbSet<MstOperation> GetMstOperation { get; set; }
        public DbSet<MstSac> GetMstSac { get; set; }
        public DbSet<MstEntryFee> GetMstEntryFee { get; set; }
        public DbSet<ReeferCharges> GetReeferChargesList { get; set; }
        public DbSet<MovementCharge> GetMovementChargesList { get; set; }
        public DbSet<FumigationCharge> GetFumigationChargesList { get; set; }
        public DbSet<RTRChargeDetails> GetRTRChargesDetailsList { get; set; }
        public DbSet<MstGroundRent> GetMstGroundRent { get; set; }

        public DbSet<MstInsurance> GetMstInsurance { get; set; }

        public DbSet<MstMiscellaneous> GetMstMiscellaneous { get; set; }
        public DbSet<MstRailFreightFees> GetMstRailFreightFees { get; set; }
        public DbSet<MstParty> GetMstParty { get; set; }
        public DbSet<Port> GetPort { get; set; }
        public DbSet<ResponsePort> ResponsePort { get; set; }
        

        public DbSet<MstEximTraderMaster> GetMstEximTraderMaster { get; set; }
        public DbSet<MstCommodity> GetMstCommodity { get; set; }
        public DbSet<State> GetState { get; set; }
        public DbSet<ResponseOBLEntry> ResponseOBLEntry { get; set; }
        public DbSet<GoDown> GetMstGoDown { get; set; }
        public DbSet<Country> GetCountryList { get; set; }
        public DbSet<InvoiceYard> GetYardInvoiceList { get; set; }


    }
}
