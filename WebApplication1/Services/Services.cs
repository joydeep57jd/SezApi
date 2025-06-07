
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using SezApi.Data;
using SezApi.Model.DBModels;
using SezApi.Model.Request;
using SezApi.Model.Response;
using System.Linq;
namespace SezApi.Services
{
    public class Services : IServices
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

        public async Task<AddEditResponse> AddMststorageCharge(RequestMststorageCharge request)
        {
            var response = new AddEditResponse();
            try
            {
                var result = await _db.AddEditResponse
                    .FromSqlInterpolated($@"
                EXEC SP_AddMstStorageCharge
                    {request.StorageChargeId},
                    {request.BranchId},
                    {request.WarehouseType},
                    {request.ChargeType},
                    {request.RateSqMPerWeek},
                    {request.RateSqMeterPerMonth},
                    {request.RateMeterPerDay},
                    {request.RateCubMeterPerDay},
                    {request.RateCubMeterPerWeek},
                    {request.RateCubMeterPerMonth},
                    {request.EffectiveDate},
                    {request.DaysRangeFrom},
                    {request.DaysRangeTo},
                    {request.SacCode},
                    {request.CommodityType},
                    {request.CreatedBy},
                    {request.UpdatedBy},
                    {request.SurCharge}
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
            @UpdatedBy = {request.UpdatedBy},
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

        public async Task<AddEditResponse> AddEditMstOperation(RequestMstOperation request)
        {
            try
            {
                var result = await _db.AddEditResponse
                       .FromSqlInterpolated($@"
                          EXEC SP_AddMstOperation 
                          @OperationId = {request.OperationId},
                          @BranchId = {request.BranchId},
                          @OperationType = {request.OperationType},
                          @OperationCode = {request.OperationCode},
                          @SacId = {request.SacId},
                          @OperationSDesc = {request.OperationSDesc},
                          @OperationDesc = {request.OperationDesc},
                          @ClauseOrder = {request.ClauseOrder},
                          @PkgCount = {request.PkgCount},
                          @CreatedBy = {request.CreatedBy},
                          @UpdatedBy = {request.UpdatedBy}
                      ")
                  .AsNoTracking()
                  .ToListAsync();


                var response = result.FirstOrDefault();

                return response ?? new AddEditResponse { Response = "No response" };
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to execute Sp_AddEditMstOperation", ex);
            }
        }

        public async Task<Response<List<MstOperation>>> GetMstOperation()
        {
            var response = new Response<List<MstOperation>>();

            try
            {
                var result = await _db.GetMstOperation.ToListAsync();
                response.Data = result;
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Data = new List<MstOperation>();
                response.Status = false;
            }

            return response;
        }

        public async Task<AddEditResponse> AddEditMstSac(RequestMstSac request) 
        {
            try
            {
                var result = await _db.AddEditResponse
                      .FromSqlInterpolated($@"
                      EXEC SP_AddMstSac 
                     @SacId = {request.SacId},
                     @BranchId = {request.BranchId},
                     @SacCode = {request.SacCode},
                     @Description = {request.Description},
                     @Gst = {request.Gst},
                     @Cess = {request.Cess},
                     @CreatedBy = {request.CreatedBy},
                     @UpdatedBy = {request.UpdatedBy}
                     ")
                    .AsNoTracking()
                    .ToListAsync();

                var response = result.FirstOrDefault();
                return response ?? new AddEditResponse { Response = "No response" };

            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to execute Sp_AddEditMstOperation", ex);
            }
        }

        public async Task<Response<List<MstSac>>> GetMstSac()
        {
            var response = new Response<List<MstSac>>();

            try
            {
                var result = await _db.GetMstSac.ToListAsync();
                response.Data = result;
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Data = new List<MstSac>();
                response.Status = false;
            }

            return response;
        }

        public async Task<AddEditResponse> AddEditMstEntryFee(RequestMstEntryFee request)
        {
            try
            {
                var result = await _db.AddEditResponse
                    .FromSqlInterpolated($@"
            EXEC SP_AddEditMstEntryFee 
                @EntryFeeId = {request.EntryFeeId},
                @ContainerType = {request.ContainerType},
                @CommodityType = {request.CommodityType},
                @OperationType = {request.OperationType},
                @Reefer = {request.Reefer},
                @Rate = {request.Rate},
                @EffectiveDate = {request.EffectiveDate},
                @ContainerSize = {request.ContainerSize},
                @SacCode = {request.SacCode},
                @BranchId = {request.BranchId},
                @CreatedBy = {request.CreatedBy},
                @UpdatedBy = {request.UpdatedBy},
                @WeightSlab = {request.WeightSlab}
        ")
                    .AsNoTracking()
                    .ToListAsync();

                var response = result.FirstOrDefault();
                return response ?? new AddEditResponse { Response = "No response" };
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to execute SP_AddMstEntryFee", ex);
            }

        }
        
        public async Task<Response<List<MstEntryFee>>> GetMstEntryFee()
        {
            var response = new Response<List<MstEntryFee>>();

            try
            {
                var result = await _db.GetMstEntryFee.ToListAsync();
                response.Data = result;
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Data = new List<MstEntryFee>();
                response.Status = false;
            }

            return response;
        }

        public async Task<AddEditResponse> AddEditHTCharges(HTChargesRequest request)
        {
            try
            {
                var result = await _db.AddEditResponse
                    .FromSqlInterpolated($@"
                    EXEC dbo.Sp_AddEditHTCharges 
                        @HTChargesID = {request.HTChargesID},
                        @OperationId = {request.OperationId},
                        @ContainerType = {request.ContainerType},
                        @Type = {request.Type},
                        @Size = {request.Size},
                        @MaxDistance = {request.MaxDistance},
                        @CommodityType = {request.CommodityType},
                        @ContainerLoadType = {request.ContainerLoadType},
                        @TransportFrom = {request.TransportFrom},
                        @EximType = {request.EximType},
                        @RateCWC = {request.RateCWC},
                        @ContractorRate = {request.ContractorRate},
                        @EffectiveDate = {request.EffectiveDate},
                        @BranchId = {request.BranchId},
                        @CreatedBy = {request.CreatedBy},
                        @UpdatedBy = {request.UpdatedBy}
                ")
                    .AsNoTracking()
                    .ToListAsync();

                var response = result.FirstOrDefault();
                return response ?? new AddEditResponse { Response = "No response from procedure." };
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to execute Sp_AddEntryHTCharges", ex);
            }
        }

        public async Task<Response<List<HTCharges>>> GetAllHTEntries()
        {
            var response = new Response<List<HTCharges>>();

            try
            {
                var result = await _db.HTChargesList.ToListAsync();
                response.Data = result;
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Data = new List<HTCharges>();
                response.Status = false;
            }

            return response;
        }

        public async Task<AddEditResponse> AddEditFSCTHCCharges(RequestFscThcChargeRequest request)
        {
            try
            {
                var result = await _db.AddEditResponse
                    .FromSqlInterpolated($@"
                    EXEC dbo.Sp_AddEditFscthcCharges 
                        @FSCChargesID = {request.FSCChargesID},
                        @OperationId = {request.OperationId},
                        @ContainerType = {request.ContainerType},
                        @Type = {request.Type},
                            @Size = {request.Size},
                            @MaxDistance = {request.MaxDistance},
                            @CommodityType = {request.CommodityType},
                            @ContainerLoadType = {request.ContainerLoadType},
                            @TransportFrom = {request.TransportFrom},
                            @EximType = {request.EximType},
                            @LocationId = {request.LocationId},
                            @FromMetric = {request.FromMetric},
                            @ToMetric = {request.ToMetric},
                            @RateCWC = {request.RateCWC},
                            @ContractorRate = {request.ContractorRate},
                            @EffectiveDate = {request.EffectiveDate},
                            @BranchId = {request.BranchId},
                            @CreatedBy = {request.CreatedBy},
                            @UpdatedBy = {request.UpdatedBy}
                             ")
                    .AsNoTracking()
                    .ToListAsync();

                var response = result.FirstOrDefault();
                return response ?? new AddEditResponse { Response = "No response from procedure." };
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to execute Sp_AddEntryHTCharges", ex);
            }
        }

        public async Task<Response<List<FSCTHCcharges>>> GetAllFSCTHCCharges()
        {
            var response = new Response<List<FSCTHCcharges>>();

            try
            {
                var result = await _db.FSCTHCchargesList.ToListAsync();
                response.Data = result;
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Data = new List<FSCTHCcharges>();
                response.Status = false;
            }

            return response;
        }
    }
}
