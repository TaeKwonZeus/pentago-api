using Microsoft.AspNetCore.Mvc;

namespace Pentago.Controllers;

/// <summary>
/// This controller represents a test endpoint.
/// </summary>
[Route("test")]
[ApiController]
public class TestController : ControllerBase
{
    /// <summary>
    /// Returns a test payload.
    /// </summary>
    /// <returns>A test payload.</returns>
    [HttpGet]
    public string Get()
    {
        return "Test endpoint";
    }
}