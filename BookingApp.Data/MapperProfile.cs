using AutoMapper;
using BookingApp.Core.Entitis;
using BookingApp.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Data
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<GymClass, GymClassesViewModel>();
        }
    }
}
