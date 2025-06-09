
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
        public DbSet<GetEntry> GetEntryList { get; set; }
        public DbSet<HTCharges> HTChargesList { get; set; }
       public DbSet<FSCTHCcharges> FSCTHCchargesList { get; set; }


        public DbSet<MstOperation> GetMstOperation { get; set; }

        public DbSet<MstSac> GetMstSac { get; set; }

        public DbSet<MstEntryFee> GetMstEntryFee { get; set; }

        public DbSet<MstGroundRent> GetMstGroundRent { get; set; }

        public DbSet<MstInsurance> GetMstInsurance { get; set; }

        public DbSet<MstMiscellaneous> GetMstMiscellaneous { get; set; }
        public DbSet<MstRailFreightFees> GetMstRailFreightFees { get; set; }

    }
}
