
using Microsoft.EntityFrameworkCore;
using SezApi.Data;
using SezApi.Model.DBModels;
using SezApi.Model.Request;
using SezApi.Model.Response;
using System.Linq;
namespace SezApi.Services
{
    public class Services  : IServices
    {
        private readonly SezApiDbContext _db;

        public Services(SezApiDbContext db)
        {
            _db = db;
        }

        public async Task AddTest(test product)
        {
            try
            {
                _db.test.Add(product);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
            }
        }

        public async Task<AddEditResponse> AddMststorageCharge(RequestMststorageCharge mststorageCharge)
        {
            var response = new AddEditResponse();
            try
            {
                var result = await _db.AddEditResponse
                    .FromSqlInterpolated($@"
                EXEC SP_AddMstStorageCharge
                    {mststorageCharge.StorageChargeId},
                    {mststorageCharge.BranchId},
                    {mststorageCharge.WarehouseType},
                    {mststorageCharge.ChargeType},
                    {mststorageCharge.RateSqMPerWeek},
                    {mststorageCharge.RateSqMeterPerMonth},
                    {mststorageCharge.RateMeterPerDay},
                    {mststorageCharge.RateCubMeterPerDay},
                    {mststorageCharge.RateCubMeterPerWeek},
                    {mststorageCharge.RateCubMeterPerMonth},
                    {mststorageCharge.EffectiveDate},
                    {mststorageCharge.DaysRangeFrom},
                    {mststorageCharge.DaysRangeTo},
                    {mststorageCharge.SacCode},
                    {mststorageCharge.CommodityType},
                    {mststorageCharge.CreatedBy},
                    {mststorageCharge.CreatedOn},
                    {mststorageCharge.UpdatedBy},
                    {mststorageCharge.UpdatedOn},
                    {mststorageCharge.SurCharge}
            ").ToListAsync();

                response.Response = result.FirstOrDefault()?.Response ?? "No response";
            }
            catch (Exception ex)
            {
                response.Response = "Some error occurred";
            }

            return response;
        }

        public async Task<Response<List<mststoragecharge>>> GetMststorageCharge()
        {
            var response = new Response<List<mststoragecharge>>();
            try
            {
                var result = await _db.mststoragecharge.ToListAsync();
                response.Data = result;
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Data = new List<mststoragecharge>();
                response.Status = false;
            }

            return response;
        }

        public async Task<AddEditResponse> AddEditGetEntry(RequestGetEntry request)
        {
            try
            {
                var result = await _db.AddEditResponse
    .FromSqlInterpolated($@"
        EXEC dbo.Sp_AddEditGetEntry 
            @EntryId = {request.EntryId},
            @CFSCode = {request.CFSCode},
            @GateInNo = {request.GateInNo},
            @EntryDateTime = {request.EntryDateTime},
            @ReferenceNo = {request.ReferenceNo},
            @ReferenceDate = {request.ReferenceDate},
            @ShippingLineId = {request.ShippingLineId},
            @ShippingLine = {request.ShippingLine},
            @CHAName = {request.CHAName},
            @ContainerNo = {request.ContainerNo},
            @Size = {request.Size},
            @Reefer = {(request.Reefer.HasValue ? (request.Reefer.Value ? 1 : 0) : (int?)null)},
            @CustomSealNo = {request.CustomSealNo},
            @ShippingLineSealNo = {request.ShippingLineSealNo},
            @VehicleNo = {request.VehicleNo},
            @ChallanNo = {request.ChallanNo},
            @CargoDescription = {request.CargoDescription},
            @CargoType = {request.CargoType},
            @NoOfPackages = {request.NoOfPackages},
            @GrossWeight = {request.GrossWeight},
            @DepositorName = {request.DepositorName},
            @Remarks = {request.Remarks},
            @TransportMode = {request.TransportMode},
            @ContainerLoadType = {request.ContainerLoadType},
            @TransportFrom = {(request.TransportFrom.HasValue ? request.TransportFrom.ToString() : null)},
            @CreatedBy = {request.CreatedBy},
            @CreatedOn = {request.CreatedOn},
            @UpdatedBy = {request.UpdatedBy},
            @UpdatedOn = {request.UpdatedOn},
            @ContainerNo1 = {request.ContainerNo1},
            @BranchId = {request.BranchId},
            @FormOneDetailId = {request.FormOneDetailId},
            @ContainerType = {request.ContainerType},
            @OperationType = {request.OperationType},
            @DisplayCfs = {request.DisplayCfs},
            @CHAId = {request.CHAId},
            @CBT = {request.CBT},
            @TPNo = {request.TPNo},
            @SystemDateTime = {request.SystemDateTime},
            @TareWeight = {request.TareWeight},
            @MsgFlag = {request.MsgFlag},
            @ActualPackages = {request.ActualPackages},
            @FileName = {request.FileName},
            @FileCode = {request.FileCode}
            ")
            .AsNoTracking()
            .ToListAsync();

                var response = result.FirstOrDefault();

                return response ?? new AddEditResponse { Response = "No response" };
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new ApplicationException("Failed to execute Sp_AddEditGetEntry", ex);
            }
        }

        public async Task<Response<List<GetEntry>>> GetAllEntries()
        {
            var response = new Response<List<GetEntry>>();

            try
            {
                var result = await _db.GetEntryList.ToListAsync();
                response.Data = result;
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Data = new List<GetEntry>();
                response.Status = false;
            }

            return response;
        }
    }
}
