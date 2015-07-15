namespace Telemedicine.Core
{
    /// <summary>
    /// Represents a user.
    /// </summary>
    public interface IUser
    {
        /// <summary>
        /// Unique identifier of a user.
        /// </summary>
        string Id { get; set; }
    }
}