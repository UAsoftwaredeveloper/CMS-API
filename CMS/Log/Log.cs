using NLog;
using System.Diagnostics;

namespace CMS.Log
{
    public class Log
    {
        private static Logger logger = LogManager.GetLogger("TUCMSAPILogger");

        public static void Info(string message, string namespaces = "", string className = "", string methodName = "")
        {
            string formattedMessage = $"{namespaces} | {className} | {methodName} | {message}";
            logger.Info(formattedMessage);
        }

        public static void Error(string message, string namespaces = "", string className = "",
            string methodName = "")
        {
            string formattedMessage = $"{namespaces} | {className} | {methodName} | {message}";
            logger.Error(formattedMessage);
        }
        public static void Debug(string message, string namespaces = "", string className = "", string methodName = "")
        {
            string formatedMessage = string.Format("{0} | {1} | {2} | {3}", namespaces, className, methodName, message);
            logger.Debug(formatedMessage);
        }
        public static void Trace(string strnamespace, string className, string methodName, string message)
        {
            string formatedMessage = string.Format("{0} | {1} | {2} | {3}", strnamespace, className, methodName, message);
            logger.Trace(formatedMessage);
        }
        public static string GetCurrentMethod()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);
            return sf.GetMethod().Name;
        }
        public static string GetCurrentClass()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);
            return sf.GetMethod().DeclaringType.Name;
        }
        public static string GetCurrentNameSpace()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);
            return sf.GetMethod().DeclaringType.Namespace;
        }
    }
}
