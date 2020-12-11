using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PumoxRecruitmentTask.API.Controllers;
using PumoxRecruitmentTask.BLL.Interfaces.Services;
using Moq;
using PumoxRecruitmentTask.BLL.Dtos;
using PumoxRecruitmentTask.BLL.Dtos.Responses;
using Xunit;

namespace PumoxRecruitmentTask.API.Tests
{
    public class CompanyControllerTests
    {
        private readonly CompanyController _companyController;
        private readonly Mock<ICompanyService> _mock = new Mock<ICompanyService>();

        public CompanyControllerTests()
        {
            var dtoMock = new CompanyDto { Id = 1 };
            var createdDtoMock = new CreatedCompanyResponseDto() { Id = 1 };

            IEnumerable<CompanyDto> dtoMocks = new List<CompanyDto>{ dtoMock };

            _mock.Setup(service =>
                    service.InsertAsync(null))
                    .Returns(Task.FromResult(createdDtoMock));

            _companyController = new CompanyController(_mock.Object);
        }
        
        [Fact]
        public async Task Return_201_If_All_Is_Correct_Insert()
        {
            var actionResult = await _companyController.InsertAsync(null);
            
            Assert.True(actionResult is CreatedResult result && result.StatusCode == 201);
        }
    }
}