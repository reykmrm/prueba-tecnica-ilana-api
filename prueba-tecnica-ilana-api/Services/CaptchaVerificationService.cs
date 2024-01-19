using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using prueba_tecnica_ilana_api.Services.Interfaz;

public class CaptchaVerificationService:ICaptchaVerificationService
{
    private readonly string _secretKey;

    public CaptchaVerificationService(string secretKey= "6LeTF04pAAAAAKFgannY06T2kF3gV3PDUUMwFwwc")
    {
        _secretKey = secretKey;
    }

    public async Task<bool> IsCaptchaValid(string captchaResponse)
    {
        var client = new HttpClient();
        var response = await client.PostAsync($"https://www.google.com/recaptcha/api/siteverify?secret={_secretKey}&response={captchaResponse}", null);
        var jsonString = await response.Content.ReadAsStringAsync();
        dynamic jsonData = JsonConvert.DeserializeObject(jsonString);
        return jsonData.success;
    }
}
