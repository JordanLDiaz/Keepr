namespace Keepr.Services;

public class VaultsService
{
  private readonly VaultsRepository _vaultsRepo;

  public VaultsService(VaultsRepository vaultsRepo)
  {
    _vaultsRepo = vaultsRepo;
  }

  internal Vault CreateVault(Vault vaultData)
  {
    Vault vault = _vaultsRepo.CreateVault(vaultData);
    return vault;
  }

  internal Vault GetVaultById(int vaultId, string userId)
  {
    Vault vault = _vaultsRepo.GetVaultById(vaultId);
    if (vault == null)
    {
      throw new Exception("No vault at this id.");
    }
    if (vault.IsPrivate == true && vault.CreatorId != userId)
    {
      throw new Exception("This is a private vault that you don't have access to.");
    }
    return vault;
  }

  internal Vault EditVault(Vault updateData, int vaultId, string userId)
  {
    Vault original = GetVaultById(vaultId, userId);
    if (original.CreatorId != userId)
    {
      throw new Exception("This is not your vault to edit.");
    }
    original.Name = updateData.Name ?? original.Name;
    original.Description = updateData.Description ?? original.Description;
    original.Img = updateData.Img ?? original.Img;
    original.IsPrivate = updateData.IsPrivate;

    _vaultsRepo.EditVault(original);
    return original;
  }

  internal string RemoveVault(int vaultId, string userId)
  {
    Vault vault = GetVaultById(vaultId, userId);
    if (vault.CreatorId != userId)
    {
      throw new Exception("You do not have permission to delete this vault.");
    }
    _vaultsRepo.RemoveVault(vaultId);
    return $"{vault.Name} was removed.";
  }
}

