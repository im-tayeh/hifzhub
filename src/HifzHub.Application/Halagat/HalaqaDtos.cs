namespace HifzHub.Application.Halaqat;

public record CreateHalaqaRequest(Guid StageId, string Name);
public record UpdateHalaqaRequest(Guid StageId, string Name);
public record HalaqaResponse(Guid Id, Guid StageId, string Name);
