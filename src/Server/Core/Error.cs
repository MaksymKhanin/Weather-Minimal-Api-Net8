﻿namespace Core;
public record Error(ErrorCode ErrorCode, string Message);
public record NotFoundError(ErrorCode ErrorCode, string Message) : Error(ErrorCode, Message) { public NotFoundError() : this(ErrorCode.NotFoundError, "No Data was found") { } }
public record AlreadyExistsError(ErrorCode ErrorCode, string Message) : Error(ErrorCode, Message) { public AlreadyExistsError() : this(ErrorCode.AlreadyExistsError, "Record with such data already exists.") { } }
public record ValidationError(ErrorCode ErrorCode, string Message) : Error(ErrorCode, Message) { public ValidationError() : this(ErrorCode.ValidationError, "Validation of object failed.") { } }
public record ValidationFailedError(string error) : ValidationError(ErrorCode.WeatherValidationError, $"Weather validation failed with error: {error}");
public record WeatherForecastNotFoundError(DateOnly Date) : NotFoundError(ErrorCode.WeatherForecastNotFoundError, $"Weather forecast for date: {Date} not found in storage.");
public record WeatherForecastAlreadyExistsError() : AlreadyExistsError(ErrorCode.WeatherForecastAlreadyExistsError, "Weather forecast with same data already exists.");
public record WeatherValidationError(string PropertyName, string PropertyValue) : ValidationError(ErrorCode.WeatherValidationError, $"Weather property: {PropertyName} with value: {PropertyValue} is invalid.");
public record WeatherNameIsEmptyError() : ValidationError(ErrorCode.WeatherNameIsEmpty, $"Weather name is empty.");
public record WeatherNameIsTooLongError() : ValidationError(ErrorCode.WeatherNameIsTooLong, $"Weather name is too long.");
public record WeatherForecastValidationError(string PropertyName, string PropertyValue) : ValidationError(ErrorCode.WeatherForecastValidationError, $"Weather forecast property: {PropertyName} with value: {PropertyValue} is invalid.");


public enum ErrorCode
{
    UnknownError = 0,
    NotFoundError = 1,
    WeatherForecastNotFoundError = 10,
    AlreadyExistsError = 2,
    WeatherForecastAlreadyExistsError = 20,
    ValidationError = 3,
    WeatherValidationError = 310,
    WeatherNameIsEmpty = 311,
    WeatherNameIsTooLong = 312,
    WeatherForecastValidationError = 320,

}
