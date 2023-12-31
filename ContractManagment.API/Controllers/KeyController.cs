﻿using AutoMapper;
using ContractManagment.API.ViewModel;
using ContractManagment.BLL.Interfaces;
using ContractManagment.BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContractManagment.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    [Authorize]
    public class KeyController : Controller
    {
        private readonly IKeyService _service;
        protected readonly IMapper _mapper;

        public KeyController(IKeyService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<KeyViewModel>> GetAllAsync(CancellationToken cancellationToken)
        {
            var tModels = await _service.GetAllAsync(cancellationToken);

            return _mapper.Map<IEnumerable<KeyViewModel>>(tModels);
        }

        [HttpGet("{id}")]
        public async Task<KeyViewModel> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var tModel = await _service.GetByIdAsync(id, cancellationToken);

            return _mapper.Map<KeyViewModel>(tModel);
        }

        [HttpPost]
        [Authorize(Roles = "admin, manager")]
        public async Task CreateAsync([FromBody] KeyViewModel tViewModel, CancellationToken cancellationToken)
        {
            var tModel = _mapper.Map<KeyModel>(tViewModel);
            await _service.CreateAsync(tModel, cancellationToken);
        }

        [HttpPut]
        [Authorize(Roles = "admin, manager")]
        public async Task UpdateAsync(int id, [FromBody] KeyViewModel tViewModel, CancellationToken cancellationToken)
        {
            var tModel = _mapper.Map<KeyModel>(tViewModel);
            await _service.UpdateAsync(id, tModel, cancellationToken);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin, manager")]
        public async Task DeleteByIdAsync(int id, CancellationToken cancellationToken)
        {
            await _service.DeleteByIdAsync(id, cancellationToken);
        }
    }
}
