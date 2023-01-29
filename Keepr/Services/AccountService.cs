namespace Keepr.Services;

public class AccountService
{
  private readonly AccountsRepository _repo;
  private readonly VaultsRepository _vaultsRepository;

  public AccountService(AccountsRepository repo, VaultsRepository vaultsRepository)
  {
    _repo = repo;
    _vaultsRepository = vaultsRepository;
  }

  internal Account GetProfileByEmail(string email)
  {
    return _repo.GetByEmail(email);
  }

  internal Account GetOrCreateProfile(Account userInfo)
  {
    Account profile = _repo.GetById(userInfo.Id);
    if (profile == null)
    {
      return _repo.Create(userInfo);
    }
    return profile;
  }

  internal Account Edit(Account editData, string userEmail)
  {
    Account original = GetProfileByEmail(userEmail);
    original.Name = editData.Name.Length > 0 ? editData.Name : original.Name;
    original.Picture = editData.Picture.Length > 0 ? editData.Picture : original.Picture;
    return _repo.Edit(original);
  }

  internal List<Vault> GetMyVaults(string id)
  {
    return _vaultsRepository.GetMyVaults(id);
  }
}
