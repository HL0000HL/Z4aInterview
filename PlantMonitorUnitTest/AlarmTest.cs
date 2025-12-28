using FluentAssertions;
using InterviewEmptyProject.Services;

namespace InterviewEmptyUnitTest;

public class Tests
{
    private Alarm _Target;
    private SensorWrapperTest _sensorWrapper;

    [SetUp]
    public void Setup()
    {
        _sensorWrapper = new SensorWrapperTest();
        _Target = new Alarm(_sensorWrapper);
    }

    [Test]
    public void GivenBelowRangeWhenGetMeasurementThenAlarmOn()
    {
        _Target.AlarmOn.Should().BeFalse();
        _Target.AlarmCount.Should().Be(0);
        _sensorWrapper.SetMeasurementForTest(14);
        _Target.Check();
        _Target.AlarmOn.Should().BeTrue();
        _Target.AlarmCount.Should().Be(1);
    }
}

public class SensorWrapperTest : ISensorWrapper
{
    private double _measurement;

    public double GetMeasurement()
    {
        return _measurement;
    }

    public void SetMeasurementForTest(double measurement)
    {
        _measurement = measurement;
    }
}