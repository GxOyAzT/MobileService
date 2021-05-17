using System;
using System.Collections.Generic;

namespace MobileService.Entities.Models
{
    public class CollectionModel : BaseModel
    {
        public string UserId { get; set; }
        public string Name { get; set; }

        public List<FlashcardModel> FlashcardModels { get; set; }
    }
}
