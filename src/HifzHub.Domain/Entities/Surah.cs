namespace HifzHub.Domain.Entities;

public class Surah
{
    public int Number { get; set; }
    public string NameAr { get; set; } = string.Empty;
    public string NameEn { get; set; } = string.Empty;
    public int AyahCount { get; set; }
}
