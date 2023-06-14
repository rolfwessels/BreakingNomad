using BreakingNomad.Api.Data;
using FluentAssertions;
using Food;
using NUnit.Framework;

namespace BreakingNomad.Api.Tests.Data;

public class MenuServiceTests
{
  private MenuService _sut;

  [Test]
  public async Task AddPlannedTrip_GivenJsonStore_ShouldStoreToTheFile()
  {
    // arrange
    Setup();
    var plannedTrip = new PlannedTrip() {
      Id = "1", Name = "Test Trip" };
    // action


    var addPlannedTrip = await _sut.AddPlannedTrip(plannedTrip,null!);
    var plannedTrips = await _sut.GetPlannedTrips(new PlannedTripsRequest(),null!);
    var removed = await _sut.RemovePlannedTrips(new RemovePlannedTripsRequest() { Id = addPlannedTrip.Id},null!);
    var plannedTrips2 = await _sut.GetPlannedTrips(new PlannedTripsRequest(),null!);

    // assert
    addPlannedTrip.Id.Should().NotBe("1");
    plannedTrips.Trips.Select(x=>x.Id).Should().Contain(addPlannedTrip.Id);
    removed.Success.Should().Be(true);
    plannedTrips2.Trips.Select(x=>x.Id).Should().NotContain(addPlannedTrip.Id);
  }

  [Test]
  public async Task AddPlannedTrip_GivenMoreOfRequest_ShouldStoreToTheFile()
  {
    // arrange
    Setup();
    // action
    var enumerable = Enumerable.Range(1,2).AsParallel().Select(async (x)=> {
      var plannedTrip = new PlannedTrip() {
        Id = "1", Name = "Test Trip "+x };
      var addPlannedTrip = await _sut.AddPlannedTrip(plannedTrip,null!);
      var removed = await _sut.RemovePlannedTrips(new RemovePlannedTripsRequest() { Id = addPlannedTrip.Id},null!);
    } ).ToArray();
    await Task.WhenAll(enumerable);
  

    // assert
    var plannedTrips = await _sut.GetPlannedTrips(new PlannedTripsRequest(),null!);
    plannedTrips.Trips.Count.Should().Be(0);
  }

  private void Setup()
  {
    var jsonDataStore = new JsonDataStore<PlannedTrip>("C:\\temp\\DataStore", (id,trip) => trip.Id = id, trip => trip.Id); 
    _sut = new MenuService(jsonDataStore);
  }
}
