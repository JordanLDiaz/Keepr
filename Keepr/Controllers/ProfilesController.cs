namespace Keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfilesController : ControllerBase
{
  private readonly ProfilesService _profilesService;
  private readonly Auth0Provider _auth0provider;
  private readonly VaultsService _vaultsService;

  public ProfilesController(ProfilesService profilesService, Auth0Provider auth0Provider, VaultsService vaultsService)
  {
    _profilesService = profilesService;
    _auth0provider = auth0Provider;
    _vaultsService = vaultsService;
  }

  [HttpGet("{profileId}")]

  public ActionResult<Profile> GetUserProfile(string profileId)
  {
    try
    {
      Profile profile = _profilesService.GetUserProfile(profileId);
      return Ok(profile);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{profileId}/keeps")]
  public ActionResult<List<Keep>> GetUserKeeps(string profileId)
  {
    try
    {
      List<Keep> profileKeeps = _profilesService.GetUserKeeps(profileId);
      return Ok(profileKeeps);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{profileId}/vaults")]
  public ActionResult<List<Vault>> GetUserVaults(string profileId)
  {
    try
    {
      List<Vault> profileVaults = _profilesService.GetUserVaults(profileId);
      return Ok(profileVaults);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
