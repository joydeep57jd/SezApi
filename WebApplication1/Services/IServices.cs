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
        Task<AddEditResponse> AddEditHTCharges(HTChargesRequest request);
        Task<Response<List<HTCharges>>> GetAllHTEntries();
        Task<AddEditResponse> AddEditFSCTHCCharges(RequestFscThcChargeRequest request);
        Task<Response<List<FSCTHCcharges>>> GetAllFSCTHCCharges();
    }
}
