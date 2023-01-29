namespace Keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultKeepsController : ControllerBase
{
  private readonly VaultKeepsService _vkService;
  private readonly Auth0Provider _auth0provider;

  public VaultKeepsController(VaultKeepsService vkService, Auth0Provider auth0provider)
  {
    _vkService = vkService;
    _auth0provider = auth0provider;
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<VaultKeep>> CreateVaultKeep([FromBody] VaultKeep vaultKeepData, string userId)
  {
    try
    {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      vaultKeepData.CreatorId = userInfo.Id;
      VaultKeep vaultKeep = _vkService.CreateVaultKeep(vaultKeepData, userId);
      return Ok(vaultKeep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpDelete("{vaultKeepId}")]
  [Authorize]
  public async Task<ActionResult<string>> RemoveVaultKeep(int vaultKeepId)
  {
    try
    {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      string message = _vkService.RemoveVaultKeep(vaultKeepId, userInfo.Id);
      return Ok(message);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
