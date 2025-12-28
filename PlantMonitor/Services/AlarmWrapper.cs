namespace InterviewEmptyProject.Services;

public class SensorWrapper : ISensorWrapper
{
    private readonly Sensor _sensor = new();

    public double GetMeasurement()
    {
        return _sensor.NextMeasure();
    }
}

public interface ISensorWrapper
{
    public double GetMeasurement();
}