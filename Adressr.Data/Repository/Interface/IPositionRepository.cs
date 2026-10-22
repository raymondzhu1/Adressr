using Adressr.Data.Model;

namespace Adressr.Data.Repository.Interface
{
    public interface IPositionRepository
    {
        /// <summary>
        /// Adds a position to a company.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        Task AddPositionToCompanyAsync(Position position);

        /// <summary>
        /// Gets a singular position for the supplied position ID.
        /// </summary>
        /// <param name="positionId">Position ID corresponding to some position in the database.</param>
        /// <returns>
        /// The Position data for a company's position retrieved from the database. If the ID didn't point to a valid row in the database, it will return null.
        /// </returns>
        Task<Position?> GetPositionByPositionIdAsync(int positionId);

        /// <summary>
        /// Gets the list of positions associated with this Company ID.
        /// </summary>
        /// <param name="companyId">Unique identifier of the company to get positions from.</param>
        /// <returns>
        /// Returns a list of positions within this company in alphabetical order.
        /// </returns>
        Task<List<Position>> GetPositionsByCompanyIdAsync(int companyId);

        /// <summary>
        /// Gets the number of employees for a specific position based off the position ID given as a parameter.
        /// </summary>
        /// <param name="positionId">The position ID to lookup in our database.</param>
        /// <returns>
        /// The number of employees for this position.
        /// </returns>
        Task<int> GetNumberOfEmployeesForPositionByPositionIdAsync(int positionId);

        /// <summary>
        /// Gets the list of employees that work under this position in this company that also have a profile on this website.
        /// </summary>
        /// <param name="positionId">Position ID to lookup in the database.</param>
        /// <returns>
        /// The list of employees for this position that have a registered account for the site.
        /// </returns>
        Task<List<User>> GetRegisterEmployeesByPositionIdAsync(int positionId);

        /// <summary>
        /// Gets the list of employees that work under this position in this company that are not registered with this website.
        /// </summary>
        /// <param name="positionId">The position ID to lookup in the database.</param>
        /// <returns>
        /// The list of employees for this position that are not a registered user of this website.
        /// </returns>
        Task<List<PlainTextEmployee>> GetPlainTextEmployeesByPositionIdAsync(int positionId);
    }
}
