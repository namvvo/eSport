namespace eSport.UI.Shared.Models.Catalog
{
    public record SeasonStageModel
    {
        public int SeasonStageId { get; set; }
        public string Year { get; set; } = string.Empty;
        public int? Year2 { get; set; }
        public int StageId { get; set; }
        public int SeasonId { get; set; }
        public string Stage { get; set; } = string.Empty;
        public int TeamId { get; set; }

    }
    public record SeasonModel(int Id,
                         string Year,
                         int? DisplayOrder,
                         int? Year2,
                         bool Status);
    public record StageModel(
            int Id,
            string Name,
            string C1WhoscoredName,
            string C3WhoscoredName,
            string EuroWhoscoredName,
            string WCWhoscoredName,
            string C1SofascoreName,
            string C3SofascoreName,
            string EuroSofascoreName,
            string WCSofascoreName,
            int? ParentId,
            bool? GroupStage,
            int DisplayOrder,
            int? NoOfMatches,
            bool Display,
            int Round);
}
