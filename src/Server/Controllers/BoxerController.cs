using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VirtuaBoxer.Shared.Boxers;

namespace VirtuaBoxer.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class BoxerController : ControllerBase
{
    private readonly IBoxerService boxerService;

    public BoxerController(IBoxerService boxerService)
    {
        this.boxerService = boxerService;
    }

    [AllowAnonymous]
    [HttpGet("{BoxerId}")]
    public Task<BoxerDto.Detail> GetDetailAsync([FromRoute] int boxerId)
    {
        return boxerService.GetDetailAsync(boxerId);
    }

    [AllowAnonymous]
    [HttpGet]
    public Task<BoxerResponse.GetIndex> GetIndexAsync([FromQuery] BoxerRequest.GetIndex request)
    {
        Console.WriteLine("Request ontvangen");
        return boxerService.GetIndexAsync(request);
    }
}
