using Microsoft.AspNetCore.Mvc;

namespace Pentago.Api.Controllers;

/// <summary>
/// This controller represents a test endpoint.
/// </summary>
[Route("[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    /// <summary>
    /// Returns a test payload.
    /// </summary>
    /// <returns>A test payload.</returns>
    [HttpGet]
    public string Get() => "Test endpoint";
}