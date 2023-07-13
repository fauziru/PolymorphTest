using System.ComponentModel.DataAnnotations;
namespace PolymorphTest.FStatisticInterval;

public class AnemoRequest
{
    [Required]
    public string valueKey { get; set; } = null!;
    [Required]
    public string windrose { get; set; } = null!;
}

public class FFTRequest
{
    [Required]
    public string valueKey { get; set; } = null!;
    [Required]
    public string test { get; set; } = null!;
}

public class AnemoResponse
{
    public string valueKey { get; set; }
    public string windrose { get; set; }
    public AnemoResponse(StatisticInterval statisticInterval)
    {
        valueKey = statisticInterval.dataPayload.valueKey;
        windrose = ((Anemo)statisticInterval.dataPayload).windrose;
    }
}

public class FFTResponse
{
    public string valueKey { get; set; }
    public string test { get; set; }
    public FFTResponse(StatisticInterval statisticInterval)
    {
        valueKey = statisticInterval.dataPayload.valueKey;
        test = ((FFT)statisticInterval.dataPayload).test;
    }
}