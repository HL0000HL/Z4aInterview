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

    [TestCase(16)]
    [TestCase(0)]
    [TestCase(-1)]
    [TestCase(long.MinValue)]
    public void GivenBelowRangeWhenGetMeasurementThenAlarmStart(long measurement)
    {
        GivenCurrentAlarmIsOff();
        WhenSensorMeasurementIs(measurement);
        ThenAlarmStart();
    }
    
    [TestCase(22)]
    [TestCase(long.MaxValue)]
    public void GivenAboveRangeWhenGetMeasurementThenAlarmStart(long measurement)
    {
        GivenCurrentAlarmIsOff();
        WhenSensorMeasurementIs(measurement);
        ThenAlarmStart();
    }
    

    private void ThenAlarmStart()
    {
        _Target.AlarmOn.Should().BeTrue();
        _Target.AlarmCount.Should().Be(1);
    }

    private void WhenSensorMeasurementIs(long measurement)
    {
        _sensorWrapper.SetMeasurementForTest(measurement);
        _Target.Check();
    }

    private void GivenCurrentAlarmIsOff()
    {
        _Target.AlarmOn.Should().BeFalse();
        _Target.AlarmCount.Should().Be(0);
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