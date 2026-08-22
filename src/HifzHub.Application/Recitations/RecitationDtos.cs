using HifzHub.Domain.Enums;

namespace HifzHub.Application.Recitations;

public record CreateRecitationRequest(
    Guid StudentId,
    DateOnly Date,
    int FromSurah, int FromAyah,
    int ToSurah, int ToAyah,
    RecitationType Type,
    RecitationGrade Grade,
    string? Note);

public record RecitationResponse(
    Guid Id, Guid StudentId, DateOnly Date,
    int FromSurah, int FromAyah, int ToSurah, int ToAyah,
    RecitationType Type, RecitationGrade Grade, string? Note);