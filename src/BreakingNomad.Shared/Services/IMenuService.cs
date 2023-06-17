using System.ServiceModel;
using ProtoBuf.Grpc;

namespace BreakingNomad.Shared.Services;

[ServiceContract]
public interface IMenuService
{
  [OperationContract]
  Task<PlannedTripsResponse> GetPlannedTrips(PlannedTripsRequest request, CallContext context = default);

  [OperationContract]
  Task<PlannedTripResponse> GetPlannedTrip(PlannedTripByIdRequest request, CallContext context = default);

  [OperationContract]
  Task<PlannedTripResponse> AddPlannedTrip(AddPlannedTripRequest request, CallContext context = default);

  [OperationContract]
  Task<PlannedTripResponse> UpdatePlannedTrip(UpdatePlannedTripRequest request, CallContext context = default);

  [OperationContract]
  Task<SuccessOrNotResponse> RemovePlannedTrips(PlannedTripByIdRequest request, CallContext context = default);
}