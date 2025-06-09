using Microsoft.AspNetCore.Mvc;
using SezApi.Model.DBModels;
using SezApi.Model.Request;
using SezApi.Model.Response;

namespace SezApi.Services
{
    public interface IServices 
    {
        Task AddTest(test product);
        Task<AddEditResponse> AddMststorageCharge(RequestMststorageCharge mststorageCharge);
        Task<Response<List<mststoragecharge>>> GetMststorageCharge();
        Task<AddEditResponse> AddEditGetEntry(RequestGetEntry request);
        Task<Response<List<GetEntry>>> GetAllEntries();
        Task<AddEditResponse> AddEditMstOperation(RequestMstOperation request);
        Task<Response<List<MstOperation>>> GetMstOperation();
        Task<AddEditResponse> AddEditMstSac(RequestMstSac mststorageCharge);
        Task<Response<List<MstSac>>> GetMstSac();
        Task<AddEditResponse> AddEditMstEntryFee(RequestMstEntryFee request);
        Task<Response<List<MstEntryFee>>> GetMstEntryFee();
        Task<AddEditResponse> AddEditHTCharges(HTChargesRequest request);
        Task<Response<List<HTCharges>>> GetAllHTEntries();
        Task<AddEditResponse> AddEditFSCTHCCharges(RequestFscThcChargeRequest request);
        Task<Response<List<FSCTHCcharges>>> GetAllFSCTHCCharges();
        Task<AddEditResponse> AddEditMstGroundRent(RequestMstGroundRent request);
        Task<Response<List<MstGroundRent>>> GetMstGroundRent();
        Task<AddEditResponse> AddEditMstInsurance(RequestMstInsurance request);
        Task<Response<List<MstInsurance>>> GetMstInsurance();
        Task<AddEditResponse> AddEditMstMiscellaneouse(RequestMstMiscellaneous request);
        Task<Response<List<MstMiscellaneous>>> GetMstMiscellaneous();

        Task<AddEditResponse> AddEditMstRailFreightFees(RequestMstRailFreightFees request);
        Task<Response<List<MstRailFreightFees>>> GetMstRailFreightFees();
    }
}
