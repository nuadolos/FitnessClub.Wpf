namespace FitnessClub.BLL.Domain;

#nullable disable
public class RequestModel
{
    public string Title { get; set; }
    public string Porpose { get; set; }
    public string RequestStatusCode{ get; set; }
    public string RequestStatusDescription { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public int IndividualPlanCount { get; set; }
    public string ClientFullName { get; set; }
    public string ManagerFullName { get; set; }
}
