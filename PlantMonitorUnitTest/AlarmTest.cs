using FluentAssertions;
using InterviewEmptyProject.Services;

namespace InterviewEmptyUnitTest;

public class Tests
{
    private Alarm _Target;
    
    [SetUp]
    public void Setup()
    {
        _Target = new Alarm();
    }

    [Test]
    public void GivenBelowRangeAlarmOn()
    {
        
    }
}