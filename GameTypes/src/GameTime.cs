using System;
using System.Collections.Generic;
using System.Text;

namespace GameTypes
{
    [Serializable]
    public struct GameTime
    {
        public int days;
        public int hours;
        public int minutes;
        public float seconds;
		
		const int SECONDS_PER_MINUTE = 60;
		const int SECONDS_PER_HOUR = 3600;
		const int SECONDS_PER_DAY = 3600 * 24;
		
		public GameTime(GameTime pGameTime)
		{
			days = pGameTime.days;
			hours = pGameTime.hours;
			minutes = pGameTime.hours;
			seconds = pGameTime.seconds;
		}
		
		public GameTime(float pTotalSeconds)
		{
			days = (int)(pTotalSeconds / (float)SECONDS_PER_DAY);
			float restSeconds = pTotalSeconds - (days * SECONDS_PER_DAY);
			
			hours = (int)(restSeconds / (float)SECONDS_PER_HOUR);
			restSeconds -= (hours * (float)SECONDS_PER_HOUR);
			
			minutes = (int)(restSeconds / (float)SECONDS_PER_MINUTE);
			restSeconds -= (minutes * (float)SECONDS_PER_MINUTE);
			
			seconds = restSeconds;
		}
		
		public GameTime(int pHours, int pMinutes) {
			days = 0;
			hours = pHours;
			minutes = pMinutes;
			seconds = 0;
		}
		
		public GameTime(int pDays, int pHours, int pMinutes, float pSeconds) {
			days = pDays;
			hours = pHours;
			minutes = pMinutes;
			seconds = pSeconds;
		}
        
        public void Tick(float pDeltaTime)
        {
            seconds += pDeltaTime;
            while (seconds >= 60.0f)
            {
                seconds -= 60.0f;
                minutes++;
            }
            while (minutes >= 60)
            {
                minutes -= 60;
                hours++;
            }
            while (hours >= 24)
            {
                hours -= 24;
                days++;
            }
        }
		
		public void TurnForwardToTime(int pHour, int pMinute) {
			string prevTime = this.ToString();
			if(pHour <= hours) {
				days++;
			}
			if(pMinute <= minutes) {
				hours++;
			}
			hours = pHour;
			minutes = pMinute;
			D.Log("Turning forward time from " + prevTime + " to " + this.ToString());
		}
		
        public override string ToString()
        {
			string hoursString = hours.ToString();
			if(hoursString.Length == 1) { hoursString = "0" + hoursString; }
			string minutesString = minutes.ToString();
			if(minutesString.Length == 1) { minutesString = "0" + minutesString; }
			string secondsString = Math.Floor(seconds).ToString();
			if(secondsString.Length == 1) { secondsString = "0" + secondsString; }
            return String.Format("Day {0}, {1}:{2}:{3}", days, hoursString, minutesString, secondsString);
        }

        public float normalizedDayTime
        {
            get 
            {
                float totalSecondsOfDay = (int)seconds + minutes * SECONDS_PER_MINUTE + hours * SECONDS_PER_HOUR;
                float normalizedTime = ((float)totalSecondsOfDay) / ((float)(SECONDS_PER_DAY));
                return normalizedTime;
            }
        }

        public float totalSeconds
        {
            get
            {
                return seconds + (float)(minutes * SECONDS_PER_MINUTE + hours * SECONDS_PER_HOUR + days * SECONDS_PER_DAY);
            }
			set 
			{
				float secondsPart= value;
                days = (int)(secondsPart / SECONDS_PER_DAY);
                secondsPart -= SECONDS_PER_DAY * days;
                hours = (int)(secondsPart / SECONDS_PER_HOUR);
                secondsPart -= SECONDS_PER_HOUR * hours;
				minutes = (int)(secondsPart / SECONDS_PER_MINUTE);
                secondsPart -= SECONDS_PER_MINUTE * minutes;
				seconds = secondsPart;
			}
        }
        public GameTime Now()
        {
            GameTime n = new GameTime();
            n.days = this.days;
            n.hours = this.hours;
            n.minutes = this.minutes;
            n.seconds = this.seconds;
            return n;
        }
		
		public static GameTime operator+(GameTime g1, GameTime g2) {
			GameTime newTime = new GameTime();
			float s = g1.totalSeconds + g2.totalSeconds;
			newTime.totalSeconds = s;
			return newTime;
		}
		
		public static GameTime operator-(GameTime g1, GameTime g2) {
			GameTime newTime = new GameTime();
			float s = g1.totalSeconds - g2.totalSeconds;
			newTime.totalSeconds = s;
			return newTime;
		}
		public static bool operator>(GameTime g1, GameTime g2) {
			return g1.totalSeconds > g2.totalSeconds;
		}
		public static bool operator>=(GameTime g1, GameTime g2) {
			return g1.totalSeconds >= g2.totalSeconds;
		}
		public static bool operator<(GameTime g1, GameTime g2) {
			return g1.totalSeconds < g2.totalSeconds;
		}
		public static bool operator<=(GameTime g1, GameTime g2) {
			return g1.totalSeconds <= g2.totalSeconds;
		}
		public static bool operator==(GameTime g1, GameTime g2) {
			return (int)g1.totalSeconds == (int)g2.totalSeconds;
		}
		public static bool operator!=(GameTime g1, GameTime g2) {
			return !(g1 == g2);
		}
		public override bool Equals(object obj)
		{
			if(obj is GameTime) {
				return (GameTime)obj == this;
			}
			else {
				return false;
			}
		}
		public override int GetHashCode()
		{
			return base.GetHashCode ();
		}
        public bool isDaytime
        {
            get { return normalizedDayTime > 0.3f; }
        }

		/// <summary>
		/// Ignores the day! Only checks hours and minutes.
		/// </summary>
		public bool IsWithinMinuteBounds(GameTime startTime, GameTime endTime)
		{
			GameTime startTimeIgnoreDay = new GameTime(startTime.hours, startTime.minutes);
			GameTime endTimeIgnoreDay = new GameTime(endTime.hours, endTime.minutes);
			GameTime thisTimeIgnoreDay = new GameTime(this.hours, this.minutes);

			if(startTimeIgnoreDay < endTimeIgnoreDay) {
				return  (startTimeIgnoreDay.totalSeconds <= thisTimeIgnoreDay.totalSeconds) &&
						(thisTimeIgnoreDay.totalSeconds < endTimeIgnoreDay.totalSeconds);
			}
			else {
				// When the timespan wraps over midnight
				//Console.WriteLine("StartTime " + startTimeIgnoreDay.totalSeconds + " endTime " + endTimeIgnoreDay.totalSeconds + " thisTime " + thisTimeIgnoreDay.totalSeconds + " withinBounds " + withinBounds);
				bool inFirstHalf = thisTimeIgnoreDay.totalSeconds <= endTimeIgnoreDay.totalSeconds;
				bool inLastHalf = thisTimeIgnoreDay.totalSeconds >= startTimeIgnoreDay.totalSeconds;
				return inFirstHalf || inLastHalf;
			}
		}
    }
}
