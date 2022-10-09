using MediatR;
using MMP.Core.Application.Extensions;
using MMP.Core.Application.Interfaces;

namespace MMP.Core.Application.Commands;

public class UpdateOperationCommandHandler : IRequestHandler<UpdateOperationCommand>
{
    private readonly IOperationCommandRepository _operationRepository;

    public UpdateOperationCommandHandler(IOperationCommandRepository operationRepository)
    {
        _operationRepository = operationRepository;
    }

    public async Task<Unit> Handle(UpdateOperationCommand request, CancellationToken cancellationToken)
    {
        var op = await _operationRepository.Get(request.OperationId);

        if (op == null)
            throw new Exception("Resource not found"); //TODO convert to a Custom Application Exception

        op.ChangeDescription(request.Description);
        op.ChangeOperationCategory(request.OperationCategory.ToDomain());
        op.ChangeOperationType(request.OperationType.ToDomain());
        op.ChangeOperationValue(request.Value);

        await _operationRepository.Update(op);
        await _operationRepository.Commit();
        
        return Unit.Value;
    }
}