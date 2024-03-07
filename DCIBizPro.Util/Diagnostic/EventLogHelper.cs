using System.Diagnostics;

namespace DCIBizPro.Util.Diagnostics
{
	// Windows event log helper.
	public class EventLogHelper
	{
		private readonly static string m_eventLogSource = "XtraSuite2005";

		private EventLogHelper()
		{
		}

		// Checks for the existing of an event source. Returns true if the event 
		// source exists; otherwise false is returned.
		public static bool exists(string eventSourceName)
		{
			return EventLog.Exists(eventSourceName);
		}

		// Creates an event source name for the windows application event log.
		public static void createSource(string eventSourceName)
		{
			if (EventLog.Exists(eventSourceName) == false)
			{
				EventLog.CreateEventSource(eventSourceName, "Application");
			}
		}

		// Removes an event source name from the windows application event log.
		public static void removeSource(string eventSourceName)
		{
			if (EventLog.Exists(eventSourceName))
			{
				EventLog.DeleteEventSource(eventSourceName, "Application");
			}
		}

		// Logs an error to the application log.
		public static void logError(string message)
		{
			Debug.WriteLine(message);
			try
			{
				logEvent(m_eventLogSource, message, EventLogEntryType.Error);
			}catch{}
		}

		// Logs a failure audit message to the application event log.
		public static void logFailureAudit(string message)
		{
			Debug.WriteLine(message);

			try
			{
				logEvent(m_eventLogSource, message, EventLogEntryType.FailureAudit);
			}catch{}
		}

		// Logs a sucess audit message to the application event log.
		public static void logSuccessAudit(string message)
		{
			Debug.WriteLine(message);

			try
			{
				logEvent(m_eventLogSource, message, EventLogEntryType.SuccessAudit);
			}catch{}
		}

		// Logs a warning message to the application event log.
		public static void logWarning(string message)
		{
			Debug.WriteLine(message);

			try
			{
				logEvent(m_eventLogSource, message, EventLogEntryType.Warning);
			}catch{}
		}

		// Logs an information message to the application event log.
		public static void logInformation(string message)
		{
			Debug.WriteLine(message);

			try
			{
				logEvent(m_eventLogSource, message, EventLogEntryType.Information);
			}catch{}
		}

		// Log an message to the Windows Application Event Log with a specified type.
		private static void logEvent(string eventLogSource, string message, EventLogEntryType eventLogEntryType)
		{
			Debug.WriteLine(message);

			try
			{
				EventLog.WriteEntry(eventLogSource, message, eventLogEntryType);
			}catch{}
		}
	}
}