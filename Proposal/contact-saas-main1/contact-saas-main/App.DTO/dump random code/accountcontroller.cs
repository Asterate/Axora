/*namespace App.DTO.dump_random_code;

public class accountcontroller
{
    // ✅ Login — just move JWT generation inside AccountService eventually
// for now this is fine as-is, just remove unused injected services
[HttpPost]
[AllowAnonymous]
public async Task<ActionResult<JWTResponse>> Login(
    [FromBody] Login loginInfo,
    [FromQuery] int? jwtExpiresInSeconds,
    [FromQuery] int? refreshTokenExpiresInSeconds)
{
    var result = await _accountService.Login(loginInfo, jwtExpiresInSeconds, refreshTokenExpiresInSeconds);
    if (!result.Success)
        return Unauthorized(new Message(result.Error!));

    var jwt = IdentityExtensions.GenerateJwt(
        result.ClaimsPrincipal!.Claims,
        _configuration.GetValue<string>(SettingsJWTKey)!,
        _configuration.GetValue<string>(SettingsJWTIssuer)!,
        _configuration.GetValue<string>(SettingsJWTAudience)!,
        DateTime.UtcNow.AddSeconds(_configuration.GetValue<int>(SettingsJWTExpiresInSeconds)));

    return Ok(new JWTResponse { JWT = jwt, RefreshToken = result.RefreshToken! });
}

// ✅ Register — now thin, publishes event
[HttpPost]
[AllowAnonymous]
[Produces("application/json")]
[Consumes("application/json")]
[ProducesResponseType(typeof(JWTResponse), StatusCodes.Status200OK)]
[ProducesResponseType(typeof(Message), StatusCodes.Status400BadRequest)]
public async Task<ActionResult<JWTResponse>> Register(
    [FromBody] Register registerModel,
    [FromQuery] int? jwtExpiresInSeconds,
    [FromQuery] int? refreshTokenExpiresInSeconds,
    CancellationToken ct)
{
    // validate before touching DB
    var validationError = ValidateInstituteSelection(registerModel);
    if (validationError != null)
        return BadRequest(new Message(validationError));

    // Identity module — create user + refresh token
    var accountResult = await _accountService.RegisterUserAsync(
        registerModel.Email,
        registerModel.Password,
        refreshTokenExpiresInSeconds);

    if (!accountResult.Success)
        return BadRequest(new Message(accountResult.Error!));

    // Research module — via event, no direct reference
    try
    {
        await _mediator.Publish(new UserRegisteredEvent(
            UserId: accountResult.User!.Id,
            Email: registerModel.Email,
            IsNewInstitute: registerModel.InstituteSelection == InstituteSelectionType.CreateNew,
            ExistingInstituteId: registerModel.InstituteId,
            NewInstituteName: registerModel.NewInstitute?.InstituteName,
            NewInstituteCountry: registerModel.NewInstitute?.InstituteCountry,
            NewInstituteAddress: registerModel.NewInstitute?.InstituteAddress,
            NewInstitutePhone: registerModel.NewInstitute?.InstitutePhoneNumber,
            NewInstituteTypeId: registerModel.NewInstitute?.InstituteTypeId
        ), ct);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Institute linking failed for {Email}", registerModel.Email);
        return BadRequest(new Message("Registration failed during institute setup"));
    }

    var jwt = IdentityExtensions.GenerateJwt(
        accountResult.ClaimsPrincipal!.Claims,
        _configuration.GetValue<string>(SettingsJWTKey)!,
        _configuration.GetValue<string>(SettingsJWTIssuer)!,
        _configuration.GetValue<string>(SettingsJWTAudience)!,
        GetExpirationDateTime(jwtExpiresInSeconds, SettingsJWTExpiresInSeconds));

    return Ok(new JWTResponse
    {
        JWT = jwt,
        RefreshToken = accountResult.RefreshToken!
    });
}

// ✅ RenewRefreshToken — stays mostly the same, fine for now
[HttpPost]
[AllowAnonymous]
public async Task<ActionResult<JWTResponse>> RenewRefreshToken(
    [FromBody] RefreshTokenModel refreshTokenModel,
    [FromQuery] int? jwtExpiresInSeconds,
    [FromQuery] int? refreshTokenExpiresInSeconds)
{
    var result = await _accountService.RenewRefreshTokenAsync(
        refreshTokenModel,
        refreshTokenExpiresInSeconds);

    if (!result.Success)
        return BadRequest(new Message(result.Error!));

    var jwt = IdentityExtensions.GenerateJwt(
        result.ClaimsPrincipal!.Claims,
        _configuration.GetValue<string>(SettingsJWTKey)!,
        _configuration.GetValue<string>(SettingsJWTIssuer)!,
        _configuration.GetValue<string>(SettingsJWTAudience)!,
        GetExpirationDateTime(jwtExpiresInSeconds, SettingsJWTExpiresInSeconds));

    return Ok(new JWTResponse { JWT = jwt, RefreshToken = result.RefreshToken! });
}

// ✅ Logout — fine as-is
[HttpPost]
[Authorize(AuthenticationSchemes = "Bearer")]
public async Task<ActionResult> Logout([FromBody] LogoutInfo logout)
{
    var userId = User.UserId();
    if (string.IsNullOrWhiteSpace(logout.RefreshToken))
        return BadRequest(new Message("Refresh token is required"));

    await _accountService.LogoutAsync(logout.RefreshToken, userId);
    return Ok();
}

// ✅ SetInstitute — publish event instead of hitting DB directly
[HttpPost("set-institute")]
[Authorize(AuthenticationSchemes = "Bearer")]
public async Task<ActionResult> SetInstitute(
    [FromBody] SetInstituteDto setInstitute,
    CancellationToken ct)
{
    var userId = User.UserId();

    try
    {
        await _mediator.Publish(new UserRegisteredEvent(
            UserId: userId,
            Email: User.FindFirstValue(ClaimTypes.Email)!,
            IsNewInstitute: setInstitute.InstituteSelection == (int)InstituteSelectionType.CreateNew,
            ExistingInstituteId: Guid.TryParse(setInstitute.InstituteId, out var g) ? g : null,
            NewInstituteName: setInstitute.NewInstitute?.InstituteName,
            NewInstituteCountry: setInstitute.NewInstitute?.InstituteCountry,
            NewInstituteAddress: setInstitute.NewInstitute?.InstituteAddress,
            NewInstitutePhone: setInstitute.NewInstitute?.InstitutePhoneNumber,
            NewInstituteTypeId: Guid.TryParse(setInstitute.NewInstitute?.InstituteTypeId, out var tg) ? tg : null
        ), ct);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "SetInstitute failed for user {UserId}", userId);
        return BadRequest(new Message(ex.Message));
    }

    return Ok();
}

// stays private in controller
private static string? ValidateInstituteSelection(Register model) =>
    model.InstituteSelection switch
    {
        InstituteSelectionType.CreateNew when model.NewInstitute == null => "New institute details are required",
        InstituteSelectionType.SelectExisting when model.InstituteId == null => "Institute ID is required",
        InstituteSelectionType.CreateNew => null,
        InstituteSelectionType.SelectExisting => null,
        _ => "Invalid institute selection"
    };

private DateTime GetExpirationDateTime(int? expiresInSeconds, string settingsKey)
{
    if (expiresInSeconds <= 0) expiresInSeconds = int.MaxValue;
    expiresInSeconds = expiresInSeconds < _configuration.GetValue<int>(settingsKey)
        ? expiresInSeconds
        : _configuration.GetValue<int>(settingsKey);
    return DateTime.UtcNow.AddSeconds(expiresInSeconds ?? 60);
}
}*/