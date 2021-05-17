using MobileService.Entities.Enums;
using MobileService.Entities.Models;
using System;
using System.Collections.Generic;

namespace MobileService.Tests.MockData
{
    public class MockDataV6
    {
        public List<CollectionModel> Collections { get; set; }
        public List<FlashcardModel> Flashcards { get; set; }
        public List<StatsUserModel> StatsUser { get; set; }

        public MockDataV6()
        {
            #region Collections
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
            #endregion

            #region Flashcards
            Flashcards = new List<FlashcardModel>();

            Flashcards.Add(new FlashcardModel()
            {
                Id = Guid.Parse("6aa83ba0-1396-428f-adb7-d7ab972459eb"),
                CollectionModelId = Guid.Parse("d30c8f79-291b-4532-8f22-b693e61d6bb5"),
                Foreign = "Foreign 1",
                Native = "Native 1",
                FlashcardProgressModels = new List<FlashcardProgressModel>()
                {
                    new FlashcardProgressModel()
                    {
                        Id = Guid.Parse("a6d821a0-75fd-4152-af8f-03fa17796430"),
                        PracticeDirection = PracticeDirection.ForeignToNative,
                        CorrectInRow = 2,
                        PracticeDate = DateTime.Now.Date.AddDays(5)
                    },
                    new FlashcardProgressModel()
                    {
                        Id = Guid.Parse("9884d783-427d-45d4-a1df-facaf81729f5"),
                        PracticeDirection = PracticeDirection.NativeToForeign,
                        CorrectInRow = 0,
                        PracticeDate = DateTime.Now.Date
                    }
                }
            });

            Flashcards.Add(new FlashcardModel()
            {
                Id = Guid.Parse("30364c9b-e00e-4811-8921-69ab3db427cd"),
                CollectionModelId = Guid.Parse("d30c8f79-291b-4532-8f22-b693e61d6bb5"),
                Foreign = "Foreign 2",
                Native = "Native 2",
                FlashcardProgressModels = new List<FlashcardProgressModel>()
                {
                    new FlashcardProgressModel()
                    {
                        Id = Guid.Parse("594b1485-e842-482f-9b09-a649cb72bdb1"),
                        PracticeDirection = PracticeDirection.ForeignToNative,
                        CorrectInRow = 2,
                        PracticeDate = DateTime.Now.Date.AddDays(15)
                    },
                    new FlashcardProgressModel()
                    {
                        Id = Guid.Parse("91b5ae74-6197-449f-a4ef-c81068179822"),
                        PracticeDirection = PracticeDirection.NativeToForeign,
                        CorrectInRow = 1,
                        PracticeDate = DateTime.Now.Date.AddDays(7)
                    }
                }
            });

            Flashcards.Add(new FlashcardModel()
            {
                Id = Guid.Parse("d9a1f8f7-af5b-479e-94ee-4f64e17fadfa"),
                CollectionModelId = Guid.Parse("bbcdadd7-2cd2-43f5-ab23-3d6260e75da6"),
                Foreign = "Foreign 3",
                Native = "Native 3",
                FlashcardProgressModels = new List<FlashcardProgressModel>()
                {
                    new FlashcardProgressModel()
                    {
                        Id = Guid.Parse("021fc2e4-e2cf-4120-a1af-df918ecad194"),
                        PracticeDirection = PracticeDirection.ForeignToNative,
                        CorrectInRow = 2,
                        PracticeDate = DateTime.Now.Date.AddDays(-5)
                    },
                    new FlashcardProgressModel()
                    {
                        Id = Guid.Parse("2c083f4e-fdce-4c67-8ca7-e3c5d1b40d4e"),
                        PracticeDirection = PracticeDirection.NativeToForeign,
                        CorrectInRow = 0,
                        PracticeDate = DateTime.MinValue
                    }
                }
            });

            Flashcards.Add(new FlashcardModel()
            {
                Id = Guid.Parse("691de3f1-8117-465f-b8d9-7cfcefc372fe"),
                CollectionModelId = Guid.Parse("82c3a0d1-a73c-41e2-a8f3-ef525e5f0ffa"),
                Foreign = "Foreign 4",
                Native = "Native 4",
                FlashcardProgressModels = new List<FlashcardProgressModel>()
                {
                    new FlashcardProgressModel()
                    {
                        Id = Guid.Parse("94313a67-4a4a-4cd0-a9bc-373863dbf4aa"),
                        PracticeDirection = PracticeDirection.ForeignToNative,
                        CorrectInRow = 2,
                        PracticeDate = DateTime.Now.Date.AddDays(3)
                    },
                    new FlashcardProgressModel()
                    {
                        Id = Guid.Parse("c23fd32f-5cd1-4c78-a3b2-06b9c87439a7"),
                        PracticeDirection = PracticeDirection.NativeToForeign,
                        CorrectInRow = 7,
                        PracticeDate = DateTime.Now.AddDays(-10)
                    }
                }
            });

            Flashcards.Add(new FlashcardModel()
            {
                Id = Guid.Parse("fcf78bdc-03c7-4333-b50c-90d714750189"),
                CollectionModelId = Guid.Parse("d30c8f79-291b-4532-8f22-b693e61d6bb5"),
                Foreign = "Foreign 5",
                Native = "Native 5",
                FlashcardProgressModels = new List<FlashcardProgressModel>()
                {
                    new FlashcardProgressModel()
                    {
                        Id = Guid.Parse("9dccbe2b-fd54-42e7-8f35-c0fba62855f7"),
                        PracticeDirection = PracticeDirection.ForeignToNative,
                        CorrectInRow = 7,
                        PracticeDate = DateTime.Now.Date.AddDays(57)
                    },
                    new FlashcardProgressModel()
                    {
                        Id = Guid.Parse("9670047b-d9ad-4746-89b7-97aae2d9f04d"),
                        PracticeDirection = PracticeDirection.NativeToForeign,
                        CorrectInRow = 11,
                        PracticeDate = DateTime.Now.Date
                    }
                }
            });
            #endregion

            #region StatsUser
            StatsUser = new List<StatsUserModel>();

            StatsUser.Add(new StatsUserModel()
            {
                Id = Guid.Parse("d28e4728-1082-477d-b9c8-be81aa165efb"),
                FlashcardsTurnOverCount = 3,
                Day = DateTime.Now.Date,
                UserId = "9a4e1d79-d64e-4ec4-85e5-53bdef5043f4"
            });

            StatsUser.Add(new StatsUserModel()
            {
                Id = Guid.Parse("1d43f5dd-ef30-45e1-a99d-d3183807b953"),
                FlashcardsTurnOverCount = 2,
                Day = DateTime.Now.AddDays(-1).Date,
                UserId = "9a4e1d79-d64e-4ec4-85e5-53bdef5043f4"
            });

            StatsUser.Add(new StatsUserModel()
            {
                Id = Guid.Parse("65d567e4-c770-4bf9-aa0f-2a4c5691bbe8"),
                FlashcardsTurnOverCount = 8,
                Day = DateTime.Now.AddDays(-1).Date,
                UserId = "fcabcb46-12dc-4013-bc92-6f00aae903b4"
            });
            #endregion
        }

        public void Reset()
        {
            using (var db = MockDatabaseFactory.Build())
            {
                db.RemoveRange(db.Flashcards);
                db.SaveChanges();
                db.RemoveRange(db.Collections);
                db.SaveChanges();
                db.RemoveRange(db.StatsUserModels);
                db.SaveChanges();

                db.AddRange(Collections);
                db.SaveChanges();
                db.AddRange(Flashcards);
                db.SaveChanges();
                db.AddRange(StatsUser);
                db.SaveChanges();
            }
        }
    }
}
