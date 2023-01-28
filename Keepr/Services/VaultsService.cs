namespace Keepr.Services;

public class VaultsService
{
  private readonly VaultsRepository _repo;

  public VaultsService(VaultsRepository repo)
  {
    _repo = repo;
  }

  internal Vault CreateVault(Vault vaultData)
  {
    Vault vault = _repo.CreateVault(vaultData);
    return vault;
  }

  internal Vault GetVaultById(int vaultId, string id)
  {
    Vault vault = _repo.GetVaultById(vaultId);
    if (vault == null)
    {
      throw new Exception("No vault at this id.");
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

    _repo.EditVault(original);
    return original;
  }

  internal string RemoveVault(int vaultId, string userId)
  {
    Vault vault = GetVaultById(vaultId, userId);
    if (vault.CreatorId != userId)
    {
      throw new Exception("You do not have permission to delete this vault.");
    }
    string message = _repo.RemoveVault(vaultId);
    return message;
  }
}
