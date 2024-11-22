using Microsoft.AspNetCore.Mvc;
using GridHub_ML; 

namespace MyEnergyPredictionApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValuePredictionController : ControllerBase
    {
        // Método POST para prever os valores com base nas entradas fornecidas
        [HttpPost("predict")]
        public ActionResult<ValuePrediction.ModelOutput> Predict([FromBody] ValuePrediction.ModelInput input)
        {
            if (input == null)
            {
                return BadRequest("Invalid input data.");
            }

            // Verifica se todos os campos necessários estão presentes
            if (input.Temperature < 0 || input.HourOfDay < 0 || input.CloudCoverage < 0 || input.WindSpeed < 0 || input.EnergyGenerated < 0)
            {
                return BadRequest("Input data contains invalid values.");
            }

            // Realiza a previsão usando o modelo ML.NET
            var result = ValuePrediction.Predict(input);

            // Retorna a previsão com o modelo de saída
            return Ok(result);
        }
    }
}
