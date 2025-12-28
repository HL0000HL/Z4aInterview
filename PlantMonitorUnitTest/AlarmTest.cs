using FluentAssertions;
using InterviewEmptyProject.Services;

namespace InterviewEmptyUnitTest;

public class Tests
{
    private const long JustBelowLowThreshold = 16;
    private const long AtLowThreshold = 17;
    private const long AtHighThreshold = 21;
    private const long JustAboveHighThreshold = 22;
    private Alarm _Target;
    private SensorWrapperTest _sensorWrapper;

    [SetUp]
    public void Setup()
    {
        _sensorWrapper = new SensorWrapperTest();
        _Target = new Alarm(_sensorWrapper);
    }

    [TestCase(JustBelowLowThreshold)]
    [TestCase(0)]
    [TestCase(-1)]
    [TestCase(long.MinValue)]
    public void GivenAlarmOffWhenMeasurementIsBelowRangeThenAlarmStart(long measurement)
    {
        GivenCurrentAlarmIsOff();
        WhenSensorMeasurementIs(measurement);
        ThenAlarmStart();
    }
    
    [TestCase(JustAboveHighThreshold)]
    [TestCase(long.MaxValue)]
    public void GivenAlarmOffWhenGetMeasurementIsAboveRangeThenAlarmStart(long measurement)
    {
        GivenCurrentAlarmIsOff();
        WhenSensorMeasurementIs(measurement);
        ThenAlarmStart();
    }
    
    [TestCase(AtLowThreshold)]
    [TestCase(18)]
    [TestCase(20)]
    [TestCase(AtHighThreshold)]
    public void GivenAlarmOffWhenGetMeasurementIsWithinRangeThenAlarmOff(long measurement)
    {
        GivenCurrentAlarmIsOff();
        WhenSensorMeasurementIs(measurement);
        ThenAlarmOff();
    }

    [Test]
    public void GivenAlarmOnWhenMeasurementIsOutRangeThenAlarmKeepOn()
    {
        GivenCurrentAlarmIsOnForCount(1);
        WhenSensorMeasurementIs(JustBelowLowThreshold);
        ThenAlarmKeepOnForCount(2);
    }
    
    [Ignore("clarify alarm off case")]
    [Test]
    public void GivenAlarmOnWhenMeasurementIsWithinRangeThenAlarmOff()
    {
        GivenCurrentAlarmIsOnForCount(1);
        WhenSensorMeasurementIs(AtLowThreshold);
        ThenAlarmOff();
    }

    private void ThenAlarmStart()
    {
        _Target.AlarmOn.Should().BeTrue();
        _Target.AlarmCount.Should().Be(1);
    }
    private void ThenAlarmKeepOnForCount(long count)
    {
        _Target.AlarmOn.Should().BeTrue();
        _Target.AlarmCount.Should().Be(count);
    }
    private void ThenAlarmOff()
    {
        _Target.AlarmOn.Should().BeFalse();
        _Target.AlarmCount.Should().Be(0);
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
    
    private void GivenCurrentAlarmIsOnForCount(long count)
    {
        _Target.AlarmOn.Should().BeFalse();
        _Target.AlarmCount.Should().Be(0);
        for (int i = 0; i < count; i++)
        {
            _sensorWrapper.SetMeasurementForTest(JustBelowLowThreshold);
            _Target.Check();
        }
        _Target.AlarmOn.Should().BeTrue();
        _Target.AlarmCount.Should().Be(count);
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