using BreakingNomad.Api.Data;
using BreakingNomad.Shared;
using FluentAssertions;
using NUnit.Framework;
using ProtoBuf.Grpc;

namespace BreakingNomad.Api.Tests.Data;

public class MenuServiceTests
{
  private MenuService _sut = null!;
  
  [Test]
  public async Task AddPlannedTrip_GivenJsonStore_ShouldStoreToTheFile()
  {
    // arrange
    Setup();
    var plannedTrip = AddPlannedTripRequest.From("Test Trip");
    var plannedTrip2 = AddPlannedTripRequest.From("Test Update");
    plannedTrip2.MealsOfTheDay.Add(new MealsOfTheDay(2,MealType.Dessert,new List<string> { "Ice cream"}));
    CallContext callContext = CallContext.Default;
    // action
    
    var addPlannedTrip = await _sut.AddPlannedTrip(plannedTrip,callContext);
    var trip = await _sut.GetPlannedTrip(new PlannedTripByIdRequest(addPlannedTrip.Id),callContext);
    var plannedTrips = await _sut.GetPlannedTrips(new PlannedTripsRequest(),callContext);
    var updated = await _sut.UpdatePlannedTrip(new UpdatePlannedTripRequest(addPlannedTrip.Id,plannedTrip2),callContext);
    var getAfterUpdate = await _sut.GetPlannedTrip(new PlannedTripByIdRequest(addPlannedTrip.Id),callContext);
    var removed = await _sut.RemovePlannedTrips(new PlannedTripByIdRequest(addPlannedTrip.Id),callContext);
    var listAfterRemove = await _sut.GetPlannedTrips(new PlannedTripsRequest(),callContext);
    // assert
    addPlannedTrip.Id.Should().NotBe("1");
    trip.Name.Should().Be(addPlannedTrip.Name);
    plannedTrips.Trips.Select(x=>x.Id).Should().Contain(addPlannedTrip.Id);
    removed.Success.Should().Be(true);
    listAfterRemove.Trips.Select(x=>x.Id).Should().NotContain(addPlannedTrip.Id);
    getAfterUpdate.Name.Should().Be(plannedTrip2.Name);
    getAfterUpdate.MealsOfTheDay.Count.Should().Be(1);
    updated.Name.Should().Be(plannedTrip2.Name);
  }
  //
  // [Test]
  // public async Task AddPlannedTrip_GivenMoreOfRequest_ShouldStoreToTheFile()
  // {
  //   // arrange
  //   Setup();
  //   // action
  //   var original = await _sut.GetPlannedTrips(new PlannedTripsRequest(),null!);
  //   var enumerable = Enumerable.Range(1,2).AsParallel().Select(async (x)=> {
  //     var plannedTrip = new AddPlannedTripRequest() { Name = "Test Trip "+x };
  //     var addPlannedTrip = await _sut.AddPlannedTrip(plannedTrip,null!);
  //     await _sut.RemovePlannedTrips(new PlannedTripByIdRequest { Id = addPlannedTrip.Id},null!);
  //   } ).ToArray();
  //   await Task.WhenAll(enumerable);
  //   // assert
  //   var plannedTrips = await _sut.GetPlannedTrips(new PlannedTripsRequest(),null!);
  //   plannedTrips.Trips.Count.Should().Be(original.Trips.Count);
  // }

  private void Setup()
  {
    _sut = new MenuService(new DataStoreFactory("C:\\temp\\DataStore").PlannedTrips);
  }
}
