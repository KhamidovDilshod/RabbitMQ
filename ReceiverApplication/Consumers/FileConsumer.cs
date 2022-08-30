using Common;
using MassTransit;

namespace ReceiverApplication.Consumers;

public class FileConsumer : IConsumer<FileProcess>
{
    public async Task Consume(ConsumeContext<FileProcess> context)
    {
        var data = context.Message;
        Random random = new Random();
        string fName = random.Next(1, 20).ToString();
        string path = $@"C:\Users\user\Desktop\LOGS\new\{fName}.html";
        var file = File.AppendText(path);
        await file.WriteAsync(await DownloadString());
        await file.FlushAsync();
        file.Close();
        var details = File.Open(path, FileMode.Open);
        var fileDetail = new FileProcess
        {
            FileName = details.Name,
            FilePath = path,
            IsSuccess = true
        };
        details.Close();
        await context.RespondAsync<FileUpdate>(fileDetail);
    }

    private async Task<string> DownloadString()
    {
        Object o = new object();        
        const string url = "https://youtu.be/L9UUUPedidg";
        using var client = new HttpClient();
        var response = await client.GetAsync(new Uri(url));
        var result = await response.Content.ReadAsStringAsync();
        return result;
    }
}