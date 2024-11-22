using ERP_InsightWise.Service.CEP;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using System.Net;

namespace ERP_InsightWise.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CEPController : ControllerBase
    {
        private readonly ICEPService _cepService;

        public CEPController(ICEPService cepService)
        {
            _cepService = cepService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(AddressResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public Task<AddressResponse> GetAddressResponse(string cep)
        {
            return _cepService.GetAddressbyCEP(cep); ;
        }
    }
}
