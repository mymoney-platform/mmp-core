using MediatR;
using MMP.Core.Application.Interfaces;

namespace MMP.Core.Application.Commands;

public class DeleteOperationCommandHandler : IRequestHandler<DeleteOperationCommand>
{
    private readonly IOperationRepository _operationRepository;

    public DeleteOperationCommandHandler(IOperationRepository operationRepository)
    {
        _operationRepository = operationRepository;
    }

    public async Task<Unit> Handle(DeleteOperationCommand request, CancellationToken cancellationToken)
    {
        await _operationRepository.Delete(request.OperationId);
        
        return Unit.Value;
    }
}