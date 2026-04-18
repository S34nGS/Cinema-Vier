public static class Program {
    public static void Main() {
        CreateMoviesTable();
    }

    public static void CreateMoviesTable()
    {
        // generate a list of movies to insert into the database
        List<MovieModel> moviesList = [
            new MovieModel(1, "The Shawshank Redemption", 142, "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.", "Frank Darabont", 15, "Drama", 1994),
            new MovieModel(2, "The Godfather", 175, "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.", "Francis Ford Coppola", 18, "Crime, Drama", 1972),
            new MovieModel(3, "The Dark Knight", 152, "When the menace known as the Joker wreaks havoc and chaos on the people of Gotham, Batman must accept one of the greatest psychological and physical tests of his ability to fight injustice.", "Christopher Nolan", 15, "Action, Crime, Drama", 2008)
        ];

        MoviesAccess movies = new();
        movies.CreateTable();

        foreach (MovieModel movie in moviesList)
        {
            movies.Write(movie);
        }
    }
}