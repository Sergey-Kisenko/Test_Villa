namespace MagicVilla_VillaApi.CustomLogs
{
    public class MyLogs : IMyLogs
    {
        public void Logs(string txt)
        {
            Console.WriteLine($"Log: {txt}");
        }
    }
}
