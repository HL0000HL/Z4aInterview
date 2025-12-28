namespace InterviewEmptyProject.Services
{
    public class Alarm
    {
        public Alarm()
        {
            _sensor = new SensorWrapper();
        }
        public Alarm(ISensorWrapper sensor)
        {
            _sensor = sensor;
        }
        private const double LowThreshold = 17;
        private const double HighThreshold = 21;

        private readonly ISensorWrapper _sensor;

        bool _alarmOn = false;
        private long _alarmCount = 0;


        public void Check()
        {
            double value = _sensor.GetMeasurement();

            if (value < LowThreshold | HighThreshold  < value)
            {
                _alarmOn = true;
                _alarmCount += 1;
            }
        }

        public bool AlarmOn
        {
            get { return _alarmOn; }
        }
        public long AlarmCount
        {
            get { return _alarmCount; }
        }
    }
}
