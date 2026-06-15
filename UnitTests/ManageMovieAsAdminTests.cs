namespace UnitTests;

[TestClass]
public sealed class ManageMovieAsAdminTests
{
    [TestMethod]
    public void AddMovie_ReturnsAddedMovie()
    {
        // arrange
        MovieModel movie = new(-1, "Test Movie Add", 120, "Test summary", "Test Director", 12, "Action", 2026);

        // act
        MoviesLogic.AddMovie(movie);
        MovieModel? result = MoviesLogic.GetMovieByTitle("Test Movie Add");

        // assert
        Assert.IsNotNull(result);
        Assert.AreEqual("Test Movie Add", result.Title);
        Assert.AreEqual(120, result.Duration);
    }

    [TestMethod]
    public void EditMovie_ReturnsEditedMovie()
    {
        // arrange
        MovieModel movie = new(-1, "Test Movie Edit", 100, "Old summary", "Old Director", 12, "Drama", 2025);
        MoviesLogic.AddMovie(movie);
        MovieModel? addedMovie = MoviesLogic.GetMovieByTitle("Test Movie Edit");

        // act
        addedMovie.Title = "Test Movie Edited";
        addedMovie.Duration = 130;
        MoviesLogic.EditMovie(addedMovie);
        MovieModel? result = MoviesLogic.GetById(addedMovie.Id);

        // assert
        Assert.IsNotNull(result);
        Assert.AreEqual("Test Movie Edited", result.Title);
        Assert.AreEqual(130, result.Duration);
    }

    [TestMethod]
    public void DisableMovie_ReturnsInactiveMovie()
    {
        // arrange
        MovieModel movie = new(-1, "Test Movie Disable", 90, "Test summary", "Test Director", 6, "Comedy", 2024);
        MoviesLogic.AddMovie(movie);
        MovieModel? addedMovie = MoviesLogic.GetMovieByTitle("Test Movie Disable");

        // act
        addedMovie.IsActive = 0;
        MoviesLogic.DisableMovie(addedMovie);
        MovieModel? result = MoviesLogic.GetById(addedMovie.Id);

        // assert
        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.IsActive);
    }

    [DataTestMethod]
    [DataRow(1, true)]
    [DataRow(0, false)]
    public void AccountIsAdmin_ReturnsCorrectResult(long isAdmin, bool expected)
    {
        // arrange
        AccountModel account = new(0, "test@test.com", "password", "Test", "User", 0, isAdmin);

        // act
        bool result = account.IsAdmin == 1;

        // assert
        Assert.AreEqual(expected, result);
    }
}