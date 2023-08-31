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
    INSERT INTO vaultKeeps
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
    vaultKeeps.*,
    COUNT(vaultKeeps.id) AS Kept,
    vaultKeeps.id AS vaultKeepId,
    vaultKeeps.CreatorId AS VaultKeepCreatorId,
    keeps.*,
    accounts.*
    FROM vaultKeeps
    JOIN accounts ON accounts.id = vaultKeeps.creatorId
    JOIN keeps ON keeps.id = vaultKeeps.keepId
    WHERE vaultKeeps.vaultId = @vaultId
    GROUP BY vaultKeeps.id;
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
    vaultKeeps.id AS vaultKeepId
    FROM vaultKeeps
    WHERE id = @vaultKeepId;
    ";
    return _db.Query<VaultKeep>(sql, new { vaultKeepId }).FirstOrDefault();
  }

  internal string RemoveVaultKeep(int vaultKeepId)
  {
    string sql = @"
    DELETE FROM vaultKeeps
    WHERE id = @vaultKeepId;
    ";
    _db.Execute(sql, new { vaultKeepId });
    return "Vault keep was deleted.";
  }
}
