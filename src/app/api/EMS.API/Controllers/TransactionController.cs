namespace EMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IWalletService _walletService;
        public TransactionController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        [Authorize(Roles = $"{ApplicationUserRoleName.AdminRoleName},{ApplicationUserRoleName.EmployeeRoleName}")]
        [HttpGet, Route("getbalance")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get() => Ok(await _walletService.GetBalance());

        
        [Authorize(Roles = ApplicationUserRoleName.AdminRoleName)]
        [HttpPost, Route("postpayment")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Post(TransactionRequest transactionRequest) => Ok(await _walletService.PostPayment(transactionRequest));

        
        [Authorize(Roles = ApplicationUserRoleName.AdminRoleName)]
        [HttpPost, Route("bulkpostpayment")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Post(List<TransactionRequest> transactionRequests) => Ok(await _walletService.BulkPostPayment(transactionRequests));
    }
}