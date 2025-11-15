namespace Meteo.ViewModels
{
    public class PaginationViewModel<TD, TF> where TF : new() 
{
    public TF Filter { get; set; } = new TF();
    public IEnumerable<TD> Data { get; set; }
    public Pagination Pagination { get; set; } = new Pagination();
}
}
