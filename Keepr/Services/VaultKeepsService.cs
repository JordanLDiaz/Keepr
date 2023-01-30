namespace Keepr.Services;

public class VaultKeepsService
{
  private readonly VaultKeepsRepository _vkRepo;
  private readonly VaultsService _vaultsService;
  private readonly KeepsService _keepsService;
  private readonly VaultsRepository _vaultsRepo;
  private readonly KeepsRepository _repo;

  public VaultKeepsService(VaultKeepsRepository vkRepo, VaultsService vaultsService, KeepsService keepsService, VaultsRepository vaultsRepository, KeepsRepository keepsRepository)
  {
    _vkRepo = vkRepo;
    _vaultsService = vaultsService;
    _keepsService = keepsService;
    _vaultsRepo = vaultsRepository;
    _repo = keepsRepository;
  }

  internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
  {
    Vault vault = _vaultsRepo.GetVaultById(vaultKeepData.VaultId);
    Keep keep = _repo.GetOne(vaultKeepData.KeepId);
    keep.Kept++;
    if (vault == null)
    {
      throw new Exception("No vault at this id.");
    }
    if (vault.CreatorId != vaultKeepData.CreatorId)
    {
      throw new Exception("You do not have permission to add a keep to this vault.");
    }
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
      throw new Exception("You do not have permission to remove a keep from someone elses vault.");
    }
    string message = _vkRepo.RemoveVaultKeep(vaultKeepId);
    return message;
  }
}
