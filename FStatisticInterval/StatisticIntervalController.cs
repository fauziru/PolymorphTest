using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
namespace PolymorphTest.FStatisticInterval;

[ApiController]
[Route("api/[controller]")]
public class StatisticIntervalController : ControllerBase
{
    private readonly ILogger<StatisticIntervalController> _logger;
    private readonly StatisticIntervalService _statisticIntervalService;
    public StatisticIntervalController(ILogger<StatisticIntervalController> logger, StatisticIntervalService statisticIntervalService)
    {
        _logger = logger;
        _statisticIntervalService = statisticIntervalService;
    }

    [HttpGet]
    public async Task<List<dynamic>> Get([FromQuery] string type)
    {
        var test = await _statisticIntervalService.GetAsync(type);
        if (type == "Anemo") return test.Select(x => (dynamic)new AnemoResponse(x)).ToList();
        else return test.Select(x => (dynamic)new FFTResponse(x)).ToList();
    }

    [HttpPost("anemo")]
    public async Task<IActionResult> StoreAnemo(AnemoRequest request)
    {
        Anemo anemo = new Anemo(request.windrose, request.valueKey);
        StatisticInterval newStatisticInterval = new StatisticInterval(anemo, typeof(Anemo).Name);
        await _statisticIntervalService.CreateAsync(newStatisticInterval);
        return CreatedAtAction(nameof(Get), new { id = newStatisticInterval.Id }, newStatisticInterval);
    }
    [HttpPost("fft")]
    public async Task<IActionResult> StoreFFT(FFTRequest request)
    {
        FFT fft = new FFT(request.test, request.valueKey);
        StatisticInterval newStatisticInterval = new StatisticInterval(fft, typeof(FFT).Name);
        await _statisticIntervalService.CreateAsync(newStatisticInterval);
        return CreatedAtAction(nameof(Get), new { id = newStatisticInterval.Id }, newStatisticInterval);
    }
}
