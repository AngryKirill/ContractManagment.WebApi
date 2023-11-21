﻿using AutoMapper;
using ContractManagment.API.Controllers.Generic;
using ContractManagment.API.ViewModel;
using ContractManagment.BLL.Interfaces;
using ContractManagment.BLL.Interfaces.Generic;
using ContractManagment.BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContractManagment.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    [Authorize]
    public class ContractController : Controller
    {
        private readonly IContractService _service;
        protected readonly IMapper _mapper;

        public ContractController(IContractService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ContractViewModel>> GetAllAsync(CancellationToken cancellationToken)
        {
            var tModels = await _service.GetAllAsync(cancellationToken);

            return _mapper.Map<IEnumerable<ContractViewModel>>(tModels);
        }

        [HttpGet("{id}")]
        public async Task<ContractViewModel> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var tModel = await _service.GetByIdAsync(id, cancellationToken);

            return _mapper.Map<ContractViewModel>(tModel);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task CreateAsync([FromBody] ContractViewModel tViewModel, CancellationToken cancellationToken)
        {
            var tModel = _mapper.Map<ContractModel>(tViewModel);
            await _service.CreateAsync(tModel, cancellationToken);
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task UpdateAsync(int id, [FromBody] ContractViewModel tViewModel, CancellationToken cancellationToken)
        {
            var tModel = _mapper.Map<ContractModel>(tViewModel);
            await _service.UpdateAsync(id, tModel, cancellationToken);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task DeleteByIdAsync(int id, CancellationToken cancellationToken)
        {
            await _service.DeleteByIdAsync(id, cancellationToken);
        }
    }
}
