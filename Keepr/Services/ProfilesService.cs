namespace Keepr.Services;

public class ProfilesService
{
  private readonly ProfilesRepository _pRepo;

  public ProfilesService(ProfilesRepository pRepo)
  {
    _pRepo = pRepo;
  }


  internal Profile GetUserProfile(string profileId)
  {
    Profile profile = _pRepo.GetUserProfile(profileId);
    if (profile == null)
    {
      throw new Exception("No profile at this id.");
    }
    return profile;
  }

  internal List<Keep> GetUserKeeps(string profileId)
  {
    return _pRepo.GetUserKeeps(profileId);
  }

  internal List<Vault> GetUserVaults(string profileId)
  {
    return _pRepo.GetUserVaults(profileId);
  }
}
