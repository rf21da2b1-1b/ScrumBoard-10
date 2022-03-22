using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScrumBoardLib.model;

namespace ScrumBoard.Services
{
    /// <summary>
    /// This Interface specifies CRUD functions for an UserStory
    /// </summary>
    public interface IUserStoryService
    {
        /// <summary>
        /// Gets all the UserStories in a list
        /// </summary>
        /// <returns>The list of UserStories, if no UserStories exists an empty list is returned </returns>
        List<UserStory> GetAll();


        /// <summary>
        /// Get a UserStory specified by an id
        /// </summary>
        /// <param name="id">The specified id of the requestedUserStory</param>
        /// <returns>The userStory of the specified Id</returns>
        /// <exception cref="KeyNotFoundException">If no userStory exists with the specified id</exception>
        UserStory GetById(int id);


        /// <summary>
        /// Create and add a new UserStory to the system
        /// </summary>
        /// <param name="newUserStory">The new UserStory to be added</param>
        /// <returns>The UserStory object which is stored in the system</returns>
        UserStory Create(UserStory newUserStory);


        /// <summary>
        /// Delete an UserStory from the system
        /// </summary>
        /// <param name="id">The specified id of the UserStory to be deleted</param>
        /// <returns>The UserStory object which is deleted from the system</returns>
        /// <exception cref="KeyNotFoundException">If no userStory exists with the specified id</exception>
        UserStory Delete(int id);

        /// <summary>
        /// Find and update a specified UserStory with new values
        /// </summary>
        /// <param name="modifiedUserStory">The new values of an UserStory</param>
        /// <returns>The UserStory object which have been modified in the system</returns>
        /// <exception cref="KeyNotFoundException">If no userStory exists with the id in the modified UserStory</exception>
        UserStory Modify(UserStory modifiedUserStory);
    }
}
