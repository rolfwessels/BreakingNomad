using ProtoBuf.Grpc;
using System.ServiceModel;
using System.Runtime.Serialization;
using BreakingNomad.Ui.Components.MenuMaker.Models;

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
  Task<HelloReply> SayHelloAsync(HelloRequest request, CallContext context = default);
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

public class AddPlannedTripRequest
{
  public string Name { get; set; }
  public DateTime StartDate { get; set; }
}
public class MealsOfTheDay
{
  public int Day { get; set; }
  public MealType Meal { get; set; }
  public List<string>? Options { get; set; }
}
