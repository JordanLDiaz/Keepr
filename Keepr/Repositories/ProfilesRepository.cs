namespace Keepr.Repositories;

public class ProfilesRepository
{
  private readonly IDbConnection _db;

  public ProfilesRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Profile GetUserProfile(string profileId)
  {
    string sql = @"
    SELECT 
    *
    FROM JDaccounts
    WHERE id = @profileId;
    ";
    return _db.Query<Profile>(sql, new { profileId }).FirstOrDefault();
  }

  internal List<Keep> GetUserKeeps(string profileId)
  {
    string sql = @"
    SELECT
    JDkeeps.*,
    JDaccounts.*
    FROM JDkeeps
    JOIN JDaccounts ON JDaccounts.id = JDkeeps.creatorId
    WHERE JDkeeps.creatorId = @profileId;
    ";
    return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
    {
      keep.Creator = profile;
      return keep;
    }, new { profileId }).ToList();
  }

  internal List<Vault> GetUserVaults(string profileId)
  {
    string sql = @"
    SELECT
    JDvaults.*,
    JDaccounts.*
    FROM JDvaults
    JOIN JDaccounts ON JDaccounts.id = JDvaults.creatorId
    WHERE JDvaults.creatorId = @profileId && JDvaults.isPrivate != 1;
    ";
    return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
    {
      vault.Creator = profile;
      return vault;
    }, new { profileId }).ToList();
  }
}
