namespace MP.Blazor.Library.Models
{
    public class NavigatablePage : NavigatableSubPage
    {
        public IEnumerable<NavigatableSubPage>? SubPages { get; set; }
    }
}
