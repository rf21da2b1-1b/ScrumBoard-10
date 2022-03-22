using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScrumBoard.MockData;
using ScrumBoardLib.model;

namespace ScrumBoard.Services
{
    /// <summary>
    /// A UserStoryService based on a static list in the system
    /// </summary>
    public class UserStoryService:IUserStoryService
    {
        private readonly List<UserStory> _userStories;
        private static int nextId = 0;


        /// <summary>
        /// Constructor to initialize this service with a static list 
        /// </summary>
        public UserStoryService()
        {
            _userStories = MockUserStories.GetMockUserStories();
            nextId = _userStories.Max(u => u.Id) + 1;
        }


        /// <inheritdoc cref="IUserStoryService.GetAll"/>
        public List<UserStory> GetAll()
        {
            return new List<UserStory>(_userStories);
        }

        /// <inheritdoc cref="IUserStoryService.GetById"/>
        public UserStory GetById(int id)
        {
            UserStory us = _userStories.Find(u => u.Id == id);
            if (us == null)
            {
                throw new KeyNotFoundException();
            }

            return us;
        }

        /// <inheritdoc cref="IUserStoryService.Create"/>
        public UserStory Create(UserStory newUserStory)
        {
            newUserStory.Id = nextId++;
            _userStories.Add(newUserStory);

            return newUserStory;
        }

        /// <inheritdoc cref="IUserStoryService.Delete"/>
        public UserStory Delete(int id)
        {
            UserStory us = GetById(id);

            _userStories.Remove(us);

            return us;
        }

        /// <inheritdoc cref="IUserStoryService.Modify"/>
        public UserStory Modify(UserStory modifiedUserStory)
        {
            int ix = _userStories.FindIndex(u => u.Id == modifiedUserStory.Id);
            if (ix == -1)
            {
                throw new KeyNotFoundException();
            }

            _userStories[ix] = modifiedUserStory;

            return modifiedUserStory;
        }
    }
}
