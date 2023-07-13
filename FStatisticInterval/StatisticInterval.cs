using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PolymorphTest.FStatisticInterval;

public class StatisticInterval
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string type { get; set; }
    public DataPayload dataPayload { get; set; }
    public StatisticInterval(DataPayload _dataPayload, string _type)
    {
        dataPayload = _dataPayload;
        type = _type;
    }
}

[BsonKnownTypes(typeof(FFT), typeof(Anemo))]
public class DataPayload
{
    public string valueKey { get; set; }
    public DataPayload(string _valueKey)
    {
        valueKey = _valueKey;
    }
}

public class FFT : DataPayload
{
    public string test { get; set; }
    public FFT(string _test, string _valueKey) : base(_valueKey)
    {
        test = _test;
        valueKey = _valueKey;
    }
}

public class Anemo : DataPayload
{
    public string windrose { get; set; }
    public Anemo(string _windrose, string _valueKey) : base(_valueKey)
    {
        windrose = _windrose;
    }
}
