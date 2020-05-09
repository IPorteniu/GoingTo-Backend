﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using GoingTo_API.Domain.Models;
using GoingTo_API.Domain.Services;
using GoingTo_API.Resources;
using GoingTo_API.Extensions;

namespace GoingTo_API.Controllers
{
    [Route("/api/[controller]")]
    public class LocatablesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILocatableService _locatableService;
       
        public LocatablesController(ILocatableService locatableService,IMapper mapper)
        {
            _mapper = mapper;
            _locatableService = locatableService;
        }

        [HttpGet]
        public async Task<IEnumerable<LocatableResource>> GetAllAsync()
        {
            var locatables = await _locatableService.ListAsync();
            var resources = _mapper
                .Map<IEnumerable<Locatable>, IEnumerable<LocatableResource>>(locatables);
            return resources;
        }

        // Porcion en desarrolllo, pensada para el uso de los partners

        //[HttpPost]
        //public async Task<IActionResult> PostAsync([FromBody] SaveLocatableResource resource)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState.GetErrorMessages());
        //    var locatable = _mapper.Map<SaveLocatableResource, Locatable>(resource);
        //    var result = await _locatableService.SaveAsync(locatable);
        //
        //    if (!result.Success)
        //        return BadRequest(result.Message);
        //
        //    var locatableresource = _mapper.Map<Locatable, LocatableResource>(result.Resource);
        //    return Ok(locatableresource);
        //}


    }
}
