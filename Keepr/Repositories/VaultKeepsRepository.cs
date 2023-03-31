namespace Keepr.Repositories;

public class VaultKeepsRepository
{
  private readonly IDbConnection _db;

  public VaultKeepsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
  {
    string sql = @"
    INSERT INTO JDvaultKeeps
    (creatorId, vaultId, keepId)
    VALUES
    (@creatorId, @vaultId, @keepId);
    SELECT LAST_INSERT_ID();
    ";
    int id = _db.ExecuteScalar<int>(sql, vaultKeepData);
    vaultKeepData.Id = id;
    return vaultKeepData;
  }

  // WHAT A DOOZY! 😅
  internal List<KeptKeep> GetKeepsByVaultId(int vaultId)
  {
    string sql = @"
    SELECT 
    JDvaultKeeps.*,
    COUNT(JDvaultKeeps.id) AS Kept,
    JDvaultKeeps.id AS vaultKeepId,
    JDvaultKeeps.CreatorId AS VaultKeepCreatorId,
    JDkeeps.*,
    JDaccounts.*
    FROM JDvaultKeeps
    JOIN JDaccounts ON JDaccounts.id = JDvaultKeeps.creatorId
    JOIN JDkeeps ON JDkeeps.id = JDvaultKeeps.keepId
    WHERE JDvaultKeeps.vaultId = @vaultId
    GROUP BY JDvaultKeeps.id;
    ";
    return _db.Query<KeptKeep, Profile, KeptKeep>(sql, (keep, profile) =>
    {
      keep.Creator = profile;
      return keep;
    }, new { vaultId }).ToList();
  }

  internal VaultKeep GetOneVaultKeepById(int vaultKeepId)
  {
    string sql = @"
    SELECT
    *,
    JDvaultKeeps.id AS vaultKeepId
    FROM JDvaultKeeps
    WHERE id = @vaultKeepId;
    ";
    return _db.Query<VaultKeep>(sql, new { vaultKeepId }).FirstOrDefault();
  }

  internal string RemoveVaultKeep(int vaultKeepId)
  {
    string sql = @"
    DELETE FROM JDvaultKeeps
    WHERE id = @vaultKeepId;
    ";
    _db.Execute(sql, new { vaultKeepId });
    return "Vault keep was deleted.";
  }
}
