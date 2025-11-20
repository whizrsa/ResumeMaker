namespace ResumeMaker.Services
{
    public interface IEmailValidator
    {
        Task<bool> IsDeliverableAsync(string email, CancellationToken ct = default);
    }
}
