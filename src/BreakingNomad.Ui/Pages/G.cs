using BreakingNomad.Shared;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using ProtoBuf.Grpc.Client;

namespace BreakingNomad.Ui.Pages
{
  public class GreeterClient
  {


    public GreeterClient()
    {

    }

    public async Task<string> SayHelloAsync(HelloRequest helloRequest)
    {
      var httpClient = new HttpClient(new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler()));
      var channel = GrpcChannel.ForAddress("http://localhost:5200", new GrpcChannelOptions { HttpClient = httpClient });

      var sayHelloAsync = await channel.CreateGrpcService<IGreeterService>().SayHelloAsync(new HelloRequest(){ Name = "asdf"});
      return "Hi"+sayHelloAsync;
    }
  }
}
