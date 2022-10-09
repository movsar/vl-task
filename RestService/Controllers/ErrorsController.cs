using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace RestService.Controllers {
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase {
        private ILogger<ErrorsController> _logger;
        public ErrorsController(ILogger<ErrorsController> logger) {
            _logger = logger;
        }

        [Route("error")]
        public object Error() {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error;
            _logger.LogError(exception?.Message);
            var code = 500;

            return new { errorMessage = "An error occured, check the payload", statusCode = code}; // Your error model
        }
    }
}
