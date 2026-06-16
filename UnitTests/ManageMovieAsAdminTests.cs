namespace UnitTests;

[TestClass]
public sealed class ManageMovieAsAdminTests
{
    [DataTestMethod]
    [DataRow("Test Movie Add", 120, "Test summary", "Test Director", 12, "Action", 2026)]
    public void AddMovie_ReturnsAddedMovie(string title, long duration, string summary, string director, long ageRating, string genre, long releaseDate)
    {
        // arrange
        MovieModel movie = new(-1, title, duration, summary, director, ageRating, genre, releaseDate);

        // act
        MoviesLogic.AddMovie(movie);
        MovieModel? result = MoviesLogic.GetMovieByTitle(title);

        // assert
        Assert.IsNotNull(result);
        Assert.AreEqual(title, result.Title);
        Assert.AreEqual(duration, result.Duration);
    }

    [DataTestMethod]
    [DataRow("Test Movie Edit", "Test Movie Edited", 100, 130, "Old summary", "Old Director", 12, "Drama", 2025)]
    public void EditMovie_ReturnsEditedMovie(string title, string editedTitle, long duration, long editedDuration, string summary, string director, long ageRating, string genre, long releaseDate)
    {
        // arrange
        MovieModel movie = new(-1, title, duration, summary, director, ageRating, genre, releaseDate);
        MoviesLogic.AddMovie(movie);
        MovieModel? addedMovie = MoviesLogic.GetMovieByTitle(title);

        Assert.IsNotNull(addedMovie);

        // act
        addedMovie.Title = editedTitle;
        addedMovie.Duration = editedDuration;
        MoviesLogic.EditMovie(addedMovie);
        MovieModel? result = MoviesLogic.GetById(addedMovie.Id);

        // assert
        Assert.IsNotNull(result);
        Assert.AreEqual(editedTitle, result.Title);
        Assert.AreEqual(editedDuration, result.Duration);
    }

    [TestMethod]
    public void DisableMovie_ReturnsInactiveMovie()
    {
        // arrange
        MovieModel movie = new(-1, "Test Movie Disable", 90, "Test summary", "Test Director", 6, "Comedy", 2024);
        MoviesLogic.AddMovie(movie);
        MovieModel? addedMovie = MoviesLogic.GetMovieByTitle("Test Movie Disable");

        Assert.IsNotNull(addedMovie);

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
