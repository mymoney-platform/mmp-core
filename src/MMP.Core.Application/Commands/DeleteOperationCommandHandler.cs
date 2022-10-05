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
        var op = await _operationRepository.Get(request.OperationId);

        if (op != null)
        {
            var copy = op.ReverseOperation();
            await _operationRepository.Save(copy);
            await _operationRepository.Commit();
        }
        return Unit.Value;
    }
}