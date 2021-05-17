using AutoMapper;
using MobileService.Entities.DataTransferModels.Collection;
using MobileService.Entities.DataTransferModels.Flashcard;
using MobileService.Entities.Models;

namespace MobileService.Entities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CollectionModel, CollectionGetModel>();
            CreateMap<CollectionInsertModel, CollectionModel>();
            CreateMap<CollectionUpdateModel, CollectionModel>();
            CreateMap<FlashcardModel, FlashcardGetModel>();
        }
    }
}
