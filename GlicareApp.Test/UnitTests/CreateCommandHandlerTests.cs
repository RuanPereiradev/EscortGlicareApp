using GlicareApp.Domain.Entities;
using GlicareApp.Domain.Interfaces.Repositories;
using GlicareApp.Services.Commands;
using GlicareApp.Services.CommandsHandlers;
using Xunit;
using Moq;
namespace GlicareApp.Test.UnitTests;

public class CreateCommandHandlerTests
{
    [Fact]
    public async Task Handle_Should_Create_Escort_When_Patient_Exists()
    {
        //Arrange
        var escortRepositoryMock = new Mock<IEscortRepository>();
        var patientRepositoryMock = new Mock<IPacientRepository>();
        
        //simula se o paciente existir
        patientRepositoryMock
            .Setup(x=> x.ExistsAsync("1"))
            .ReturnsAsync(true);
        
        //Simula a inserção do acompanhante
        escortRepositoryMock
            .Setup(x=> x.InsertEscortAsync(It.IsAny<Escort>()))
            .ReturnsAsync("100");
        
        
        //Cria o handler
        var handler = new CreateEscortCommandHandler(escortRepositoryMock.Object, patientRepositoryMock.Object);

        var request = new CreateEscortCommand
        {
            Name = "John Doe",
            Phone = "1234567890",
            Email = "YVYdF@example.com",
            Relationship = "Irmão",
            PacientId = "1"
        };
        
        var result = await handler.Handle(request, CancellationToken.None);
        
        //Assert
        Assert.NotNull(result);
        Assert.Equal("1", result.PacientId);
        Assert.Equal("John Doe", result.Name);
       
        //verifica se chamou os metodos corretamente
        patientRepositoryMock.Verify(x => x.ExistsAsync("1"), Times.Once);
        escortRepositoryMock.Verify(x => x.InsertEscortAsync(It.IsAny<Escort>()), Times.Once);
    }
}
