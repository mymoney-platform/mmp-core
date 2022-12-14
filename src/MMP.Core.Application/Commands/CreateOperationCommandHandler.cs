using MediatR;
using MMP.Core.Application.Extensions;
using MMP.Core.Application.Interfaces;
using MMP.Core.Domain.Enums;
using MMP.Core.Domain.Models;

namespace MMP.Core.Application.Commands;

public class CreateOperationCommandHandler : IRequestHandler<CreateOperationCommand, Guid>
{
    private readonly IOperationCommandRepository _operationRepository;

    public CreateOperationCommandHandler(IOperationCommandRepository operationRepository)
    {
        _operationRepository = operationRepository;
    }

    public async Task<Guid> Handle(CreateOperationCommand request, CancellationToken cancellationToken)
    {
        var op = request.ToDomain();

        await _operationRepository.Save(op);
        await _operationRepository.Commit();
        
        return op.OperationId;
    }
}