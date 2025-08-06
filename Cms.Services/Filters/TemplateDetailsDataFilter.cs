namespace Cms.Services.Filters
{
    public class TemplateDetailsDataFilter:CommonFilter
    {
        public int portalId {  get; set; }
        public string portalCode {  get; set; }
        public string portalName {  get; set; }
        public string TemplateName {  get; set; }
        public string Url {  get; set; }
        public string Keywords {  get; set; }
        public bool? Showonhomepage {  get; set; }
        public string TemplateType {  get; set; }
        public string TemplateCategory {  get; set; }
        public int TemplateId {  get; set; }
        public bool? IncludeSimilarItems {  get; set; }
        public int? SimilarItems { get; set; } = 5;
        public bool? Approved { get; set; }
    }
}
