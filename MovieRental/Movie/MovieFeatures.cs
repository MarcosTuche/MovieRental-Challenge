using MovieRental.Data;

namespace MovieRental.Movie
{
	public class MovieFeatures : IMovieFeatures
	{
		private readonly MovieRentalDbContext _movieRentalDb;
		public MovieFeatures(MovieRentalDbContext movieRentalDb)
		{
			_movieRentalDb = movieRentalDb;
		}
		
		public Movie Save(Movie movie)
		{
			_movieRentalDb.Movies.Add(movie);
			_movieRentalDb.SaveChanges();
			return movie;
		}

		// TODO: tell us what is wrong in this method? Forget about the async, what other concerns do you have?
		public List<Movie> GetAll()
		{
			//bringing all movies at once without pagination or limit
			//in this case, missing AsNoTracking and making it much more expensive and slower
			return _movieRentalDb.Movies.ToList();
		}
	}
}
