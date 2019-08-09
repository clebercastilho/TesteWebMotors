using AutoMapper;
using WebMotors.Test.Domain.Entities;
using WebMotors.Teste.Api.ViewModels;

namespace WebMotors.Test.Api.Mapper
{
    public class ModelToViewModel: Profile
    {
        public ModelToViewModel()
        {
            CreateMap<Anuncio, AnuncioViewModel>();
        }
    }
}
