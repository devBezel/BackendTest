using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using PumoxRecruitmentTask.BLL.Dtos;
using PumoxRecruitmentTask.BLL.Dtos.Responses;
using PumoxRecruitmentTask.BLL.Interfaces.Services;
using PumoxRecruitmentTask.BLL.Services;
using PumoxRecruitmentTask.DAL.DataAccess.Models;
using PumoxRecruitmentTask.DAL.Enums;
using PumoxRecruitmentTask.DAL.UnitOfWork;
using Xunit;

namespace PumoxRecruitmentTask.BLL.Tests
{
    public class CompanyServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();
        private readonly ICompanyService _companyService;

        public CompanyServiceTests()
        {
            IEnumerable<CompanyModel> mockCompanies = new List<CompanyModel>()
            {
                new CompanyModel
                {
                    Id = 1, 
                    Name = "Reddit",
                    EstablishmentYear = 2005,
                    Employees = new List<EmployeeModel>()
                    {
                        new EmployeeModel
                        {
                            Id = 1,
                            CompanyId = 1,
                            FirstName = "Josh",
                            LastName = "Kenny",
                            DateOfBirth = new DateTime(1999, 08, 22),
                            JobTitle = JobTitle.Administrator
                        },
                        new EmployeeModel
                        {
                            Id = 2,
                            CompanyId = 1,
                            FirstName = "Josh",
                            LastName = "McKenny",
                            DateOfBirth = new DateTime(1978, 08, 22),
                            JobTitle = JobTitle.Architect
                        },
                        new EmployeeModel
                        {
                            Id = 3,
                            CompanyId = 1,
                            FirstName = "Richard",
                            LastName = "McCartney",
                            DateOfBirth = new DateTime(1959, 08, 22),
                            JobTitle = JobTitle.Administrator
                        }
                    },
                },
                new CompanyModel()
                {
                    Id = 2,
                    Name = "Modecom",
                    EstablishmentYear = 1999,
                    Employees = new List<EmployeeModel>
                    {
                        new EmployeeModel()
                        {
                            Id = 4,
                            CompanyId = 2,
                            FirstName = "Christopher",
                            LastName = "Hoppe",
                            DateOfBirth = new DateTime(1972, 06, 30),
                            JobTitle = JobTitle.Developer
                        },
                        new EmployeeModel()
                        {
                            Id = 5,
                            CompanyId = 2,
                            FirstName = "Lisa",
                            LastName = "Evans",
                            DateOfBirth = new DateTime(1981, 07, 05),
                            JobTitle = JobTitle.Administrator
                        },
                        new EmployeeModel()
                        {
                            Id = 6,
                            CompanyId = 2,
                            FirstName = "Bruce",
                            LastName = "Fasano",
                            DateOfBirth = new DateTime(1960, 04, 19),
                            JobTitle = JobTitle.Manager
                        }
                    }
                },
                new CompanyModel()
                {
                    Name = "Slack",
                    EstablishmentYear = 2013,
                    Employees = new List<EmployeeModel>
                    {
                        new EmployeeModel
                        {
                            FirstName = "Kimberley",
                            LastName = "Jones",
                            DateOfBirth = new DateTime(1962, 09, 14),
                            JobTitle = JobTitle.Manager
                        }
                    }
                }
            };
            
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CompanyModel, CompanyDto>();
                cfg.CreateMap<CompanyDto, CompanyModel>();

                cfg.CreateMap<EmployeeModel, EmployeeDto>();
                cfg.CreateMap<EmployeeDto, EmployeeModel>();

                cfg.CreateMap<CompanyDto, CreatedCompanyResponseDto>();
                cfg.CreateMap<EmployeeDto, CreatedCompanyResponseDto>();
            });

            var mapper = mapperConfig.CreateMapper();
            _unitOfWorkMock.Setup(unitOfWork =>
                    unitOfWork.CompanyRepository.GetAllAsync(null, It.IsAny<Func<IQueryable<CompanyModel>, IQueryable<CompanyModel>>>()))
                .Returns(Task.FromResult(mockCompanies));
            _companyService = new CompanyService(_unitOfWorkMock.Object, mapper);
            
        }
        
        [Fact]
        public async Task Check_Search_Companies_JobTitle()
        {
            var searchDto = new SearchCompanyDto
            {
                EmployeeJobTitles = new List<string> { "Administrator" }
            };

            var result = (await _companyService.SearchAsync(searchDto)).ToList();
            
            Assert.True(result.Count == 2);
        }

        [Fact]
        public async Task Check_Search_Companies_JobTitles()
        {
            var searchDto = new SearchCompanyDto
            {
                EmployeeJobTitles = new List<string> { "Manager", "Administrator" }
            };

            var result = (await _companyService.SearchAsync(searchDto)).ToList();
            Assert.True(result.Count == 3);
        }

        [Fact]
        public async Task Check_Search_Companies_Keywords()
        {
            var searchDto = new SearchCompanyDto
            {
                Keyword = "Slack"
            };
            
            var result = (await _companyService.SearchAsync(searchDto)).ToList();
            Assert.True(result.Count == 1 && result.Any(x => x.Name.Contains(searchDto.Keyword)));
        }
        
        [Fact]
        public async Task Check_Search_Companies_EmployeeDateOfBirthFrom()
        {
            var searchDto = new SearchCompanyDto
            {
                EmployeeDateOfBirthFrom = new DateTime(1972, 06, 30)
            };
            var result = (await _companyService.SearchAsync(searchDto)).ToList();
            Assert.True(result.Count == 2);
        }
        
        [Fact]
        public async Task Check_Search_Companies_EmployeeDateOfBirthTo()
        {
            var searchDto = new SearchCompanyDto
            {
                EmployeeDateOfBirthTo = new DateTime(1959, 11, 30)
            };
            var result = (await _companyService.SearchAsync(searchDto)).ToList();
            Assert.True(result.Count == 1);
        }
        
        [Fact]
        public async Task Check_Search_Companies_EmployeeDateOfBirth()
        {
            var searchDto = new SearchCompanyDto
            {
                EmployeeDateOfBirthFrom = new DateTime(1959, 11, 30),
                EmployeeDateOfBirthTo = new DateTime(1962, 09, 14),
            };
            
            var result = (await _companyService.SearchAsync(searchDto)).ToList();
            Assert.True(result.Count == 3);
        }
        
    }
}