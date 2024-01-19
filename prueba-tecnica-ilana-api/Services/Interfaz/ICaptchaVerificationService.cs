namespace prueba_tecnica_ilana_api.Services.Interfaz
{
    public interface ICaptchaVerificationService
    {
        Task<bool> IsCaptchaValid(string captchaResponse);
    }
}
