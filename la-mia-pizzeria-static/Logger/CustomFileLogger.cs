namespace la_mia_pizzeria_static.Logger
{
    public class CustomFileLogger : ICustomLogger
    {
        public void WriteLog(string message)
        {
            File.AppendAllText("C:\\Users\\user\\Desktop\\DotNet\\la-mia-pizzeria-static\\la-mia-pizzeria-static\\my-logs.txt", $"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} LOG: {message}\n");
        }
    }
}
