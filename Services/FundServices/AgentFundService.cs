using ofarz_rest_api.Domain.IRepositories;
using ofarz_rest_api.Domain.IRepositories.IFundRepository;
using ofarz_rest_api.Domain.IService.Communication.FundCommunication;
using ofarz_rest_api.Domain.IService.IFundServices;
using ofarz_rest_api.Domain.Models.Fund;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ofarz_rest_api.Services.FundServices
{
    public class AgentFundService : IAgentFundService
    {
        private readonly IAgentFundRepository _agentFundRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AgentFundService(IAgentFundRepository agentFundRepository, IUnitOfWork unitOfWork)
        {
            _agentFundRepository = agentFundRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<SaveAgentFundResponse> DeleteAsync(int id)
        {
            var existingAgentFund = await _agentFundRepository.FindByIdAsync(id);

            if (existingAgentFund == null)
                return new SaveAgentFundResponse("Category not found.");

            try
            {
                _agentFundRepository.Remove(existingAgentFund);
                await _unitOfWork.CompleteAsync();

                return new SaveAgentFundResponse(existingAgentFund);
            }

            catch (Exception ex)
            {
                return new SaveAgentFundResponse($"An error occurred when deleting the category: {ex.Message}");
            }
        }

        public async Task<AgentFund> FindByIdAsync(int id)
        {
            return await _agentFundRepository.FindByIdAsync(id);
        }

        public async Task<IEnumerable<AgentFund>> ListAsync()
        {
            return await _agentFundRepository.ListAsync();
        }

        public async Task<SaveAgentFundResponse> SaveAsync(AgentFund agentFund)
        {
            try
            {
                await _agentFundRepository.AddAsync(agentFund);
                await _unitOfWork.CompleteAsync();

                return new SaveAgentFundResponse(agentFund);
            }
            catch (Exception ex)
            {
                return new SaveAgentFundResponse($"An error occurred when saving the category: {ex.Message}");
            }
        }

        public async Task<SaveAgentFundResponse> UpdateAsync(int id, AgentFund agentFund)
        {
            var existingAgentFund = await _agentFundRepository.FindByIdAsync(id);

            if (existingAgentFund == null)
                return new SaveAgentFundResponse("Coumntry not found");

            //existingAgentFund.Name = country.Name;

            try
            {
                _agentFundRepository.Update(existingAgentFund);
                await _unitOfWork.CompleteAsync();

                return new SaveAgentFundResponse(existingAgentFund);
            }
            catch (Exception ex)
            {
                return new SaveAgentFundResponse($"An error occurred when updating the category: {ex.Message}");
            }
        }
    }
}
