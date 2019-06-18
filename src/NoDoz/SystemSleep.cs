namespace NoDoz
{
    internal class SystemSleep
    {
        public static void Prevent()
        {
            NativeMethods.SetThreadExecutionState(NativeMethods.ES_CONTINUOUS | NativeMethods.ES_SYSTEM_REQUIRED);
        }
    }
}
