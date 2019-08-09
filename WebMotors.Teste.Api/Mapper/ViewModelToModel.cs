using WebMotors.Test.Domain.Entities;
using AutoMapper;
using WebMotors.Teste.Api.ViewModels;

namespace WebMotors.Test.Api.Mapper
{
    public class ViewModelToModel : Profile
    {
        public ViewModelToModel()
        {
            CreateMap<AnuncioViewModel, Anuncio>();
        }
    }
}
