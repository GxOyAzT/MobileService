using MobileService.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileService.Tests.MockData
{
    public class MockDataV1
    {
        public List<CollectionModel> Collections { get; set; }

        public MockDataV1()
        {
            Collections = new List<CollectionModel>();

            Collections.Add(new CollectionModel()
            { 
                Id = Guid.Parse("d30c8f79-291b-4532-8f22-b693e61d6bb5"),
                Name = "User 1 Name 1",
                UserId = "9a4e1d79-d64e-4ec4-85e5-53bdef5043f4"
            });

            Collections.Add(new CollectionModel()
            {
                Id = Guid.Parse("82c3a0d1-a73c-41e2-a8f3-ef525e5f0ffa"),
                Name = "User 2 Name 2",
                UserId = "fcabcb46-12dc-4013-bc92-6f00aae903b4"
            });

            Collections.Add(new CollectionModel()
            {
                Id = Guid.Parse("254967ae-ab25-4160-90dd-46b780fc1413"),
                Name = "User 3 Name 6",
                UserId = "a071553b-70e4-4998-aac2-37883d2d83ab"
            });

            Collections.Add(new CollectionModel()
            {
                Id = Guid.Parse("08a8529b-96c9-4e29-9d67-4d01e78ebf56"),
                Name = "User 2 Name 3",
                UserId = "fcabcb46-12dc-4013-bc92-6f00aae903b4"
            });

            Collections.Add(new CollectionModel()
            {
                Id = Guid.Parse("eca6796d-ab58-453c-904b-24469ba13b76"),
                Name = "User 2 Name 4",
                UserId = "fcabcb46-12dc-4013-bc92-6f00aae903b4"
            });

            Collections.Add(new CollectionModel()
            {
                Id = Guid.Parse("bbcdadd7-2cd2-43f5-ab23-3d6260e75da6"),
                Name = "User 1 Name 5",
                UserId = "9a4e1d79-d64e-4ec4-85e5-53bdef5043f4"
            });
        }

        public void Reset()
        {
            using (var db = MockDatabaseFactory.Build())
            {
                db.RemoveRange(db.Collections);
                db.SaveChanges();
                db.AddRange(Collections);
                db.SaveChanges();
            }
        }
    }
}
