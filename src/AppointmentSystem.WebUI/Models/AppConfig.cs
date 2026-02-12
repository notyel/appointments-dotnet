namespace AppointmentSystem.WebUI.Models;

public class AppConfig
{
    public string BusinessName { get; set; } = string.Empty;
    public ContactInfo Contact { get; set; } = new();
}

public class ContactInfo
{
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
}
