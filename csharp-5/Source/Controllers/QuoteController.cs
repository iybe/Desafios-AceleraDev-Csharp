using System;
using System.Collections.Generic;
using Codenation.Challenge.Models;
using Codenation.Challenge.Services;
using Microsoft.AspNetCore.Mvc;

namespace Codenation.Challenge.Controllers
{
    [Route("api/quote")]
    [ApiController]
    public class QuoteController : ControllerBase
    {
        private readonly IQuoteService _service;

        public QuoteController(IQuoteService service)
        {
            _service = service;
        }

        // GET api/quote
        [HttpGet]
        public ActionResult<QuoteView> GetAnyQuote()
        {
            var randomQuote = _service.GetAnyQuote();
            return new QuoteView { 
                Id = randomQuote.Id, 
                Actor = randomQuote.Actor,
                Detail = randomQuote.Detail
            };
        }

        // GET api/quote/{actor}
        [HttpGet("{actor}")]
        public ActionResult<QuoteView> GetAnyQuote(string actor)
        {
            var quoteActor = _service.GetAnyQuote(actor);

            if(quoteActor == null)
            {
                return NotFound();
            }

            return new QuoteView
            {
                Id = quoteActor.Id,
                Actor = quoteActor.Actor,
                Detail = quoteActor.Detail
            };
        }

    }
}
