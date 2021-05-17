using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MobileService.Entities.Models
{
    public class FlashcardModel : BaseModel
    {
        [MaxLength(250)]
        public string Native { get; set; }
        [MaxLength(250)]
        public string Foreign { get; set; }

        public Guid CollectionModelId { get; set; }
        public CollectionModel CollectionModel { get; set; }

        public List<FlashcardProgressModel> FlashcardProgressModels { get; set; }
    }
}
