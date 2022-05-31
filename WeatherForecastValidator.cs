using FluentValidation;
using Microsoft.Extensions.Localization;

namespace LocalizationAPI
{
    public class WeatherForecastValidator : AbstractValidator<TestWeatherForcast>
    {

        public WeatherForecastValidator(IStringLocalizer<TestWeatherForcast> localizer)
        {
            RuleFor(x => x.Name).NotNull().WithMessage(x => localizer["HELLO"]);
        }
    }

    public class TestWeatherForcast
    {
        public string Name { get; set; }
    }
}
