using Microsoft.AspNetCore.Components;

namespace ELM.Web.Pages.TestPage
{
    public class FormControles
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; } = DateTime.Now.Date;
        public string Number { get; set; }
        public string Color { get; set; }
        public string Gender { get; set; }
        public List<string> SelectedValues { get; set; } = new List<string>();
        public void HandleCheckboxValueChanged(ChangeEventArgs e, string value)
        {
            if ((bool)e.Value)
                SelectedValues.Add(value);
            else
                SelectedValues.Remove(value);
        }
    }
}
