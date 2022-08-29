using AutoMapper;
using BookingApp.Core.Entitis;
using BookingApp.Core.Repositories;
using BookingApp.Core.ViewModels;
using BookingApp.Data;
using BookingApp.Data.Repositories;
using BookingApp.Tests.Helpers;
using BookingApp.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BookingApp.Tests.Controllers
{
    public class GymClassesControllerTests
    {
        private GymClassesController controller;
        private Mock<IGymClassRepository> mockRepo;

        public GymClassesControllerTests()
        {
            mockRepo = new Mock<IGymClassRepository>();
            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.Setup(u => u.GymClassRepository).Returns(mockRepo.Object);

            var mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.ConstructServicesUsing(c => new AttendingResolver(Mock.Of<HttpContextAccessor>()));
                cfg.AddProfile<MapperProfile>();
            }));

            var mockUserStore = new Mock<IUserStore<ApplicationUser>>();
            var userManager = new UserManager<ApplicationUser>(mockUserStore.Object, null, null, null, null, null, null, null, null);

            controller = new GymClassesController(mapper, userManager, mockUoW.Object);
        }


        [Fact]
        public async Task Index_NotAuthenticated_ShouldReturnsIndexViewModel()
        {
            //Arrange
            controller.SetUserIsAuthenticated(false);

            //Act
            var actual = await controller.Index(new IndexViewModel()) as ViewResult;

            //Assert
            Assert.IsType<IndexViewModel>(actual?.Model);
        } 
        
        
        [Fact]
        public async Task Index_AuthenticatedShowHistoryFalse_ShouldReturnsIndexViewModelAndExcpectedClasses()
        {
            //Arrange
            controller.SetUserIsAuthenticated(true);
            var gymClasses = GetGymClasses();
            var expected = gymClasses.Count();
            mockRepo.Setup(r => r.GetWithAttendinAsync()).ReturnsAsync(gymClasses);

            var vm = new IndexViewModel() { ShowHistory = false };

            //Act
            var actual = (await controller.Index(vm) as ViewResult)?.Model as IndexViewModel;

            //Assert
            Assert.IsType<IndexViewModel>(actual);
            Assert.Equal(expected, actual?.GymClasses.Count());

        }



        private IEnumerable<GymClass> GetGymClasses()
        {
            return new List<GymClass>
            {
                new GymClass
                {
                    Id = 1,
                    Name = "Spinning",
                    Description = "Easy",
                    StartDate = DateTime.Now.AddDays(3),
                    Duration = new TimeSpan(0,60,0)

                },
                new GymClass
                {
                    Id = 2,
                    Name = "Body Pump",
                    Description = "Hard",
                    StartDate = DateTime.Now.AddDays(23),
                    Duration = new TimeSpan(0,60,0)
                },
                new GymClass
                {
                    Id = 3,
                    Name = "Core",
                    Description = "Hard",
                    StartDate = DateTime.Now.AddDays(-2),
                    Duration = new TimeSpan(0,60,0)
                }
            };
        }
    }
}