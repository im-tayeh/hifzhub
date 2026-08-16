namespace HifzHub.Application.Stages;

public record CreateStageRequest(string Name, int Order);
public record UpdateStageRequest(string Name, int Order);
public record StageResponse(Guid Id, string Name, int Order);
