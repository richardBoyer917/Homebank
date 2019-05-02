﻿using Homebank.Api.UseCases.Categories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Homebank.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        public async Task<IActionResult> Create(Create.Command request)
        {
            var response = await _mediator.Send(request);

            return CreatedAtRoute("api/category", response);
        }

        [HttpPut]
        public async Task<IActionResult> Create(Update.Command request)
        {
            return Ok(await _mediator.Send(request));
        }
    }
}