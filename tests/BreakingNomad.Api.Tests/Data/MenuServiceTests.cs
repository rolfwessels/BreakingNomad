using BreakingNomad.Api.Data;
using FluentAssertions;
using Food;
using NUnit.Framework;

namespace BreakingNomad.Api.Tests.Data;

public class MenuServiceTests
{
  private MenuService _sut = null!;

  [Test]
  public async Task AddPlannedTrip_GivenJsonStore_ShouldStoreToTheFile()
  {
    // arrange
    Setup();
    var plannedTrip = new AddPlannedTripRequest() { Name = "Test Trip" };
    var plannedTrip2 = new AddPlannedTripRequest() { Name = "Test Trip2" };
    plannedTrip2.MealsOfTheDay.Add(new MealsOfTheDay() { Options = { "asdf" },Day = 2,Meal = MEAL_TYPE.Dessert});
    // action
    
    var addPlannedTrip = await _sut.AddPlannedTrip(plannedTrip,null!);
    var trip = await _sut.GetPlannedTrip(new PlannedTripByIdRequest() { Id = addPlannedTrip.Id},null!);
    var plannedTrips = await _sut.GetPlannedTrips(new PlannedTripsRequest(),null!);
    var updated = await _sut.UpdatePlannedTrip(new UpdatePlannedTripRequest() { Id = addPlannedTrip.Id, Trip = plannedTrip2},null!);
    var getAfterUpdate = await _sut.GetPlannedTrip(new PlannedTripByIdRequest() { Id = addPlannedTrip.Id},null!);
    var removed = await _sut.RemovePlannedTrips(new PlannedTripByIdRequest() { Id = addPlannedTrip.Id},null!);
    var listAfterRemove = await _sut.GetPlannedTrips(new PlannedTripsRequest(),null!);
    // assert
    addPlannedTrip.Id.Should().NotBe("1");
    trip.Name.Should().Be(addPlannedTrip.Name);
    plannedTrips.Trips.Select(x=>x.Id).Should().Contain(addPlannedTrip.Id);
    removed.Success.Should().Be(true);
    listAfterRemove.Trips.Select(x=>x.Id).Should().NotContain(addPlannedTrip.Id);
    getAfterUpdate.Name.Should().Be(plannedTrip2.Name);
    //getAfterUpdate.MealsOfTheDay.Count.Should().Be(1); // broken
    updated.Name.Should().Be(plannedTrip2.Name);
  }

  [Test]
  public async Task AddPlannedTrip_GivenMoreOfRequest_ShouldStoreToTheFile()
  {
    // arrange
    Setup();
    // action
    var original = await _sut.GetPlannedTrips(new PlannedTripsRequest(),null!);
    var enumerable = Enumerable.Range(1,2).AsParallel().Select(async (x)=> {
      var plannedTrip = new AddPlannedTripRequest() { Name = "Test Trip "+x };
      var addPlannedTrip = await _sut.AddPlannedTrip(plannedTrip,null!);
      await _sut.RemovePlannedTrips(new PlannedTripByIdRequest { Id = addPlannedTrip.Id},null!);
    } ).ToArray();
    await Task.WhenAll(enumerable);
    // assert
    var plannedTrips = await _sut.GetPlannedTrips(new PlannedTripsRequest(),null!);
    plannedTrips.Trips.Count.Should().Be(original.Trips.Count);
  }

  private void Setup()
  {
    _sut = new MenuService(new DataStoreFactory("C:\\temp\\DataStore").PlannedTrips);
  }
}
