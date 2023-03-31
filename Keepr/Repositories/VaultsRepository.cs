namespace Keepr.Repositories;

public class VaultsRepository
{
  private readonly IDbConnection _db;

  public VaultsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Vault CreateVault(Vault vaultData)
  {
    string sql = @"
    INSERT INTO JDvaults
    (creatorId, name, description, img, isPrivate)
    VALUES
    (@creatorId, @name, @description, @img, @isPrivate);
    SELECT LAST_INSERT_ID();
    ";
    int id = _db.ExecuteScalar<int>(sql, vaultData);
    vaultData.Id = id;
    return vaultData;
  }

  internal Vault GetVaultById(int vaultId)
  {
    string sql = @"
    SELECT
    JDvaults.*,
    JDaccounts.*
    FROM JDvaults
    JOIN JDaccounts ON JDaccounts.id = JDvaults.CreatorId
    WHERE JDvaults.id = @vaultId;
    ";
    Vault vault = _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
    {
      vault.Creator = profile;
      return vault;
    }, new { vaultId }).FirstOrDefault();
    return vault;
  }
  internal bool EditVault(Vault original)
  {
    string sql = @"
    UPDATE JDvaults
    SET
    name = @name,
    description = @description,
    img = @img,
    isPrivate = @isPrivate
    where id = @id;
    ";
    int rows = _db.Execute(sql, original);
    return rows > 0;
  }

  internal void RemoveVault(int id)
  {
    string sql = @"
    DELETE FROM JDvaults
    WHERE id = @id;
    ";
    _db.Execute(sql, new { id });
  }

  internal List<Vault> GetMyVaults(string id)
  {
    string sql = @"
    SELECT
    JDvaults.*,
    JDaccounts.*
    FROM JDvaults
    JOIN JDaccounts ON JDaccounts.id = JDvaults.creatorId
    WHERE JDvaults.creatorId = @id;
    ";
    return _db.Query<Vault, Account, Vault>(sql, (vault, account) =>
    {
      vault.Creator = account;
      return vault;
    }, new { id }).ToList();
  }

}
