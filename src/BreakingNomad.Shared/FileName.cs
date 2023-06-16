using System.Runtime.Serialization;
using System.ServiceModel;
using ProtoBuf.Grpc;

namespace BreakingNomad.Shared;

[DataContract]
public class HelloReply
{
  [DataMember(Order = 1)] public string Message { get; set; }
}

[DataContract]
public class HelloRequest
{
  [DataMember(Order = 1)] public string Name { get; set; }
}

[ServiceContract]
public interface IGreeterService
{
  [OperationContract]
  Task<HelloReply> SayHelloAsync(HelloRequest request,
    CallContext context = default);
}

public class GreeterService : IGreeterService
{
  public Task<HelloReply> SayHelloAsync(HelloRequest request, CallContext context = default)
  {
    return Task.FromResult(
      new HelloReply
      {
        Message = $"Hello {request.Name}"
      });
  }
}


public record PlannedTripResponse()
{
  public string Id { get; set; }
}


