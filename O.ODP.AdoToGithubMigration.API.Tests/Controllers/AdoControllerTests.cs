using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using O.ODP.AdoToGithubMigration.API.Abstractions;
using O.ODP.AdoToGithubMigration.API.Constants;
using O.ODP.AdoToGithubMigration.API.Controllers;
using O.ODP.AdoToGithubMigration.API.Models.ResponseDTO;

namespace O.ODP.AdoToGithubMigration.API.Tests.Controllers;

public class AdoControllerTests
{
    private AdoController _sut;

    private readonly Mock<IResourceFactory> _resourceFactoryMock;
    private readonly Mock<IResource> _resourceMock;

    public AdoControllerTests()
    {
        _resourceFactoryMock = new Mock<IResourceFactory>();
        _resourceMock = new Mock<IResource>();
        _sut = new AdoController(_resourceFactoryMock.Object);
    }

    [Fact]
    public async Task GetResources_ShouldReturnExpectedResult_WhenInvoked()
    {
        // Arrange
        var expectedResult = new List<ResourceResponse>
        {
            new ResourceResponse
            {
                Id = "1",
                Name = "Board1",
                Source = ResourceSourceConstants.Ado
            },
            new ResourceResponse
            {
                Id = "2",
                Name = "Board2",
                Source = ResourceSourceConstants.Ado
            }
        };
        _resourceMock.Setup(x => x.GetResources(It.IsAny<string>())).Returns(expectedResult);
        _resourceFactoryMock.Setup(x => x.GetResource(It.IsAny<string>())).Returns(_resourceMock.Object);

        // Act
        var result = ((await _sut.GetResources("1", ResourceTypeConstants.Board)).Result as OkObjectResult)?.Value;

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedResult);
    }
}
