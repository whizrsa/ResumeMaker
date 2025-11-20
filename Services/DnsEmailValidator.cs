using System.Net.Mail;
using System.Net.Sockets;

namespace ResumeMaker.Services
{
    public class DnsEmailValidator : IEmailValidator
    {
        public async Task<bool> IsDeliverableAsync(string email, CancellationToken ct = default)
        {
            try
            {
                var address = new MailAddress(email);
                var host = address.Host;
                // Very lightweight check: DNS resolve host
                var entry = await System.Net.Dns.GetHostEntryAsync(host);
                return entry.AddressList.Any(a => a.AddressFamily == AddressFamily.InterNetwork || a.AddressFamily == AddressFamily.InterNetworkV6);
            }
            catch
            {
                return false;
            }
        }
    }
}
