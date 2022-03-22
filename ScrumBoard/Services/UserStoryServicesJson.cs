using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ScrumBoard.MockData;
using ScrumBoard.util;
using ScrumBoardLib.model;

namespace ScrumBoard.Services
{
    /// <summary>
    /// A UserStoryService based on a json-file in the system at 'data\UserStory.json'
    /// </summary>
    public class UserStoryServicesJson:IUserStoryService
    {
        private const String FileName = @"data\UserStory.json";
        private List<UserStory> _dataBuffer = null;
        private static int nextId = 0;


        /// <summary>
        /// Constructor to initialize this service from the assigned json-file,
        /// if no file exists a new empty file is created
        /// </summary>
        public UserStoryServicesJson()
        {
            _dataBuffer = JsonFileReader.ReadJsonGeneric<UserStory>(FileName);

            // hack
            if (_dataBuffer.Count == 0)
            {
                foreach (UserStory u in MockUserStories.GetMockUserStories())
                {
                    _dataBuffer.Add(u);
                }

                SaveChanges();
            }

            nextId = _dataBuffer.Max(u => u.Id) + 1;
        }

        /// <inheritdoc cref="IUserStoryService.GetAll"/>
        public List<UserStory> GetAll()
        {
            return new List<UserStory>(_dataBuffer);
        }

        /// <inheritdoc cref="IUserStoryService.GetById"/>
        public UserStory GetById(int id)
        {
            UserStory us = _dataBuffer.Find(u => u.Id == id);
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
            _dataBuffer.Add(newUserStory);

            SaveChanges(); // remember to save changes to the json file
            return newUserStory;
        }

        /// <inheritdoc cref="IUserStoryService.Delete"/>
        public UserStory Delete(int id)
        {
            UserStory us = GetById(id);
            _dataBuffer.Remove(us);

            SaveChanges(); // remember to save changes to the json file
            return us;
        }

        /// <inheritdoc cref="IUserStoryService.Modify"/>
        public UserStory Modify(UserStory modifiedUserStory)
        {
            int ix = _dataBuffer.FindIndex(u => u.Id == modifiedUserStory.Id);
            if (ix == -1)
            {
                throw new KeyNotFoundException();
            }

            _dataBuffer[ix] = modifiedUserStory;

            SaveChanges(); // remember to save changes to the json file
            return modifiedUserStory;
        }

        private void SaveChanges()
        {
            JsonFileWriter.WriteToJsonGeneric(_dataBuffer, FileName);
        }
    }
}
