using MMP.Core.Domain.Models;

namespace MMP.Core.Domain.Extensions;

public static class BaseExtension
{
    public static void SetInternalId(this Operation model, long internalId) => model.SetInternalId(internalId);
}