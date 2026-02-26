using InternetShop.Contract.Responses;
using InternetShop.Service;
using Microsoft.AspNetCore.Mvc;

namespace InternetShop.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[Controller]")]
    [ApiController]
    public class OrderLineController : ControllerBase
    {

    }
}
