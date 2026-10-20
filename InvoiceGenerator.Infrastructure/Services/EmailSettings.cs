namespace InvoiceGenerator.Infrastructure.Services
{
    // Bound from the "EmailSettings" configuration section. SMTP credentials come from user secrets, not appsettings
    public class EmailSettings
    {
        public const string SectionName = "EmailSettings";

        public string SenderEmail { get; set; } = string.Empty;
        public string SenderName { get; set; } = string.Empty;

        public string? SmtpServer { get; set; }
        public int Port { get; set; } = 587;
        public string? Username { get; set; }
        public string? Password { get; set; }

        // Some Windows machines can't complete the certificate revocation check, which fails the TLS handshake.
        // Turning it off still validates the certificate chain and host name
        public bool CheckCertificateRevocation { get; set; } = true;

        // When set, emails are written as .eml files to this folder instead of being sent (for local testing)
        public string? PickupDirectory { get; set; }
    }
}
