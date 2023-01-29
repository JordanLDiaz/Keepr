namespace Keepr.Services;

public class VaultKeepsService
{
  private readonly VaultKeepsRepository _vkRepo;
  private readonly VaultsService _vaultsService;
  private readonly KeepsService _keepsService;

  public VaultKeepsService(VaultKeepsRepository vkRepo, VaultsService vaultsService, KeepsService keepsService)
  {
    _vkRepo = vkRepo;
    _vaultsService = vaultsService;
    _keepsService = keepsService;
  }

  internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
  {
    VaultKeep vaultKeep = _vkRepo.CreateVaultKeep(vaultKeepData);
    return vaultKeep;
  }

  internal List<KeptKeep> GetKeepsByVaultId(int vaultId, string userId)
  {
    Vault vault = _vaultsService.GetVaultById(vaultId, userId);
    List<KeptKeep> vaultKeeps = _vkRepo.GetKeepsByVaultId(vaultId);
    return vaultKeeps;
  }

  public VaultKeep GetOneVaultKeepById(int vaultKeepId)
  {
    VaultKeep vaultKeep = _vkRepo.GetOneVaultKeepById(vaultKeepId);
    if (vaultKeep == null)
    {
      throw new Exception("No vault keep found by this id.");
    }
    return vaultKeep;
  }

  internal string RemoveVaultKeep(int vaultKeepId, string userId)
  {
    VaultKeep vaultKeep = GetOneVaultKeepById(vaultKeepId);
    if (vaultKeep.CreatorId != userId)
    {
      throw new Exception("You do not have permission to remove the keep from this vault.");
    }
    string message = _vkRepo.RemoveVaultKeep(vaultKeepId);
    return message;
  }
}
