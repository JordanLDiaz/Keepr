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
    FROM accounts
    WHERE id = @profileId;
    ";
    return _db.Query<Profile>(sql, new { profileId }).FirstOrDefault();
  }

  internal List<Keep> GetUserKeeps(string profileId)
  {
    string sql = @"
    SELECT
    keeps.*,
    accounts.*
    FROM keeps
    JOIN accounts ON accounts.id = keeps.creatorId
    WHERE keeps.creatorId = @profileId;
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
    vaults.*,
    accounts.*
    FROM vaults
    JOIN accounts ON accounts.id = vaults.creatorId
    WHERE vaults.creatorId = @profileId && vaults.isPrivate != 1;
    ";
    return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
    {
      vault.Creator = profile;
      return vault;
    }, new { profileId }).ToList();
  }
}
